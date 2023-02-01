using System;

namespace PlayerInformation
{
    public class Player
    {
        private string _name;
        private int _power;
        private int _level;
        private int _health;

        public Player(string name, int power, int level, int health)
        {
            _name = name;

            if(power < 0)
                _power = 0;
            else
                _power = power;

            if(level < 0)
                level = 1;
            else
                _level = level;

            if (health < 0)
                health = 5;
            else
                _health = health;
        }

        public void ShowInformation() => Console.WriteLine($"Я {_name}, моя сила {_power}, мой уровень {_level} и у меня осталось {_health} жизней");
    }
}
