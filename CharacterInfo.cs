using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    internal class CharacterInfo
    {
        /// <summary>이름 입력 화면 출력</summary>
        public static void DisplayName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.WriteLine();
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"입력하신 이름은 {name} 입니다.");
            Console.WriteLine();
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(1,2);
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

        /// <summary>직업 선택 화면 출력</summary>
        public static void DisplayJob()
        {
            player.Level = 1;
            player.Exp = 0;
            player.Hp = 100;
            player.Mp = 0;
            player.Atk = 0;
            player.Def = 0;
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 직업을 선택해주세요.");
            Console.WriteLine();
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    player.Job = CharacterJob.전사;
                    player.Mp = 20;
                    player.Atk = 10;
                    player.Def = 5;
                    DisplayGameIntro();
                    break;
                case 2:
                    player.Job = CharacterJob.도적;
                    player.Mp = 50;
                    player.Atk = 7;
                    player.Def = 3;
                    DisplayGameIntro();
                    break;
            }
        }
    }
}
