using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    internal class Program
    {
        private const string TakeCardCommand = "1";
        private const string TakeCardsCommand = "2";
        private const string ExitCommand = "3";

        private static void Main(string[] args)
        {
            bool isProgramRunning = true;
            int deckSize = 52;

            Deck deck = GetRandomlyFilledDeck(deckSize);
            Player player = new Player();

            while (isProgramRunning)
            {
                ShowMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case TakeCardCommand:
                        TakeCard(player, deck);
                        break;

                    case TakeCardsCommand:
                        TakeCards(player, deck);
                        break;

                    case ExitCommand:
                        isProgramRunning = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует");
                        break;
                }
            }

            player.ShowAllCards();
        }

        private static void TakeCards(Player player, Deck deck)
        {
            Console.WriteLine($"Сколько карт вы хотите взять? (в колоде осталось {deck.CurrentCount} карт)");

            if(uint.TryParse(Console.ReadLine(), out uint amount))
            {
                if (deck.TryGet((int)amount, out List<Card> cards))
                {
                    player.AddCards(cards);
                    Console.WriteLine("Вы успешно взяли карты");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Введите неотрицательное число");
            }

            Console.WriteLine("Не получилось взять карты");
        }

        private static void TakeCard(Player player, Deck deck)
        {
            if(deck.TryGet(out Card card))
            {
                player.AddCard(card);
                Console.WriteLine("Вы успешно взяли карту");
                return;
            }

            Console.WriteLine("Не получилось взять карту");
        }

        private static Deck GetRandomlyFilledDeck(int size)
        {
            Deck deck = new Deck(size);

            Random random = new Random();   

            while (deck.IsHasPlace)
            {
                int randomCardNumber = random.Next(Card.MinNumber, Card.MaxNumber);
                CardColor randomCardColor = (CardColor)random.Next(Enum.GetValues(typeof(CardColor)).Length);

                deck.TryAdd(new Card(randomCardNumber, randomCardColor));
            }

            return deck;
        }

        private static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine($"Чтобы взять одну карту введите - {TakeCardCommand}");
            Console.WriteLine($"Чтобы взять несколько карт введите - {TakeCardsCommand}");
            Console.WriteLine($"Чтобы прекратить брать карты - {ExitCommand}");
            Console.WriteLine();
        }
    }

    public enum CardColor
    {
        yellow = 0,
        green,
        red,
        black
    }

    public class Card
    {
        public const int MaxNumber = 10;
        public const int MinNumber = 0;

        private int _number;
        private CardColor _color;

        public Card(int number, CardColor color)
        {
            if (number > MaxNumber || number < MinNumber)
                throw new ArgumentOutOfRangeException(nameof(number));

            _number = number;
            _color = color;
        }

        public void ShowInfo() => Console.WriteLine($"Номер - {_number}, цвет - {_color}");
    }

    public class Deck
    {
        private List<Card> _cards;
        private int _size;

        public int CurrentCount => _cards.Count;
        public bool IsHasPlace => _cards.Count + 1 <= _size;

        public Deck(List<Card> cards)
        {
            if(cards == null)
                throw new ArgumentNullException(nameof(cards));

            _cards = new List<Card>(cards);
            _size = cards.Count;
        }

        public Deck(int size)
        {
            if(size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            _cards = new List<Card>();
            _size = size;
        }

        public bool TryAdd(Card card)
        {
            if (_cards.Count + 1 > _size)
            {
                Console.WriteLine("В колоде максимальное количество карт");
                return false;
            }

            _cards.Add(card);
            return true;
        }

        public bool TryGet(out Card card)
        {
            if (_cards.Count == 0)
            {
                ShowCardsIsOverMessage();
                card = null;
                return false;
            }

            card = _cards[_cards.Count - 1];
            _cards.RemoveAt(_cards.Count - 1);
            return true;
        }

        public bool TryGet(int cardsAmount, out List<Card> cards)
        {
            if (_cards.Count < cardsAmount)
            {
                ShowCardsIsOverMessage();
                cards = null;
                return false;
            }

            cards = _cards.GetRange(_cards.Count - cardsAmount, cardsAmount);
            _cards.RemoveRange(_cards.Count - cardsAmount, cardsAmount);
            return true;
        }

        private void ShowCardsIsOverMessage() => Console.WriteLine("Не хватает карт в колоде");
    }

    public class Player
    {
        private List<Card> _cards = new List<Card>();

        public void AddCard(Card card) => _cards.Add(card);

        public void AddCards(List<Card> cards) => _cards.AddRange(cards);

        public void ShowAllCards()
        {
            if(_cards.Count == 0)
            {
                Console.WriteLine("Карт нет");
                return;
            }

            foreach(Card card in _cards)
                card.ShowInfo();
        }
    }
}
