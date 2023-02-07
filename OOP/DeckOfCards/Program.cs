using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Player player = new Player();
            Casino casino = new Casino(player);

            casino.Work();
        }
    }

    public class Casino
    {
        private const string TakeCardCommand = "1";
        private const string TakeCardsCommand = "2";
        private const string CheckCardsCommand = "3";
        private const string ExitCommand = "4";

        private const int MaxScore = 21;

        private Player _player;

        public Casino(Player player)
        {
            _player = player;
        }

        public void Work()
        {
            Deck deck = new Deck();

            Console.WriteLine("Добро пожаловать в наше казино, помните что вам нельзя набрать больше 21 очка!");

            bool isWork = true;

            while (isWork)
            {
                ShowMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case TakeCardCommand:
                        TakeCard(deck);
                        break;

                    case TakeCardsCommand:
                        TakeCards(deck);
                        break;

                    case CheckCardsCommand:
                        ShowCards();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует");
                        break;
                }
            }

            if(CheckVictory())
                Console.WriteLine("Поздравляю, вы победили!");
            else
                Console.WriteLine("О нет! у вас перебор(");
        }

        private void TakeCards(Deck deck)
        {
            Console.WriteLine($"Сколько карт вы хотите взять? (в колоде осталось {deck.CurrentCount} карт)");

            if (uint.TryParse(Console.ReadLine(), out uint amount))
            {
                if(deck.CurrentCount < amount)
                {
                    ShowNotEnoughCardsMessage();
                    return;
                }

                _player.AddCards(deck.Get((int) amount));

                Console.WriteLine("Вы успешно взяли карты");
                return;
            }

            Console.WriteLine("Введите неотрицательное число");
        }

        private void TakeCard(Deck deck)
        {
            if (deck.CurrentCount == 0)
            {
                ShowNotEnoughCardsMessage();
                return;
            }

            _player.AddCard(deck.Get());

            Console.WriteLine("Вы успешно взяли карту");
        }

        private void ShowCards() => _player.ShowInformation();

        private bool CheckVictory() => _player.GetScore() <= MaxScore;

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Введите одну из следующих команд:");
            Console.WriteLine($"Чтобы взять одну карту введите - {TakeCardCommand}");
            Console.WriteLine($"Чтобы взять несколько карт введите - {TakeCardsCommand}");
            Console.WriteLine($"Чтобы посмотреть свои карты - {CheckCardsCommand}");
            Console.WriteLine($"Чтобы закончить добор карт - {ExitCommand}");
            Console.WriteLine();
        }

        private void ShowNotEnoughCardsMessage() => Console.WriteLine("Не хватает карт в колоде");
    }

    public enum CardSuits
    {
        Черви = 0,
        Пики,
        Бубны,
        Трефы
    }

    public enum CardCosts
    {
        Шестерка = 6,
        Семерка = 7,
        Восьмерка = 8,
        Девятка = 9,
        Десятка = 10,
        Валет = 2,
        Дама = 3,
        Король = 4,
        Туз = 11
    }

    public class Card
    {
        private CardSuits _suit;
        private CardCosts _cost;

        public Card(CardCosts cost, CardSuits suit)
        {
            _cost = cost;
            _suit = suit;
        }

        public int Value => (int)_cost;

        public void ShowInfo() => Console.WriteLine($"Название - {_cost}, масть - {_suit}");
    }

    public class Deck
    {
        private Stack<Card> _cards;

        public Deck()
        {
            List<Card> cards = new List<Card>();

            foreach (CardCosts cost in Enum.GetValues(typeof(CardCosts)))
                foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
                    cards.Add(new Card(cost, suit));

            Shuffle(cards);
            _cards = new Stack<Card>(cards);
        }

        public int CurrentCount => _cards.Count;

        public Card Get() => _cards.Pop();

        public List<Card> Get(int cardsAmount)
        {
            List<Card> takenCards = new List<Card>();

            for (int i = 0; i < cardsAmount; i++)
                takenCards.Add(_cards.Pop());

            return takenCards;
        }

        private void Shuffle(List<Card> cards)
        {
            Random random = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int randomIndexForTemp = random.Next(cards.Count);
                int randomIndexForSource = random.Next(cards.Count);

                Card temp = cards[randomIndexForTemp];
                cards[randomIndexForTemp] = cards[randomIndexForSource];
                cards[randomIndexForSource] = temp;
            }
        }
    }

    public class Player
    {
        private List<Card> _cards = new List<Card>();

        public void AddCard(Card card) => _cards.Add(card);

        public void AddCards(List<Card> cards) => _cards.AddRange(cards);

        public void ShowInformation()
        {
            if(_cards.Count == 0)
            {
                Console.WriteLine("Карт нет");
                return;
            }


            foreach(Card card in _cards)
                card.ShowInfo();

            Console.WriteLine($"Ваш текущий счет {GetScore()}");
        }

        public int GetScore()
        {
            int currentScore = 0;

            foreach (Card card in _cards)
                currentScore += card.Value;

            return currentScore;
        }
    }
}
