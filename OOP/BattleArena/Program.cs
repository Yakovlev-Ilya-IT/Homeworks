using System;
using System.Collections.Generic;

namespace BattleArena
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Fighter> fighters = new List<Fighter>()
            {
                new Berserk("Берсерк Гатс", 1000, 100, 20, 500, 2),
                new Knight("Рыцарь Артур", 1200, 70, 30, 2, 4, 2),
                new Paladin("Паладин Вестероса", 1200, 70, 20, 3, 80),
                new ShadowTheif("Теневой вор Люк", 900, 110, 5, 30, 3, 2),
                new Archer("Лучник Робин", 900, 120, 10, 3)
            };

            BattleArena battleArena = new BattleArena(fighters);

            while (battleArena.TryBattle() == false)
            {
                Console.WriteLine("Выберите бойцов для боя");
                battleArena.ShowFighters();
                battleArena.ChooseFightersForBattle();
            }
        }
    }

    public class BattleArena
    {
        private List<Fighter> _fighters;

        private Fighter _firstFighterForBattle;
        private Fighter _secondFighterForBattle;

        private bool _isBattleReady;

        public BattleArena(List<Fighter> fighters)
        {
            _fighters = new List<Fighter>(fighters);
        }

        public void ShowFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _fighters[i].ShowStats();
            }
        }

        public void ChooseFightersForBattle()
        {
            bool isFightersMatch = true;

            do
            {
                Console.WriteLine("Выберите номер первого бойца для боя");

                HandleFighterNumberInput(out int fighterNumber);

                _firstFighterForBattle = _fighters[fighterNumber];

                Console.WriteLine("Выберите номер второго бойца для боя");

                HandleFighterNumberInput(out fighterNumber);

                _secondFighterForBattle = _fighters[fighterNumber];

                isFightersMatch = _firstFighterForBattle == _secondFighterForBattle;

                if (isFightersMatch)
                    Console.WriteLine("Вы выбрали одного и того же бойца, выберите разных бойцов для сражения");

            } while (isFightersMatch);

            _isBattleReady = true;
            Console.WriteLine("Бойцы успешно выбраны");
        }

        public bool TryBattle()
        {
            if (_isBattleReady == false)
                return false;

            while (_firstFighterForBattle.Health > 0 && _secondFighterForBattle.Health > 0)
            {
                _firstFighterForBattle.Attack(_secondFighterForBattle);
                _secondFighterForBattle.Attack(_firstFighterForBattle);
                _firstFighterForBattle.ShowCurrentHealth();
                _secondFighterForBattle.ShowCurrentHealth();
            }

            if (_firstFighterForBattle.Health > 0)
            {
                Console.WriteLine($"Выиграл боец {_firstFighterForBattle.Name}");
                return true;
            }

            if (_secondFighterForBattle.Health > 0)
            {
                Console.WriteLine($"Выиграл боец {_secondFighterForBattle.Name}");
                return true;
            }

            Console.WriteLine("Боевая ничья! Оба бойца пали смертью храбрых...");
            return true;
        }

        private void HandleFighterNumberInput(out int fighterNumber)
        {
            bool isInputSuccessful = false;
            bool isInputNotOutOfRange = false;

            do
            {
                isInputSuccessful = uint.TryParse(Console.ReadLine(), out uint number);
                fighterNumber = (int)number - 1;

                if (isInputSuccessful)
                {
                    isInputNotOutOfRange = fighterNumber >= 0 && fighterNumber < _fighters.Count;

                    if (isInputNotOutOfRange == false)
                        Console.WriteLine("Ошибка, такого номера нет в списке бойцов");
                }
                else
                {
                    Console.WriteLine("Ошибка, введите неотрицательное число");
                }

            } while (isInputSuccessful == false && isInputNotOutOfRange == false);
        }
    }

    public interface IDamagable
    {
        void TakeDamage(int damage);
    }

    public abstract class Fighter : IDamagable
    {
        private int _maxHealth;
        private int _health;

        protected Fighter(string name, int maxHealth, int damage, int armor)
        {
            Name = name;
            _maxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; }
        public int Health
        {
            get => _health;
            set
            {
                if (value > _maxHealth)
                    _health = _maxHealth;

                _health = value;
            }
        }
        protected virtual int Damage { get; }
        protected virtual int Armor { get; }

        public void ShowStats() => Console.WriteLine($"{Name} - здоровье: {Health}, урон: {Damage}, броня: {Armor}");

        public void ShowCurrentHealth() => Console.WriteLine($"{Name} - здоровье: {Health}");

        public virtual void Attack(IDamagable damagable) => damagable.TakeDamage(Damage);

        public virtual void TakeDamage(int damage)
        {
            if (Armor >= damage)
                return;

            Health -= damage - Armor;
        }
    }

    public class Berserk : Fighter
    {
        private const string RageEffectName = "Ярость берсерка";

        private int _amountOfDamageToUseRage;
        private int _rageDamageMultiplier;

        private int _currentReceivedDamage;

        private bool _isRageActivated;

        public Berserk(string name, int maxHealth, int damage, int armor, int amountOfDamageForUseAbility, int rageDamageMultiplier) : base(name, maxHealth, damage, armor)
        {
            _amountOfDamageToUseRage = amountOfDamageForUseAbility;
            _rageDamageMultiplier = rageDamageMultiplier;
        }

        protected override int Damage
        {
            get
            {
                if (_isRageActivated)
                    return base.Damage * _rageDamageMultiplier;

                return base.Damage;
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            _currentReceivedDamage += damage;

            if (IsCanActivateRage())
                ActivateRageEffect();
        }

        private void ActivateRageEffect()
        {
            _isRageActivated = true;
            Console.WriteLine($"{Name} активировал эффект \"{RageEffectName}\" и умножил свой урон на {_rageDamageMultiplier} до конца боя");
        }

        private bool IsCanActivateRage() => _currentReceivedDamage >= _amountOfDamageToUseRage && _isRageActivated == false;
    }

    public class Paladin : Fighter
    {
        private const string HealEffectName = "Богословение паладина";

        private int _numberOfHitsToUseHeal;
        private int _healAmount;

        private int _currentNumberOfHits;

        public Paladin(string name, int maxHealth, int damage, int armor, int numberOfHitsToUseHeal, int healAmount) : base(name, maxHealth, damage, armor)
        {
            _numberOfHitsToUseHeal = numberOfHitsToUseHeal;
            _healAmount = healAmount;
        }

        public override void Attack(IDamagable damagable)
        {
            base.Attack(damagable);

            _currentNumberOfHits++;

            if (IsCanActivateHeal())
                ActivateHeal();
        }

        private void ActivateHeal()
        {
            Health += _healAmount;
            _currentNumberOfHits = 0;

            Console.WriteLine($"{Name} активировал эффект \"{HealEffectName}\" и излечил себя на {_healAmount} здоровья");
        }

        private bool IsCanActivateHeal() => _currentNumberOfHits >= _numberOfHitsToUseHeal;
    }

    public class ShadowTheif : Fighter
    {
        private const string ShadowWorldEffectName = "Мир теней";

        private int _chanceOfActivateShadowWorld;
        private int _cooldownShadowWorld;
        private int _remainingCooldownShadowWorld;

        private int _durationShadowWorld;
        private int _remainingDurationShadowWorld;

        private bool IsShadowWorldActive => _remainingDurationShadowWorld > 0;

        public ShadowTheif(string name, int maxHealth, int damage, int armor, int chanceOfUsingShadowWorld, int cooldownShadowWorld, int durationShadowWorld) : base(name, maxHealth, damage, armor)
        {
            _chanceOfActivateShadowWorld = chanceOfUsingShadowWorld;
            _cooldownShadowWorld = cooldownShadowWorld;
            _durationShadowWorld = durationShadowWorld;
        }

        public override void TakeDamage(int damage)
        {
            if (IsShadowWorldActive)
            {
                _remainingDurationShadowWorld--;
                Console.WriteLine($"{Name} увернулся от удара благодаря способности \"{ShadowWorldEffectName}\"");

                if (_remainingDurationShadowWorld == 0)
                    Console.WriteLine($"У бойца {Name} способность \"{ShadowWorldEffectName}\" закончила действовать");

                return;
            }

            if(_remainingCooldownShadowWorld > 0)
                _remainingCooldownShadowWorld--;

            base.TakeDamage(damage);
        }

        public override void Attack(IDamagable damagable)
        {
            base.Attack(damagable);

            Random random = new Random();
            int maxChance = 100;

            if (IsCanActivateShadowWorld(random.Next(0, maxChance)))
                ActivateShadowWorld();
        }

        private bool IsCanActivateShadowWorld(int chance) => chance <= _chanceOfActivateShadowWorld && _remainingCooldownShadowWorld == 0;

        private void ActivateShadowWorld()
        {
            _remainingCooldownShadowWorld = _cooldownShadowWorld;
            _remainingDurationShadowWorld = _durationShadowWorld;

            Console.WriteLine($"{Name} активировал способность \"{ShadowWorldEffectName}\" на {_durationShadowWorld} атак (не получается урона во время действия способности)");
        }
    }

    public class Archer : Fighter
    {
        private const string DoubleShotEffectName = "Двойной выстрел";

        private int _numberOfAttackToDoubleShot;
        private int _currentNumberOfAttack;

        public Archer(string name, int maxHealth, int damage, int armor, int numberOfAttackToDoubleShot) : base(name, maxHealth, damage, armor)
        {
            _numberOfAttackToDoubleShot = numberOfAttackToDoubleShot;
        }

        public override void Attack(IDamagable damagable)
        {
            base.Attack(damagable);

            _currentNumberOfAttack++;

            if (_currentNumberOfAttack >= _numberOfAttackToDoubleShot)
                ActivateDoubleShot(damagable);
        }

        private void ActivateDoubleShot(IDamagable damagable)
        {
            _currentNumberOfAttack = 0;
            base.Attack(damagable);

            Console.WriteLine($"{Name} использовал способность {DoubleShotEffectName} и нанес двойной урон!");
        }
    }

    public class Knight : Fighter
    {
        private const string StrengtheningEffectName = "Укрепление";

        private int _strengtheningArmorMultiplier;

        private int _cooldownStrengthening;
        private int _remainingCooldownStrengthening;

        private int _durationStrengthening;
        private int _remainingDurationStrengthening;

        private bool IsStrengtheningActive => _remainingDurationStrengthening > 0;

        public Knight(string name, int maxHealth, int damage, int armor, int strengtheningArmorMultiplier, int cooldownStrengthening, int durationStrengthening) : base(name, maxHealth, damage, armor)
        {
            _strengtheningArmorMultiplier = strengtheningArmorMultiplier;
            _cooldownStrengthening = cooldownStrengthening;
            _durationStrengthening = durationStrengthening;
        }

        protected override int Armor
        {
            get
            {
                if (IsStrengtheningActive)
                    return base.Armor * _strengtheningArmorMultiplier;

                return base.Armor;
            }
        }

        public override void Attack(IDamagable damagable)
        {
            base.Attack(damagable);

            if (IsStrengtheningActive)
            {
                _remainingDurationStrengthening--;

                if (_remainingDurationStrengthening == 0)
                    Console.WriteLine($"У бойца {Name} отключается способность \"{StrengtheningEffectName}\"");
            }

            if (_remainingCooldownStrengthening > 0)
                _remainingCooldownStrengthening--;

            if (_remainingCooldownStrengthening == 0)
                ActivateStrengthening();
        }

        private void ActivateStrengthening()
        {
            _remainingCooldownStrengthening = _cooldownStrengthening;
            _remainingDurationStrengthening = _durationStrengthening;

            Console.WriteLine($"{Name} активировал спобность \"{StrengtheningEffectName}\" и умножил свою броню на {_strengtheningArmorMultiplier} на {_durationStrengthening} атаки");
        }
    }
}
