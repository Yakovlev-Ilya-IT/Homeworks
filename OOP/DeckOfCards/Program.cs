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
            deck.Shuffle();

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
        Hearts = 0,
        Spades,
        Diamonds,
        Clubs
    }

    public enum CardNames
    {
        Six = 0,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card
    {
        private CardSuits _suit;
        private CardNames _name;

        private Dictionary<CardNames, string> _namesTranslation = new Dictionary<CardNames, string>()
        {
            {CardNames.Six, "Шестерка" },
            {CardNames.Seven, "Семерка" },
            {CardNames.Eight, "Восьмерка" },
            {CardNames.Nine, "Девятка" },
            {CardNames.Ten, "Десятка" },
            {CardNames.Jack, "Валет" },
            {CardNames.Queen, "Дама" },
            {CardNames.King, "Король" },
            {CardNames.Ace, "Туз" }
        };

        private Dictionary<CardSuits, string> _suitsTranslation = new Dictionary<CardSuits, string>()
        {
            {CardSuits.Hearts, "черви"},
            {CardSuits.Spades, "пики" },
            {CardSuits.Diamonds, "бубны" },
            {CardSuits.Clubs, "трефы" }
        };

        private Dictionary<CardNames, int> _values = new Dictionary<CardNames, int>()
        {
            {CardNames.Six, 6 },
            {CardNames.Seven, 7 },
            {CardNames.Eight, 8 },
            {CardNames.Nine, 9 },
            {CardNames.Ten, 10 },
            {CardNames.Jack, 2 },
            {CardNames.Queen, 3 },
            {CardNames.King, 4 },
            {CardNames.Ace, 11 }
        };

        public Card(CardNames name, CardSuits suit)
        {
            _name = name;
            _suit = suit;
            Value = _values[_name];
        }

        public int Value { get; }

        public void ShowInfo() => Console.WriteLine($"Название - {_namesTranslation[_name]}, масть - {_suitsTranslation[_suit]}");
    }

    public class Deck
    {
        private List<Card> _cards;

        public int CurrentCount => _cards.Count;

        public Deck()
        {
            _cards = new List<Card>();
            int cardNamesNumber = Enum.GetValues(typeof(CardNames)).Length;
            int cardSuitsNumber = Enum.GetValues(typeof(CardSuits)).Length;

            for (int i = 0; i < cardNamesNumber; i++)
            {
                for (int j = 0; j < cardSuitsNumber; j++)
                {
                    _cards.Add(new Card((CardNames)i, (CardSuits)j));
                }
            }
        }

        public Card Get()
        {
            Card takenCard = _cards[_cards.Count - 1];
            _cards.RemoveAt(_cards.Count - 1);
            return takenCard;
        }

        public List<Card> Get(int cardsAmount)
        {
            List<Card> takenCards = _cards.GetRange(_cards.Count - cardsAmount, cardsAmount);
            _cards.RemoveRange(_cards.Count - cardsAmount, cardsAmount);
            return takenCards;
        }

        public void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndexForTemp = random.Next(_cards.Count);
                int randomIndexForSource = random.Next(_cards.Count);

                Card temp = _cards[randomIndexForTemp];
                _cards[randomIndexForTemp] = _cards[randomIndexForSource];
                _cards[randomIndexForSource] = temp;
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
