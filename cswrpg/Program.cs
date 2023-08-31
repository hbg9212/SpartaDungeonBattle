using System;
using System.Collections.Generic;

namespace DungeonGame
{
    class Program
    {
        static Character player;
        static List<Monster> monsters;
        static Random rand = new Random();

        static void Main(string[] args)
        {
            InitGame();
            MainMenu();
        }

        static void InitGame()
        {
            player = new Character
            {
                Level = 1,
                Name = "Chad",
                Job = "전사",
                Attack = 10,
                Defense = 5,
                HP = 100,
                MaxHP = 100,
                Gold = 1500
            };
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowStatus();
                        break;
                    case "2":
                        StartBattle();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("** 상태 보기 **");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ({player.Job})");
            Console.WriteLine($"공격력 : {player.Attack}");
            Console.WriteLine($"방어력 : {player.Defense}");
            Console.WriteLine($"체력 : {player.HP}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine("0. 나가기");
            Console.ReadLine();
        }

        static void StartBattle()
        {
            GenerateMonsters();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("** Battle!! **");

                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {monsters[i].Name}  HP {monsters[i].HP}");
                }

                Console.WriteLine($"[내 정보]\nLv.{player.Level}  {player.Name} (전사)\nHP {player.HP}/{player.MaxHP}");
                Console.WriteLine("1. 공격");
                Console.Write("원하시는 행동을 입력해주세요: ");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    PlayerAttackPhase();
                    EnemyPhase();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        static void GenerateMonsters()
        {
            monsters = new List<Monster>();
            int monsterCount = rand.Next(1, 5);

            for (int i = 0; i < monsterCount; i++)
            {
                int monsterType = rand.Next(0, 3);

                Monster monster = null;
                switch (monsterType)
                {
                    case 0:
                        monster = new Monster { Name = "미니언", Level = 2, HP = 15, Attack = 5 };
                        break;
                    case 1:
                        monster = new Monster { Name = "공허충", Level = 3, HP = 10, Attack = 9 };
                        break;
                    case 2:
                        monster = new Monster { Name = "대포미니언", Level = 5, HP = 25, Attack = 8 };
                        break;
                }
                monsters.Add(monster);
            }
        }

        static void PlayerAttackPhase()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                int dmg = player.Attack - rand.Next(0, 3);
                monsters[i].HP -= dmg;
                Console.WriteLine($"{monsters[i].Name}에게 {dmg}의 피해를 입혔습니다!");
                if (monsters[i].HP <= 0)
                {
                    Console.WriteLine($"{monsters[i].Name}을 처치했습니다!");
                    monsters.RemoveAt(i);
                    i--;
                }
            }
        }

        static void EnemyPhase()
        {
            foreach (var monster in monsters)
            {
                int dmg = monster.Attack - rand.Next(0, 3);
                player.HP -= dmg;
                Console.WriteLine($"{monster.Name}에게 {dmg}의 피해를 받았습니다!");
                if (player.HP <= 0)
                {
                    Console.WriteLine("당신은 쓰러졌습니다...");
                    Environment.Exit(0);
                }
            }
        }

    }

    public class Character
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Gold { get; set; }
    }

    public class Monster
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
    }
}
