using System;
using System.Buffers.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using Newtonsoft.Json;
using static SpartaDungeonBattle.Common;
using static SpartaDungeonBattle.Program;

namespace SpartaDungeonBattle
{
    internal class Battle
    {
        private static List<Monster> battleMonsters = new List<Monster>();
        private static bool isAttack = false;
        private static int requireExp = 10;
        public static int baseHP = 0;
        public static int baseMP = 0;
        public static void DisplayBattle()
        {
            initSkill();
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
                case 2:
                    DisplaySkillPhase();
                    break;
            }
        }
        static void DisplayBattlePhase()
        {
            int avoid = rand.Next(0, 101);
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
                    if (avoid <= 10) DisplayAvoid(input - 1);
                    else DisplayPlayerPhase(input-1,0);
                    break;

            }
        }
        static void DisplaySkillPhase()
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
            int index = 1;
            for(int i = 1;i<player.Skills.Count;i++)
            {
                Skill skill = player.Skills[i];
                Console.WriteLine($"{index}. {skill.Name} - MP {skill.MPCost}");
                Console.WriteLine($" {skill.Description}");
                index++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 취소");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, player.Skills.Count);
            switch (input)
            {
                case 0:
                    DisplayBattle();
                    break;
                default:
                    if (player.Mp - player.Skills[input].MPCost >= 0)
                    {
                        if (player.Skills[input].isMunti) DisplayPlayerPhase(input, input);
                        else
                        {
                            isAttack = true;
                            DisplayPlayerSkillPhase(input);
                        }
                    }
                    else
                    {
                        Console.WriteLine("MP가 부족합니다!");
                        Thread.Sleep(1000);
                        DisplaySkillPhase();
                    }
                    break;

            }
        }
        static void DisplayAvoid(int num)
        {
            Monster monster = battleMonsters[num];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            int input = CheckValidInput(0, 0);
            if(input == 0)
            {
                DisplayEnemyPhase(0);
            }
        }
        static void DisplayPlayerPhase(int num, int skillnum)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 공격!");
            if (player.Skills[skillnum].isMunti)
            {
                player.Skills[skillnum].MultipleAction(player, battleMonsters);
            }
            else
            {
                Monster monster = battleMonsters[num];
                if (monster.HP == 0)
                {
                    DisplayBattlePhase();
                    return;
                };
                player.Skills[skillnum].SingleAction(player, monster);
            }
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            int input = CheckValidInput(0, 0);
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
        static void DisplayPlayerSkillPhase(int num)
        {
         
            Skill skill= player.Skills[num];         
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
            Console.WriteLine($"현재 스킬 : {skill.Name} - MP {skill.MPCost}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 취소");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, battleMonsters.Count);
            switch (input)
            {
                case 0:
                    isAttack = false;
                    DisplaySkillPhase();
                    break;
                default:
                    DisplayPlayerPhase(input - 1,num);
                    break;

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
            else
            {
                isAttack = false;
                DisplayBattle();
            }
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
            Console.WriteLine("[획득 아이템]");
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
        static void initSkill()
        {
            if (player.Skills.Count == 0)
            {
                if (player.Job == "전사")
                {
                    player.Skills = jobs[0].Skills;
                }
                else
                {
                    player.Skills = jobs[1].Skills;
                }
            }
        }
    }
}
