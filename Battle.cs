using System;
using System.Buffers.Text;
using System.Reflection.Emit;
using Newtonsoft.Json;
using static SpartaDungeonBattle.Common;
using static SpartaDungeonBattle.Program;

namespace SpartaDungeonBattle
{
    internal class Battle
    {
        private static Random rand = new Random();
        private static List<Monster> battleMonsters = new List<Monster>();
        private static bool isAttack = false;
        private static int requireExp = 10;
        public static int baseHP = 0;
        public static int baseMP = 0;
        public static void DisplayBattle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!");
            Console.ResetColor();
            Console.WriteLine();
            PrintMonsters(isAttack);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHP}");
            Console.WriteLine($"MP {player.Mp}/{player.MaxMP}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1,2);
            switch (input)
            {
                case 1:
                    isAttack = true;
                    DisplayBattlePhase();
                    break;
            }
        }
        static void DisplayBattlePhase()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!");
            Console.ResetColor();
            Console.WriteLine();
            PrintMonsters(isAttack);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHP}");
            Console.WriteLine($"MP {player.Mp}/{player.MaxMP}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, battleMonsters.Count);
            switch (input)
            {
                case 0:
                    isAttack = false;
                    DisplayBattle();
                    break;
                default:
                    DisplayPlayerPhase(input-1);
                    break;

            }
        }
        static void DisplayPlayerPhase(int num)
        { 

            Monster monster = battleMonsters[num];
            int playerDamage = Damage();
            int critical = rand.Next(0, 101);
            string cri = "";
            if (monster.HP == 0)
            {
                DisplayBattlePhase();
                return;
            };
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 공격!");
            if (critical <= 15)
            {
                playerDamage = (int)(playerDamage * 1.6);
                cri = " - 치명타 공격!!";
            }
            else cri = "";
            Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]{cri}");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
            Console.Write($"HP {monster.HP} -> ");
            if (monster.HP - playerDamage > 0)
            {
                monster.HP -= playerDamage;
                Console.WriteLine(monster.HP);
            }
            else
            {
                monster.HP = 0;
                Console.WriteLine("Dead");
            }
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            int input = CheckValidInput(0,0);
            int sumHp = 0;
            foreach (Monster M in battleMonsters)
            {
                sumHp += M.HP;
            }
            if (input == 0)
            {
                if (sumHp > 0)
                {
                    DisplayEnemyPhase(0);
                }
                else DisplayResult();
            }
        }
        static void DisplayEnemyPhase(int num)
        {
            if (num < battleMonsters.Count)
            {
                Monster monster = battleMonsters[num];
                int monsterDamage = rand.Next(monster.Level, monster.Level * 3);

                if (monster.HP > 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Battle!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {monsterDamage}]");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    Console.Write($"HP {player.Hp} -> ");
                    if (player.Hp - monsterDamage > 0)
                    {
                        player.Hp -= monsterDamage;
                        Console.WriteLine(player.Hp);
                    }
                    else
                    {
                        player.Hp = 0;
                        DisplayResult();
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 다음");
                    Console.WriteLine();
                    int input = CheckValidInput(0, 0);
                    if (input == 0)
                    {
                        DisplayEnemyPhase(num + 1);
                    }
                }
                else DisplayEnemyPhase(num + 1);
            }
            else DisplayBattlePhase();
        }

        static void DisplayResult() {
            int baseExp = player.Exp;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!! - Result");
            Console.ResetColor();
            Console.WriteLine();
            if (player.Hp == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("You Lose");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {battleMonsters.Count}마리를 잡았습니다.");
                dungeonClear();
            }
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            if (player.Exp < requireExp)
            {
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
            }
            else
            {
                Console.Write($"Lv.{player.Level} {player.Name}");
                LevelUp();
                Console.WriteLine($"-> Lv.{player.Level} {player.Name}");
            }
            Console.WriteLine($"HP {baseHP} -> {player.Hp}");
            Console.Write($"Exp {baseExp} -> {player.Exp}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            int input = CheckValidInput(0, 0);
            if (input == 0)
            {
                if (player.Hp == 0)
                {
                    Environment.Exit(0);
                }
                else
                {
                    DisplayGameIntro();
                }
            }
        }

        public static void SpawnRandomMonsters()
        {
            int numberOfMonsters = rand.Next(1, player.DungeonFloor+2); // 1부터 (4 X 층수) 까지 랜덤한 수

            for (int i = 0; i < numberOfMonsters; i++)
            {
                int randomIndex = rand.Next(monsters.Count);
                Monster originalMonster = monsters[randomIndex];
                Monster clonedMonster = new Monster(originalMonster); // 복사 생성자를 사용하여 복제
                battleMonsters.Add(clonedMonster);
            }
        }
        static void PrintMonsters(bool Attack)
        {
            for (int i = 0; i < battleMonsters.Count; i++)
            {
                Monster monster = battleMonsters[i];
                string monsterNum = Attack ? $"{i + 1}. " : "";
                if (monster.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{monsterNum}{monster.Name}, Lv.{monster.Level} Dead");
                    Console.ResetColor();
                    continue;
                }
                Console.WriteLine($"{monsterNum}{monster.Name} Lv.{monster.Level} HP {monster.HP}");
            }

        }

        static int Damage()
        { 
            double eAtk = player.Atk * 0.1;
            int roundedeAtk = (int)Math.Ceiling(eAtk);
            int finalDamage = rand.Next((int)player.Atk - roundedeAtk, (int)player.Atk + roundedeAtk + 1);

            return finalDamage;
        }
        static void dungeonClear()
        {
            player.DungeonFloor++;
            player.Mp += 10;
            if(player.Mp > player.MaxMP) player.Mp = player.MaxMP;
            foreach (Monster monster in battleMonsters)
            {
                player.Exp += monster.Level * 1;
            }
            battleMonsters.Clear();
        }
        static void LevelUp() //플레이어 레벨업
        {
            int increase = 0;
            for(int i =0;i<=player.Level;i++)
            {
                increase += 5;
            }
            requireExp += increase;
            player.Level++;
            player.MaxHP += 5;
            player.MaxMP += 5;
            player.Hp = player.MaxHP;
            player.Mp = player.MaxMP;
            player.Atk += 0.5;
            player.Def += 1;
        }
    }
}
