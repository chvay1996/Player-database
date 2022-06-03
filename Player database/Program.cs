using System;
using System.Collections.Generic;

namespace Player_database
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Work();
        }
    }

    class Player
    {
        private string _nickName;
        private int _lvl;
        public bool IsBanned { get; private set; }

        public Player(string nickName, int lvl)
        {
            _nickName = nickName;

            if (lvl > 0 && lvl < 100)
            {
                _lvl = lvl;
            }
            else
            {
                _lvl = 1;
                IsBanned = false;
            }
        }
        public void Unban()
        {
            IsBanned = false;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Ник персонажа - {_nickName}, его уровень - {_lvl}, статус бана - {IsBanned}");
        }
    }

    class Server
    {
        private bool _isServerWork = true;
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            string[] menu = { "Добавить игрока", "Разбанить игрока", "Заблокировать игрока", "Удалить игрока", "Выход" };
            int index = 0;

            while (_isServerWork)
            {
                Console.SetCursorPosition(0, 0);
                Console.ResetColor();
                Console.WriteLine("\t\tМеню");

                for (int i = 0; i < menu.Length; i++)
                {
                    if (index == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo userInput = Console.ReadKey(true);

                switch (userInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index != 0) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index != menu.Length - 1) index++;
                        break;
                    case ConsoleKey.Enter:
                        ButtonControl(index);
                        break;
                }
            }
        }

        private void ButtonControl(int index)
        {
            switch (index)
            {
                case 0:
                    AddPlayer();
                    break;
                case 1:
                    LockPlaywr();
                    break;
                case 2:
                    UnlockPlayer();
                    break;
                case 3:
                    DeletePlayer();
                    break;
                case 4:
                    Exit();
                    break;
            }
        }

        private void DeletePlayer()
        {
            string userInput;
            int checkingForANumber;
            bool isStringNumber;

            if (_players.Count > 0)
            {
                ShowDataServer();
                Console.Write("Что бы удалить игрока, ведите его номер: ");

                isStringNumber = CheckString(out userInput, out checkingForANumber);

                if (isStringNumber)
                {
                    _players.RemoveAt(checkingForANumber - 1);
                }
                else
                {
                    Console.WriteLine("Данные не корректны");
                }
            }
            else Console.WriteLine("Сервер пустой!");

            Clear();
        }

        private void LockPlaywr()
        {
            string userInput;
            int checkingForANumber;
            bool isConsoleBanActive;

            if (_players.Count > 0)
            {
                ShowDataServer();
                Console.Write("Что бы разблокировать игрока, введите его номер: ");

                isConsoleBanActive = CheckString(out userInput, out checkingForANumber);

                if (isConsoleBanActive)
                {
                    if (isConsoleBanActive)
                    {
                        if (_players[checkingForANumber - 1].IsBanned == true)
                        {
                            _players[checkingForANumber - 1].Unban();
                            _players[checkingForANumber - 1].ShowDetails();
                        }
                        else Console.WriteLine("Игрок разаблокирован");
                    }
                    else
                    {
                        Console.WriteLine("Данные не корректны");
                    }
                }
                else Console.WriteLine("Сервер пустой!");

                Clear();
            }

        }

        private void UnlockPlayer()
        {
            string userInput;
            int checkingForANumber;
            bool isConsoleBanActive;

            if (_players.Count > 0)
            {
                ShowDataServer();
                Console.Write("Что бы заблокировать игрока, введите его номер: ");

                isConsoleBanActive = CheckString(out userInput, out checkingForANumber);

                if (isConsoleBanActive)
                {
                    if (isConsoleBanActive)
                    {
                        if (_players[checkingForANumber - 1].IsBanned == false)
                        {
                            _players[checkingForANumber - 1].Ban();
                            _players[checkingForANumber - 1].ShowDetails();
                        }
                        else Console.WriteLine("Игрок заблокирован");
                    }
                    else
                    {
                        Console.WriteLine("Данные не корректны");
                    }
                }
                else Console.WriteLine("Сервер пустой!");

                Clear();
            }
        }

        private void ShowDataServer()
        {
            Console.WriteLine("Персонажи на сервере");

            for (int i = 0; i < _players.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _players[i].ShowDetails();
            }
        }

        private void AddPlayer()
        {
            string nickName;
            string lvl;
            int checkingForANumber;
            bool isStringName;

            Console.Write("Введите Ник игрока: ");
            nickName = Convert.ToString(Console.ReadLine());

            Console.Write("Введите LVL игрока: ");
            isStringName = CheckString(out lvl, out checkingForANumber);

            if (isStringName)
            {
                _players.Add(new Player(nickName, checkingForANumber));
                Console.WriteLine($"Вы добавели игрока {nickName} - {checkingForANumber} LVL");
            }
            else
            {
                Console.WriteLine("Введите коректные данные!");

            }

            Clear();
        }

        private bool CheckString(out string userInput, out int result)
        {
            userInput = " ";
            result = 0;
            bool isStringNumber;

            userInput = Console.ReadLine();
            isStringNumber = int.TryParse(userInput, out result);
            return isStringNumber;
        }

        private void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }

        private void Clear()
        {
            byte indentDown = 5;
            byte cleaningTheConsole = 15;
            Console.SetCursorPosition(0, indentDown);
            Console.ReadKey();

            for (int i = 0; i < cleaningTheConsole; i++)
            {
                Console.WriteLine("\t\t\t\t\t\t\t\t\t");
            }
        }
    }
}
/*Задача:
Реализовать базу данных игроков и методы для работы с ней.
У игрока может быть порядковый номер, ник, уровень, флаг – забанен ли он(флаг - bool).
Реализовать возможность добавления игрока, бана игрока по порядковому номеру, разбана игрока по порядковому номеру и удаление игрока.*/