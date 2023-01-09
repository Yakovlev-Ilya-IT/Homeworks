using System;

namespace CalculationOfWaitiongInQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint timeForServicePerPeopleInMinutes = 10;
            uint numberOfMinutesInHour = 60;

            Console.Write("Введите кол-во людей в очереди: ");

            uint peopleNumber = uint.Parse(Console.ReadLine());

            uint waitingTimeInMinutes = peopleNumber * timeForServicePerPeopleInMinutes;
            uint numberOfFullWaitingHours = waitingTimeInMinutes / numberOfMinutesInHour;
            uint numberOfRemainingWaitingMinutes = waitingTimeInMinutes % numberOfMinutesInHour;

            Console.WriteLine($"Вы должны отстоять в очереди {numberOfFullWaitingHours} часа и {numberOfRemainingWaitingMinutes} минут.");
        }
    }
}
