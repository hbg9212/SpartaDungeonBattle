using static SpartaDungeonBattle.Common;
using static SpartaDungeonBattle.Inventory;
using static SpartaDungeonBattle.Shop;

namespace SpartaDungeonBattle
{
    class Program
    {
        /// <summary>게임 시작</summary>
        static void Main()
        {
            GameDataSetting(0);
            DisplayGameIntro();
        }
        /// <summary>게임 초기 화면 출력</summary>
        public static void DisplayGameIntro()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
            }
        }

        /// <summary>케릭터 정보 화면 출력</summary>
        static void DisplayMyInfo()
        {
            AddStat();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.Write($"공격력 : {player.Atk} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{(myAddStat[0] > 0 ? "(+" + myAddStat[0] + ")" : "")}");
            Console.ResetColor();
            Console.Write($"방어력 : {player.Def} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{(myAddStat[1] > 0 ? "(+" + myAddStat[1] + ")" : "")}");
            Console.ResetColor();
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

    }
}