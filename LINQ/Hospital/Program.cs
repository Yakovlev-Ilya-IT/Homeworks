using System;
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
            const string SortingByFullNameCommand = "1";
            const string SortingByAgeCommand = "2";
            const string SortingByDiseaseCommand = "3";
            const string ExitCommand = "4";

            bool isWorking = true;

            while (isWorking)
            {
                ShowMenu(SortingByFullNameCommand, SortingByAgeCommand, SortingByDiseaseCommand, ExitCommand);

                string input = Console.ReadLine();

                switch (input)
                {
                    case SortingByFullNameCommand:
                        ShowSorted(SortByFullName());
                        break;

                    case SortingByAgeCommand:
                        ShowSorted(SortByAge());
                        break;

                    case SortingByDiseaseCommand:
                        ShowSorted(FilterByDisease());
                        break;

                    case ExitCommand:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Команда нераспознана попробуйте еще");
                        break;
                }
            }
        }

        private IEnumerable<Patient> FilterByDisease()
        {
            Console.WriteLine("\nВведите заболевание");
            string Disease = Console.ReadLine();

            var sortedPatients = _patients
                .Where(patient => patient.Disease.ToString().ToUpper() == Disease.ToUpper());

            return sortedPatients;
        }

        private IEnumerable<Patient> SortByAge() => _patients.OrderBy(patient => patient.Age);

        private IEnumerable<Patient> SortByFullName() => _patients.OrderBy(patient => patient.FullName);

        private void ShowSorted(IEnumerable<Patient> patients)
        {
            if(patients.Count() == 0)
            {
                Console.WriteLine("Таких пациентов нет");
                return;
            }

            Console.WriteLine("Отсортированные пациенты");
            foreach (var patient in patients)
                Console.WriteLine(patient.FullName + " " + patient.Age);
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
