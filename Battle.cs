using System;
using static SpartaDungeonBattle.CharacterInfo;
using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    class Battle
    {

        public class Monster
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int Hp { get; set; }
            public int Atk { get; set; }
            public bool IsLive => Hp > 0;
            public Monster(string name, int level, int hp, int atk)
            {
                Name = name;
                Level = level;
                Hp = hp;
                Atk = atk;
            }
        }

        public static List<Monster> monsterList = new List<Monster>();

        /// <summary>확률 계산 메소드</summary>
        public static bool Probability(float loss)
        {
            Random rand = new Random();
            // 0 : 실패, 1 : 성공
            int ran = rand.Next(0, 101);
            float[] probs = { 100 - loss, loss };

            float cumulative = 0f;
            int target = -1;
            for (int i = 0; i < 2; i++)
            {
                cumulative += probs[i];
                if (ran <= cumulative)
                {
                    target = 1 - i;
                    break;
                }
            }
            return target > 0 ? true : false;
        }

        /// <summary>Monst List 세팅 메소드</summary>
        public static void SetMonsterList()
        {
            monsterList.Clear();

            Random rand = new Random();
            // monsters에서 monster 랜덤 뽑기
            for (int i = 0; i < rand.Next(1, 4); i++)
            {
                int monsterNum = rand.Next(0, 3);
                Monster monster;
                switch (monsterNum)
                {
                    case 0:
                        monster = new Monster("미니언", 2, 15, 5);
                        monsterList.Add(monster);
                        break;
                    case 1:
                        monster = new Monster("대포미니언", 5, 25, 10);
                        monsterList.Add(monster);
                        break;
                    case 2:
                        monster = new Monster("공허충", 3, 10, 10);
                        monsterList.Add(monster);
                        break;
                    case 3:
                        monster = new Monster("슈펴미니언", 10, 30, 15);
                        monsterList.Add(monster);
                        break;
                }
            }

            DisplayBattle();
        }

        /// <summary>전투 시작 화면 출력</summary>
        public static void DisplayBattle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!");
            Console.ResetColor();
            Console.WriteLine();
            foreach (Monster monster in monsterList)
            {
                if(monster.IsLive)
                {
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} HP {monster.Hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} Dead");
                    Console.ResetColor();
                }
               
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine($"MP {player.Mp}/50");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayBattleAttack("");
                    break;
                case 2:
                    DisplaySkill(); 
                    break;
            }
        }

        /// <summary>전투 공격 화면 출력</summary>
        public static void DisplayBattleAttack(string msg)
        {
            int num = 1;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Monster monster in monsterList)
            {
                if (monster.IsLive)
                {
                    Console.WriteLine($"{num++} Lv.{monster.Level} {monster.Name} HP {monster.Hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{num++} Lv.{monster.Level} {monster.Name} Dead");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine($"MP {player.Mp}/50");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 취소");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            Console.WriteLine(msg);
            int input = CheckValidInput(0, monsterList.Count);
            switch (input)
            {
                case 0:
                    DisplayBattle();
                    break;
                default:
                    Attack(input);
                    break;
            }
        }

        /// <summary>전투 공격</summary>
        private static void Attack(int index)
        {
            index--;
            if (monsterList[index].IsLive)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"{player.Name} 의 공격!");
                bool IsHit = Probability(10.0f);

                if (IsHit) 
                {
                    bool IsCritical = Probability(85.0f);

                    // 데미지 계산
                    Random rand = new Random();
                    int atk = player.Atk + myAddStat[(int)Abilitys.공격력];
                    int error = (int)(atk * 0.1);
                    int damage = rand.Next(atk - error, atk + error);

                    if (IsCritical) damage = (int)(damage * 1.6f);

                    Console.Write($"Lv.{monsterList[index].Level} {monsterList[index].Name} 을(를) 맞췄습니다. [데미지: {damage}]");
                    if (IsCritical) Console.WriteLine(" - 치명타 공격!!");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{monsterList[index].Level} {monsterList[index].Name}");
                    if (monsterList[index].Hp - damage > 0)
                    {
                        Console.WriteLine($"HP {monsterList[index].Hp}->{monsterList[index].Hp - damage}");
                        monsterList[index].Hp -= damage;
                    }
                    else
                    {
                        Console.WriteLine($"HP {monsterList[index].Hp}->Dead");
                        monsterList[index].Hp = 0;
                    }
                }
                else
                {
                    Console.WriteLine($"Lv.{monsterList[index].Level} {monsterList[index].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        Defend(0);
                        break;
                }
            }
            else 
            {
                DisplayBattleAttack("잘못된 입력입니다.");
            }
        }

        /// <summary>전투 스킬 화면 출력</summary>
        public static void DisplaySkill()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Monster monster in monsterList)
            {
                if (monster.IsLive)
                {
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} HP {monster.Hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} Dead");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine($"MP {player.Mp}/50");
            Console.WriteLine();
            Skill[] skills = SkillSet[(int)player.Job];
            for (int i = 0; i < skills.Length; i++)
            {
                Console.WriteLine($"{i+1}. {skills[i].Name} - MP {skills[i].Mp}");
                Console.WriteLine($"   {skills[i].Described}");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 취소");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            int input = CheckValidInput(0, skills.Length);
            switch (input)
            {
                case 0:
                    DisplayBattle();
                    break;
                default:
                    DisplaySkillAttack(input,"");
                    break;
            }
        }

        /// <summary>전투 스킬 공격 화면 출력</summary>
        public static void DisplaySkillAttack(int index, string msg)
        {
            --index;
            // 대상 선택 필요 여부
            Skill[] skills = SkillSet[(int)player.Job];
            if (skills[index].Count == 1)
            {

                int num = 1;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("[대상 선택]");
                Console.WriteLine();
                foreach (Monster monster in monsterList)
                {
                    if (monster.IsLive)
                    {
                        Console.WriteLine($"{num++} Lv.{monster.Level} {monster.Name} HP {monster.Hp}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{num++} Lv.{monster.Level} {monster.Name} Dead");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/100");
                Console.WriteLine($"MP {player.Mp}/50");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0. 취소");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("대상을 선택해주세요.");
                Console.WriteLine(msg);
                int input = CheckValidInput(0, monsterList.Count);
                switch (input)
                {
                    case 0:
                        DisplayBattle();
                        break;
                    default:
                        SkillAttack(index, input);
                        break;
                }
            }
            else
            {
                SkillAttack(index, -1);
            }
        }

        /// <summary>스킬 공격</summary>
        private static void SkillAttack(int index, int input)
        {
            Skill[] skills = SkillSet[(int)player.Job];
            if ( input > 0)
            {
                input--;
                if (monsterList[input].IsLive)
                {
                    //마나 소비
                    player.Mp -= skills[index].Mp;
                    
                    // 데미지 계산
                    Random rand = new Random();
                    int atk = player.Atk + myAddStat[(int)Abilitys.공격력];
                    int error = (int)(atk * 0.1);
                    int damage = rand.Next(atk - error, atk + error);
                    damage = (int)(damage * skills[index].Damage);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Battle!!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine($"{player.Name} 의 {skills[index].Name}");
                    Console.WriteLine($"Lv.{monsterList[input].Level} {monsterList[input].Name} 을(를) 맞췄습니다. [데미지: {damage}]");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{monsterList[input].Level} {monsterList[input].Name}");
                    if (monsterList[input].Hp - damage > 0)
                    {
                        Console.WriteLine($"HP {monsterList[input].Hp}->{monsterList[input].Hp - damage}");
                        monsterList[input].Hp -= damage;
                    }
                    else
                    {
                        Console.WriteLine($"HP {monsterList[input].Hp}->Dead");
                        monsterList[input].Hp = 0;
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 다음");
                    Console.WriteLine();
                    int i = CheckValidInput(0, 0);
                    switch (i)
                    {
                        case 0:
                            Defend(0);
                            break;
                    }
                }
                else
                {
                    DisplayBattleAttack("잘못된 입력입니다.");
                }
            }
            else
            {

            }
        }


        /// <summary>전투 방어</summary>
        private static void Defend(int index)
        {
            //플레이어 사망 여부
            if(player.Hp == 0)
            {
                //사망
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("You Lose");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                }
            }
            else
            {
                //몬스터 유무
                if(index == monsterList.Count)
                {
                    DisplayBattle();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Battle!!");
                    Console.ResetColor();
                    Console.WriteLine();
                    //몬스터 사망 우유
                    if (monsterList[index].IsLive)
                    {

                        bool IsHit = Probability(10.0f);
                        Console.WriteLine($"Lv.{monsterList[index].Level} {monsterList[index].Name} 의 공격!");
                        if (IsHit)
                        {
                            bool IsCritical = Probability(85.0f);
                            // 데미지 계산
                            Random rand = new Random();
                            int atk = monsterList[index].Atk;
                            int error = (int)(atk * 0.1);
                            int damage = rand.Next(atk - error, atk + error);

                            if (IsCritical) damage = (int)(damage * 1.6f);

                            Console.Write($"Lv.{player.Name} 을(를) 맞췄습니다. [데미지: {damage}]");
                            if (IsCritical) Console.WriteLine(" - 치명타 공격!!");
                            if (player.Hp - damage > 0)
                            {
                                Console.WriteLine($"HP {player.Hp}->{player.Hp - damage}");
                                player.Hp -= damage;
                            }
                            else
                            {
                                Console.WriteLine($"HP {player.Hp}->0");
                                player.Hp = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Lv.{player.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                        }

                        Console.WriteLine();
                        Console.WriteLine("0. 다음");
                        Console.WriteLine();
                        int input = CheckValidInput(0, 0);
                        switch (input)
                        {
                            case 0:
                                Defend(++index);
                                break;
                        }
                    }
                    else
                    {
                        index++;
                        if (index == monsterList.Count)
                        {
                            if (monsterList.FindLastIndex(i => i.IsLive == true) == -1) 
                            {
                                //승리
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Battle!! - Result");
                                Console.WriteLine();
                                Console.WriteLine("Victory");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine($"던전에서 몬스터 {monsterList.Count}를 잡았습니다.");
                                Console.WriteLine();
                                Console.WriteLine("0. 다음");
                                Console.WriteLine();

                                player.Mp += 10;
                                if (player.Mp > 50) player.Mp = 50;

                                int input = CheckValidInput(0, 0);
                                switch (input)
                                {
                                    case 0:
                                        DisplayGameIntro();
                                        break;
                                }
                            }
                            else
                            {
                                DisplayBattle();
                            }

                        }
                        else
                        {
                            Defend(index);
                        }
                    }
                }
            }
        }
    }
}