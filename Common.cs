using Newtonsoft.Json;
using System.Reflection;
using static SpartaDungeonBattle.Inventory;
using static SpartaDungeonBattle.Shop;
using static SpartaDungeonBattle.MyInfo;
using static SpartaDungeonBattle.Battle;
using static SpartaDungeonBattle.CharacterInfo;
using static SpartaDungeonBattle.Portion;

namespace SpartaDungeonBattle
{
    internal class Common
    {
        //저장경로 설정
        public static string savePath = "";

        //게임 규칙 선언
        public enum ItemTypes { 무기, 방어구, 방패 };
        public static int[] myEquipment = new int[3];
        public enum Abilitys { 공격력, 방어력 };
        public static int[] myAddStat = new int[2];

        //게임 데이터 관련 변수 선언
        public static Character player;
        public static List<Item> myItem = new();
        public static List<Item> shop = new();
        public static int[] maxExp = { 0, 10, 35, 65, 100 };

        //아이템 정렬관련 변수 선언
        public static int sort = 0;
        public static bool order = true;

        //케릭터 관련 클레스
        public enum CharacterJob { 전사, 도적 };
        public class Character
        {
            public string Name { get; set; }
            public CharacterJob Job { get; set; }
            public int Level { get; set; }
            public int Atk { get; set; }
            public int Def { get; set; }
            public int Hp { get; set; }
            public int Mp { get; set; }
            public int Gold { get; set; }
            public int Exp { get; set; }
            public int HpPortion { get; set; }
            public int MpPortion { get; set; }
        }

        public class Item
        {
            public int ItemId { get; set; }
            public bool Equipment { get; set; }
            public string ItemName { get; set; }
            public ItemTypes Type { get; set; }
            public List<ItemAbility> ItemAbilitys { get; set; }
            public string Described { get; set; }
            public int Stat { get; set; }
            public int Price { get; set; }
        }

        public class ItemAbility
        {
            public Abilitys Ability { get; set; }
            public int Stat { get; set; }
        }

        /// <summary>초기 세팅</summary>
        public static void GameDataSetting(int set)
        {
            // 프로젝트 경로와, 솔루션 명을 활용하여 파일경로를 설정하고 해당 파일을 읽기
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string solutionName = Assembly.GetEntryAssembly().GetName().Name;

            // 게임 저장을 위한 경로 저장
            savePath = projectPath.Substring(0, projectPath.IndexOf(solutionName) + solutionName.Length) + "/data";

            // 상점 정보 세팅
            string jsonShop = File.ReadAllText($"{savePath}/shopData.json");
            shop = JsonConvert.DeserializeObject<List<Item>>(jsonShop);

            // 캐릭터 정보 세팅
            string jsonPlayer = File.ReadAllText($"{savePath}/playerData.json");
            player = JsonConvert.DeserializeObject<Character>(jsonPlayer);

            // 아이템 정보 세팅
            string jsonMyItem = File.ReadAllText($"{savePath}/myItemData.json");
            myItem = JsonConvert.DeserializeObject<List<Item>>(jsonMyItem);

            // 아이템 장착 여부 검증
            foreach (Item item in myItem)
            {
                bool equipment = item.Equipment;
                if (equipment) myEquipment[(int)item.Type] = item.ItemId;
            }

            //장비 추가 스텟 적용
            AddStat();

            //게임 시작 화면
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
            Console.WriteLine("4. 전투시작");
            Console.WriteLine("5. 회복 아이템");
            Console.WriteLine();
            Console.WriteLine("0. 캐릭터 정보 변경");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 5);
            switch (input)
            {
                case 0:
                    DisplayName();
                    break;
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
                    AddStat();
                    SetMonsterList();
                    break;
                case 5:
                    DisplayPortion("");
                    break;
            }
        }

        /// <summary>착용한 장비의 스텟 계산 메소드</summary>
        public static void AddStat()
        {
            myAddStat[0] = 0;
            myAddStat[1] = 0;
            foreach (Item item in myItem)
            {
                if (item.Equipment)
                {
                    foreach (ItemAbility itemAbility in item.ItemAbilitys)
                    {
                        switch (itemAbility.Ability)
                        {
                            case Abilitys.공격력:
                                myAddStat[0] += itemAbility.Stat;
                                break;
                            case Abilitys.방어력:
                                myAddStat[1] += itemAbility.Stat;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>정보 저장 메소드</summary>
        public static void Save()
        { 
            string jsonPlayer = JsonConvert.SerializeObject(player);
            string jsonMyItem = JsonConvert.SerializeObject(myItem);
            File.WriteAllText($"{savePath}/playerData.json", jsonPlayer);
            File.WriteAllText($"{savePath}/myItemData.json", jsonMyItem);
        }

        /// <summary>입력 검증 메소드</summary>
        public static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                    {
                        if (ret == 0) Save();
                        return ret;
                    }

                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
