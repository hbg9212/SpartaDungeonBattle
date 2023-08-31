using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    internal class Portion
    {
        /// <summary>회복 아이템 화면 출력</summary>
        public static void DisplayPortion(string msg)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("회복");
            Console.ResetColor();
            Console.WriteLine("포션을 사용 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine($"MP {player.Mp}/50");
            Console.WriteLine();

            Console.WriteLine($"1. HP 포션 : 사용시 HP 30 회복 (남은포션 {player.HpPortion})");
            Console.WriteLine($"2. MP 포션 : 사용시 MP 30 회복 (남은포션 {player.MpPortion})");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(msg);
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    UseHpPortion();
                    break;
                case 2:
                    UseMpPortion();
                    break;
            }
        }

        /// <summary>Hp 회복 아이템 사용 메서드</summary>
        public static void UseHpPortion()
        {
            if(player.HpPortion == 0)
            {
                DisplayPortion("HP 포션이 부족 합니다.");
            }
            else
            {
                if(player.Hp == 100)
                {
                    DisplayPortion("더이상 회복이 불가능 합니다.");
                }
                else
                {
                    player.HpPortion--;
                    if (player.Hp + 30 > 100)
                    {
                        player.Hp = 100;
                        DisplayPortion($"HP {player.Hp} -> 100");
                    }
                    else
                    {
                        player.Hp += 30;
                        DisplayPortion($"HP {player.Hp-30} -> {player.Hp}");
                    }
                }
            }
        }

        /// <summary>Mp 회복 아이템 사용 메서드</summary>
        public static void UseMpPortion()
        {
            if (player.MpPortion == 0)
            {
                DisplayPortion("MP 포션이 부족 합니다.");
            }
            else
            {
                if (player.Mp == 50)
                {
                    DisplayPortion("더이상 회복이 불가능 합니다.");
                }
                else
                {
                    player.MpPortion--;
                    if (player.Mp + 30 > 50)
                    {
                        player.Mp = 50;
                        DisplayPortion($"HP {player.Hp} -> 50");
                    }
                    else
                    {
                        player.Mp += 30;
                        DisplayPortion($"HP {player.Mp-30} -> {player.Mp}");
                    }
                }
            }
        }
    }
}
