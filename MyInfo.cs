using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    internal class MyInfo
    {
        /// <summary>케릭터 정보 화면 출력</summary>
        public static void DisplayMyInfo()
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 캐릭터 이름 변경");
            Console.WriteLine("2. 캐릭터 직업 선택");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    DisplayCharacterName();
                    break;

                case 2:
                    DisplayCharacterJob();
                    break;
            }


            static void DisplayCharacterName()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("캐릭터 이름 변경");
                Console.ResetColor();
                Console.WriteLine("새로운 이름을 입력해주세요.");

                string newName = Console.ReadLine();
                player.Name = newName;
                DisplayMyInfo();
            }



            static void DisplayCharacterJob()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("캐릭터 직업 선택");
                Console.ResetColor();
                Console.WriteLine("새로운 직업을 선택해주세요.");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 도적");

                int jobChoice = CheckValidInput(1, 4);

                switch (jobChoice)
                {
                    case 1:
                        player.Job = "전사";
                        break;

                    case 2:
                        player.Job = "마법사";
                        break;

                    case 3:
                        player.Job = "도적";
                        break;
                }
                DisplayMyInfo();
            }
        }







    }

}




