using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    internal class CharacterInfo
    {
        public class Skill
        {
            public string Name { get; }
            public string Described { get; }
            public int Mp { get; }
            public int Count { get; }
            public float Damage { get; }
            public Skill(string name, string descrided, int mp, int count, float damage) 
            {
                Name = name;
                Described = descrided; 
                Mp = mp;
                Count = count;
                Damage = damage;
            }
        }

        static Skill w1 = new Skill("알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 10, 1, 2.0f);
        static Skill w2 = new Skill("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", 15, 2, 1.5f);

        static Skill s1 = new Skill("알파 스트라이크", "공격력 * 3 로 하나의 적을 공격합니다.", 10, 1, 3.0f);
        static Skill s2 = new Skill("메가 스트라이크", "공격력 * 1.5 로 적 전체를 공격합니다.", 15, -1, 2.0f);

        static Skill[] Warrior = { w1, w2 };
        static Skill[] Thief = { s1, s2 };

        public static List<Skill[]> SkillSet = new List<Skill[]>{ Warrior, Thief };

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
            player.Mp = 50;
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
                    player.Atk = 10;
                    player.Def = 5;
                    DisplayGameIntro();
                    break;
                case 2:
                    player.Job = CharacterJob.도적;
                    player.Atk = 7;
                    player.Def = 3;
                    DisplayGameIntro();
                    break;
            }
        }
    }
}
