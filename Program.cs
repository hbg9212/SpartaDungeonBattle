using static SpartaDungeonBattle.Common;
using static SpartaDungeonBattle.Inventory;
using static SpartaDungeonBattle.Shop;
using static SpartaDungeonBattle.Battle;
using System.Xml.Linq;

namespace SpartaDungeonBattle
{
    class Program
    {
        /// <summary>게임 시작</summary>
        static void Main()
        {
            GameDataSetting(0);
            if (!player.Initialized) DisplayName();
            DisplayGameIntro();
        }
        /// <summary>게임 초기 화면 출력</summary>
        public static void DisplayName()
        {
            player.Level = 1;
            player.Exp = 0;
            player.Hp = 100;
            player.Mp = 50;
            player.Atk = 0;
            player.Def = 0;
            player.Gold = 1000;
            player.DungeonFloor = 1;
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            string name = CheckValidNameInput();
            Console.WriteLine($"이름을 {name} (으)로 하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 확인");
            Console.WriteLine("2. 취소");
            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    player.Name = name;
                    DisplayJob();
                    break;
                case 2:
                    DisplayName();
                    break;
            }
        }
        public static void DisplayJob()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("직업 선택");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 직업을 선택해주세요.");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            int num = CheckValidInput(1, 2);
            switch (num)
            {
                case 1:
                    player.Job = warrior.JobName;
                    player.Hp = warrior.BaseHp;
                    MaxHP = warrior.BaseHp;
                    player.Mp = warrior.BaseMp;
                    MaxMP = warrior.BaseMp;
                    player.Atk = warrior.BaseAtk;
                    player.Def = warrior.BaseDef;
                    break;
                case 2:
                    player.Job = mage.JobName;
                    player.Hp = mage.BaseHp;
                    MaxHP = mage.BaseHp;
                    MaxMP = mage.BaseMp;
                    player.Mp = mage.BaseMp;
                    player.Atk = mage.BaseAtk;
                    player.Def = mage.BaseDef;
                    break;
            }
            Console.Clear() ;
            Console.WriteLine($"{player.Job} (으)로 전직하였습니다.");
            Console.WriteLine("0. 다음");
            int input = CheckValidInput(0, 0);
            if(input == 0)
            {
                player.Initialized = true;
                DisplayGameIntro();
            }
        }
        public static void DisplayGameIntro()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine($"4. 전투시작 (현재 진행 : {player.DungeonFloor}층)");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 4);
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
                case 4:
                    if (player.Hp != 0)
                    {
                        baseHP = player.Hp;
                        baseMP = player.Mp;
                        SpawnRandomMonsters();
                        DisplayBattle();
                    }
                    else Console.WriteLine("체력이 부족합니다. 체력을 회복하고 도전해주세요!");
                    Thread.Sleep(1000);
                    DisplayGameIntro();
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
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
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
            Console.WriteLine($"마나 : {player.Mp}");
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