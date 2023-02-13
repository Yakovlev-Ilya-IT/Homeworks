﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            hospital.Work();
        }
    }

    public class Hospital
    {
        private List<Patient> _patients = new List<Patient>()
        {
            new Patient("Цветков Ефрем Данилович", 29, Diseases.Бронхит),
            new Patient("Макаров Севастьян Максимович", 20, Diseases.Бронхит),
            new Patient("Стрелков Давид Оскарович", 29, Diseases.Бронхит),
            new Patient("Лебедев Юлиан Георгьевич", 45, Diseases.Диабет),
            new Patient("Юдин Вольдемар Васильевич", 50, Diseases.Диабет),
            new Patient("Королёв Любовь Протасьевич", 18, Diseases.Диабет),
            new Patient("Князев Алексей Макарович", 18, Diseases.Коронавирус),
            new Patient("Беляков Ефим Оскарович", 50, Diseases.Коронавирус),
            new Patient("Галкин Роберт Русланович", 31, Diseases.Коронавирус),
            new Patient("Мышкин Афанасий Иванович", 29, Diseases.Коронавирус)
        };

        public void Work()
        {
            const string sortingByFullNameCommand = "1";
            const string sortingByAgeCommand = "2";
            const string sortingByDiseaseCommand = "3";
            const string exitCommand = "4";

            bool isWorking = true;

            while (isWorking)
            {
                ShowMenu(sortingByFullNameCommand, sortingByAgeCommand, sortingByDiseaseCommand, exitCommand);

                string input = Console.ReadLine();

                switch (input)
                {
                    case sortingByFullNameCommand:
                        SortByFullName();
                        break;

                    case sortingByAgeCommand:
                        SortByAge();
                        break;

                    case sortingByDiseaseCommand:
                        SortByDisease();
                        break;

                    case exitCommand:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Команда нераспознана попробуйте еще");
                        break;
                }
            }
        }

        private void SortByDisease()
        {
            Console.WriteLine("\nВведите заболевание");
            string Disease = Console.ReadLine();

            var sortedPatients = _patients
                .Where(patient => patient.Disease.ToString().ToUpper() == Disease.ToUpper());

            ShowSorted(sortedPatients);
        }

        private void SortByAge()
        {
            Console.WriteLine("\nВведите возраст");
            int age = GetInputNumber();

            var sortedPatients = _patients
                .Where(patient => patient.Age == age);

            ShowSorted(sortedPatients);
        }

        private int GetInputNumber()
        {
            uint number = 0;

            while (uint.TryParse(Console.ReadLine(), out number) == false)
                Console.WriteLine("Введите неотрицательное число");

            return (int)number;
        }

        private void SortByFullName()
        {
            Console.WriteLine("\nВведите фио");
            string fullName = Console.ReadLine();

            var sortedPatients = _patients
                .Where(patient => patient.FullName.ToUpper() == fullName.ToUpper());

            ShowSorted(sortedPatients);
        }

        private void ShowSorted(IEnumerable<Patient> patients)
        {
            if(patients.Count() == 0)
            {
                Console.WriteLine("Таких пациентов нет");
                return;
            }

            Console.WriteLine("Отсортированные пациенты");
            foreach (var patient in patients)
                Console.WriteLine(patient.FullName);
        }

        private void ShowMenu(string sortingByFullNameCommand, string sortingByAgeCommand, string sortingByDiseaseCommand, string exitCommand)
        {
            Console.WriteLine("\nВыберите один из следующих пунктов");
            Console.WriteLine($"Сортировать больных по ФИО - {sortingByFullNameCommand}");
            Console.WriteLine($"Сортироваться больных по возрасту - {sortingByAgeCommand}");
            Console.WriteLine($"Вывести больных с определенным заболеванием - {sortingByDiseaseCommand}");
            Console.WriteLine($"Выйти из программы - {exitCommand} \n");
        }
    }

    public enum Diseases
    {
        Бронхит = 0,
        Диабет,
        Коронавирус,
    }

    public class Patient
    {
        public Patient(string fullName, int age, Diseases disease)
        {
            FullName = fullName;
            Age = age;
            Disease = disease;
        }

        public string FullName { get; }
        public int Age { get; }
        public Diseases Disease { get; }
    }
}