using System;

namespace BossFight
{
    internal class Program
    {
        private const string ShadowLabelCommand = "1";
        private const string ShadowAttackCommand = "2";
        private const string VampireBiteCommand = "3";
        private const string HitFromBackCommand = "4";

        static void Main(string[] args)
        {
            int windowWidth = 185;

            if (windowWidth <= Console.LargestWindowWidth)
                Console.WindowWidth = windowWidth;

            Random random = new Random();
            int maxPercantage = 100;

            string bossName = "Хеллсинг";
            ConsoleColor bossInfoColor = ConsoleColor.Red;
            int bossHealth = 1000;
            int minBossDamage = 70;
            int maxBossDamage = 130;

            string playerName = "Теневой вампир";
            ConsoleColor playerInfoColor = ConsoleColor.Green;
            int playerHealth = 1000;

            string shadowLabelName = "Теневая метка";
            ConsoleColor shadowLabelNameColor = ConsoleColor.Magenta;
            int shadowLabelWeakeningInPercentage = 40;
            int shadowLabelDuration = 2;
            int shadowLabelRemainingDuration = 0;
            bool shadowLabelIsActive = false;

            string shadowAttackName = "Теневая атака";
            ConsoleColor shadowAttackNameColor = ConsoleColor.DarkGreen;
            int shadowAttackDamage = 50;
            int shadowAttackBonusDamage = 50;

            string vampireBiteName = "Вампирский укус";
            ConsoleColor vampireBiteNameColor = ConsoleColor.DarkRed;
            int vampireBiteDamage = 30;
            int vampireBiteHeal = 70;

            string hitFromBackName = "Удар со спины";
            ConsoleColor hitFromBackNameColor = ConsoleColor.DarkYellow;
            int hitFromBackHeal = 50;
            int hitFromBackStunChanceInPercentage = 50;
            int hitFromBackStunDuration = 2;
            int hitFromBackStunRemainingDuration = 0;
            string stunEffectName = "Оглушение";
            bool attackCanStun = false;
            bool stanIsActive = false;

            Console.Write("Наконец-то я - ");
            Console.ForegroundColor = bossInfoColor;
            Console.Write(bossName);
            Console.ResetColor();
            Console.Write(" нашел тебя, ");
            Console.ForegroundColor = playerInfoColor;
            Console.WriteLine(playerName);
            Console.ResetColor();
            Console.WriteLine("\nНАЧНЕМ ЖЕ СРАЖЕНИЕ!");

            while(bossHealth > 0 && playerHealth > 0)
            {
                Console.ForegroundColor = bossInfoColor;
                Console.Write($"\n{bossName}");
                Console.Write($" - Количество хп: {bossHealth}, ");

                Console.Write($"Эффекты:");

                if (shadowLabelRemainingDuration > 0)
                {
                    shadowLabelIsActive = true;
                    Console.ForegroundColor = shadowLabelNameColor;
                    Console.Write($" {shadowLabelName} ({shadowLabelRemainingDuration} ходов)");
                    shadowLabelRemainingDuration--;
                }
                else
                {
                    shadowLabelIsActive = false;
                }

                if (hitFromBackStunRemainingDuration > 0)
                {
                    stanIsActive = true;
                    Console.ForegroundColor = hitFromBackNameColor;
                    Console.Write($" {stunEffectName} ({hitFromBackStunRemainingDuration} ходов)");
                    hitFromBackStunRemainingDuration--;
                }
                else
                {
                    stanIsActive = false;
                }

                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = playerInfoColor;
                Console.Write(playerName);
                Console.WriteLine($" - Количество хп: {playerHealth}");
                Console.ResetColor();

                Console.WriteLine("\nВыберите способность (введите соответствуюущую цифру):");

                Console.ForegroundColor = shadowLabelNameColor;
                Console.Write($"{ShadowLabelCommand}. {shadowLabelName} ");
                Console.ResetColor();
                Console.WriteLine($"- вешает на врага эффект \"{shadowLabelName}\", ослабляя урон босса на {shadowLabelWeakeningInPercentage}% (длительность {shadowLabelDuration} ходов, начиная со следующего хода)");

                Console.ForegroundColor = shadowAttackNameColor;
                Console.Write($"{ShadowAttackCommand}. {shadowAttackName} ");
                Console.ResetColor();
                Console.WriteLine($"- наносит {shadowAttackDamage} урона, если враг подвержен эффекту \"{shadowLabelName}\", то наносит дополнительно {shadowAttackBonusDamage} урона");

                Console.ForegroundColor = vampireBiteNameColor;
                Console.Write($"{VampireBiteCommand}. {vampireBiteName} ");
                Console.ResetColor();
                Console.WriteLine($"- наносит {vampireBiteDamage} урона, а так же восстанавливает {vampireBiteHeal} здоровья");

                Console.ForegroundColor = hitFromBackNameColor;
                Console.Write($"{HitFromBackCommand}. {hitFromBackName} ");
                Console.ResetColor();
                Console.WriteLine($"- вы восстанавливаете {hitFromBackHeal} здоровья, а следующая, нансоящая урон, способность с шансом {hitFromBackStunChanceInPercentage}% наложит на врага эффект \"{stunEffectName}\" на {hitFromBackStunDuration} ходов, начиная со следующего хода");

                Console.Write("\nАктивировать способность ");

                string inputCommand = Console.ReadLine();

                Console.WriteLine("\nЖурнал действий:");

                switch (inputCommand)
                {
                    case ShadowLabelCommand:
                        shadowLabelRemainingDuration = shadowLabelDuration;
                        Console.WriteLine($"Эффект \"{shadowLabelName}\" наложен");
                        break;
                    case ShadowAttackCommand:
                        bossHealth -= shadowAttackDamage;
                        Console.WriteLine($"Босс получил {shadowAttackDamage} урона");

                        if (shadowLabelIsActive)
                        {
                            bossHealth -= shadowAttackBonusDamage;
                            Console.WriteLine($"Босс получил {shadowAttackBonusDamage} урона, от эффекта \"{shadowLabelName}\"");
                        }

                        if (attackCanStun)
                        {
                            if (hitFromBackStunChanceInPercentage > random.Next(0, maxPercantage))
                            {
                                hitFromBackStunRemainingDuration = hitFromBackStunDuration;
                                Console.WriteLine($"Эффект \"{stunEffectName}\" наложен");
                            }

                            attackCanStun = false;
                        }

                        break;
                    case VampireBiteCommand:
                        bossHealth -= vampireBiteDamage;
                        Console.WriteLine($"Босс получил {vampireBiteDamage} урона");

                        playerHealth += vampireBiteHeal;
                        Console.WriteLine($"Вы восстановили {vampireBiteHeal} жизней");

                        if (attackCanStun)
                        {
                            if (hitFromBackStunChanceInPercentage > random.Next(0, maxPercantage))
                            {
                                hitFromBackStunRemainingDuration = hitFromBackStunDuration;
                                Console.WriteLine($"Эффект \"{stunEffectName}\" наложен");
                            }

                            attackCanStun = false;
                        }

                        break;
                    case HitFromBackCommand:
                        playerHealth += hitFromBackHeal;
                        Console.WriteLine($"Вы восстановили {hitFromBackHeal} жизней");

                        attackCanStun = true;
                        Console.WriteLine($"В следующий ход способность, наносящая урон, может оглушить босса с шансом {hitFromBackStunChanceInPercentage}% ");
                        break;
                    default:
                        Console.WriteLine("Введен неверный вариант способности, попробуйте еще");
                        continue;
                }

                if (stanIsActive)
                {
                    Console.WriteLine($"Босс пропускает ход за счет эффета \"{stunEffectName}\"");
                }
                else
                {
                    int resultBossDamage = random.Next(minBossDamage, maxBossDamage);

                    if (shadowLabelIsActive)
                    {
                        resultBossDamage -= (int)(resultBossDamage * shadowLabelWeakeningInPercentage / (float)maxPercantage);
                        Console.WriteLine($"Урон босса снижен на {shadowLabelWeakeningInPercentage}% за счет эффекта \"{shadowLabelName}\"");
                    }

                    playerHealth -= resultBossDamage;
                    Console.WriteLine($"Вы получили {resultBossDamage} урона");
                }
            }

            if (bossHealth <= 0 && playerHealth <= 0)
                Console.WriteLine("\nЧтож, похоже мы вместе тут умрем...");
            else if (playerHealth <= 0)
                Console.WriteLine("\nСлабак... Я знал, что мне не составит труда убить тебя");
            else
                Console.WriteLine("\nО нет... Ты убил МЕНЯЯ!!!");
        }
    }
}
