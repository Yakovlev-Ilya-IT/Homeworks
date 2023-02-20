using System;
using System.Collections.Generic;

namespace SquadsBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UnitFactory unitFactory = new UnitFactory();

            List<Unit> units = new List<Unit>()
            {
                unitFactory.Get(UnitTypes.Soldier),
                unitFactory.Get(UnitTypes.ImprovedSoldier),
                unitFactory.Get(UnitTypes.Bomber),
                unitFactory.Get(UnitTypes.Bomber),
                unitFactory.Get(UnitTypes.Medic),
                unitFactory.Get(UnitTypes.ImprovedMedic)
            };

            Squad firstSquad = new Squad(units, "Альфа");

            units = new List<Unit>()
            {
                unitFactory.Get(UnitTypes.Soldier),
                unitFactory.Get(UnitTypes.Soldier),
                unitFactory.Get(UnitTypes.Bomber),
                unitFactory.Get(UnitTypes.ImprovedBomber),
                unitFactory.Get(UnitTypes.Medic),
                unitFactory.Get(UnitTypes.ImprovedMedic)
            };

            Squad secondSquad = new Squad(units, "Браво");

            BattleArena battleArena = new BattleArena(firstSquad, secondSquad);
            battleArena.Battle();
        }
    }

    public enum UnitTypes
    {
        Soldier = 0,
        ImprovedSoldier,
        Bomber,
        ImprovedBomber,
        Medic,
        ImprovedMedic
    }

    public class UnitFactory
    {
        public Unit Get(UnitTypes type)
        {
            switch (type)
            {
                case UnitTypes.Soldier:
                    return new Soldier("Солдат", 200, 30, 5, 2);

                case UnitTypes.ImprovedSoldier:
                    return new Soldier("Улучшенный солдат", 230, 35, 5, 3);
                    
                case UnitTypes.Bomber:
                    return new Bomber("Подрывник", 150, 30, 0, 2);

                case UnitTypes.ImprovedBomber:
                    return new Bomber("Улучшенный подрывник", 170, 20, 0, 4);

                case UnitTypes.Medic:
                    return new Medic("Медик", 200, 15, 0, 5); 

                case UnitTypes.ImprovedMedic:
                    return new Medic("Улучшенный медик", 220, 15, 0, 10);

                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }

    public class BattleArena
    {
        private Squad _firstSquad;
        private Squad _secondSquad;

        public BattleArena(Squad firstSquad, Squad secondSquad)
        {
            _firstSquad = firstSquad;
            _secondSquad = secondSquad;
        }

        public void Battle()
        {
            Random random = new Random();

            Console.WriteLine("Бой начался!");

            while(_firstSquad.Count > 0 && _secondSquad.Count > 0)
            {
                ShowSquad(_firstSquad);
                ShowSquad(_secondSquad);

                Unit firstSquadUnit = _firstSquad.GetRandomUnit(random);
                Unit secondSquadUnit = _secondSquad.GetRandomUnit(random);

                Console.WriteLine($"\nДерутся {firstSquadUnit.Name} из взвода {_firstSquad.Name} и {secondSquadUnit.Name} из взвода {_secondSquad.Name}");
                Battle(firstSquadUnit, secondSquadUnit);

                Console.WriteLine("\nЧтобы продолжить бой взводов нажмите любую клавишу");
                Console.ReadKey(true);
                Console.Clear();
            }

            ShowWinner();
        }

        private void ShowWinner()
        {
            if (_firstSquad.Count > 0)
                Console.WriteLine($"Выиграл взвод {_firstSquad.Name}");
            else if( _secondSquad.Count > 0)
                Console.WriteLine($"Выиграл взвод {_secondSquad.Name}");
            else
                Console.WriteLine("Боевая ничья! Оба взвода пали...");
        }

        private void ShowSquad(Squad squad)
        {
            Console.WriteLine($"\nУ взвода {squad.Name} осталось бойцов:");
            squad.ShowUnits();
        }

        private void Battle(Unit firstUnit, Unit secondUnit)
        {
            while (firstUnit.Health > 0 && secondUnit.Health > 0)
            {
                firstUnit.Attack(secondUnit);
                secondUnit.Attack(firstUnit);
                firstUnit.ShowCurrentHealth();
                secondUnit.ShowCurrentHealth();
            }
        }
    }

    public interface IDamagable
    {
        void TakeDamage(int damage);
    }

    public abstract class Unit : IDamagable
    {
        private int _maxHealth;
        private int _health;

        protected Unit(string name, int maxHealth, int damage, int armor)
        {
            Name = name;
            _maxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;
            Armor = armor;
        }

        public event Action<Unit> Died;

        public string Name { get; }
        public int Health
        {
            get => _health;
            protected set
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

            if(Health <= 0)
                Died?.Invoke(this);
        }
    }

    public class Squad
    {
        private List<Unit> _units;

        public Squad(List<Unit> units, string name)
        {
            _units = new List<Unit>(units);
            Name = name;

            foreach (Unit unit in _units)
                unit.Died += OnDied;
        }

        ~Squad()
        {
            foreach (Unit unit in _units)
                unit.Died -= OnDied;
        }

        public string Name { get; }
        public int Count => _units.Count;

        public Unit GetRandomUnit(Random random) => _units[random.Next(0, _units.Count)];

        public void ShowUnits()
        {
            for(int i = 0; i < _units.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _units[i].ShowStats();
            }
        }

        private void OnDied(Unit unit)
        {
            unit.Died -= OnDied;
            _units.Remove(unit);
        }
    }

    public class Soldier : Unit
    {
        private int _armorMultiplier;

        public Soldier(string name, int maxHealth, int damage, int armor, int armorMultiplier) : base(name, maxHealth, damage, armor)
        {
            _armorMultiplier = armorMultiplier;
        }

        protected override int Armor => base.Armor * _armorMultiplier;
    }

    public class Bomber : Unit
    {
        private int _damageMultiplier;

        public Bomber(string name, int maxHealth, int damage, int armor, int damageMultiplier) : base(name, maxHealth, damage, armor)
        {
            _damageMultiplier = damageMultiplier;
        }

        protected override int Damage => base.Damage * _damageMultiplier;
    }

    public class Medic : Unit
    {
        private int _healAmount;

        public Medic(string name, int maxHealth, int damage, int armor, int healAmount) : base(name, maxHealth, damage, armor)
        {
            _healAmount = healAmount;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            Health += _healAmount;
        }
    }
}
