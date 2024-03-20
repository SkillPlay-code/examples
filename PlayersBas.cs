using System;
using System.Collections.Generic;
 
namespace SkillPlay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProgramMenu programMenu = new ProgramMenu();
            programMenu.Launch();
        }
    }
 
    class Player
    {
        private static int _counter;
 
        public Player(string name, int level)
        {
            NickName = name;
            Level = level;
            IsBan = false;
            AddId();
        }
 
        public int NumberId { get; private set; }
        public string NickName { get; private set; }
        public int Level { get; private set; }
        public bool IsBan { get; private set; }
 
        public void Ban()
        {
            IsBan = true;
        }
 
        public void Unban()
        {
            IsBan = false;
        }
 
        public void ShowInfo()
        {
            string status;
 
            if (IsBan == true)
            {
                status = "Да";
            }
            else 
            {
                status = "Нет";
            }
 
            Console.WriteLine($" Номер игрока: {NumberId}\n" +
                              $" Имя игрока: {NickName}\n" +
                              $" Уровень игрока: {Level}\n" +
                              $" Бан: {status}");
        }
 
        private void AddId()
        {
            NumberId = ++_counter;
        }
    }
 
    class Database
    {
        private List<Player> _players = new List<Player>();
 
        public void AddPlayer()
        {
            Console.WriteLine("Введите имя игрока");
            string name = Console.ReadLine();
 
            Console.WriteLine("Введите уровень игрока");
            int level = ReadInt(Console.ReadLine());
 
            _players.Add(new Player(name, level));
        }
 
        public void RemovePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
            }
            else
            {
                Console.WriteLine("Такого игрока нет в базе данных");
            }
        }
 
        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
            }
            else
            {
                Console.WriteLine("Такого игрока нет в базе данных");
            }
        }
 
        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
            }
            else
            {
                Console.WriteLine("Такого игрока нет в базе данных");
            }
        }
 
        public void ShowAllPlayers()
        {
            if (_players.Count > 0)
            {
                foreach (Player player in _players)
                {
                    player.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("База данных пока что пуста:");
            }
        }
 
        public void ShowAllBannedPlayers()
        {
            if (_players.Count > 0)
            {
                foreach (Player player in _players)
                {
                    if (player.IsBan == true)
                    {
                        player.ShowInfo();
                    }
                }
            }
            else
            {
                Console.WriteLine("Забаненных игроков нет");
            }
        }
 
        private bool TryGetPlayer(out Player player)
        {
            player = null;
 
            Console.WriteLine("Введите номер игрока для продолжения операции");
 
            int userInput = ReadInt(Console.ReadLine());
 
            for (int i = 0; i < _players.Count; i++)
            {
                if (userInput == _players[i].NumberId)
                {
                    player = _players[i];
                    return true;
                }
            }
 
            return false;
        }
 
        private int ReadInt(string userInput)
        {
            int numberId = 0;
            bool isWork = true;
 
            while (isWork)
            {
                bool success = int.TryParse(userInput, out numberId);
 
                if (success)
                {
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз");
                    userInput = Console.ReadLine();
                }
            }
 
            return numberId;
        }
    }
 
    class ProgramMenu
    {
        public void Launch()
        {
            Database database = new Database();
 
            const string CommandAddPlayer = "1";
            const string CommandRemovePlayer = "2";
            const string CommandBanPlayer = "3";
            const string CommandUnbanPlayer = "4";
            const string CommandShowAllPlayers = "5";
            const string CommandShowBanPlayers = "6";
            const string ExitProgramm = "7";
 
            bool isWork = true;
 
            while (isWork)
            {
                Console.WriteLine("Меню: \n" +
                                  $"Добавить игрока - {CommandAddPlayer} \n" +
                                  $"Удалить игрока - {CommandRemovePlayer} \n" +
                                  $"Забанить игрока - {CommandBanPlayer} \n" +
                                  $"Разбанить игрока - {CommandUnbanPlayer} \n" +
                                  $"Список всех игроков - {CommandShowAllPlayers} \n" +
                                  $"Показать всех забаненных игроков - {CommandShowBanPlayers} \n" +
                                  $"Завершить работу - {ExitProgramm}");
 
                string userInput = Console.ReadLine();
 
                switch (userInput)
                {
                    case CommandAddPlayer:
                        database.AddPlayer();
                        break;
 
                    case CommandRemovePlayer:
                        database.RemovePlayer();
                        break;
 
                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;
 
                    case CommandUnbanPlayer:
                        database.UnbanPlayer();
                        break;
 
                    case CommandShowAllPlayers:
                        database.ShowAllPlayers();
                        break;
 
                    case CommandShowBanPlayers:
                        database.ShowAllBannedPlayers();
                        break;
 
                    case ExitProgramm:
                        isWork = false;
                        break;
 
                    default:
                        Console.WriteLine("Ошибка, не правильный ввод!");
                        break;
                }
 
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
