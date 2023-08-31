using Newtonsoft.Json;
using System.Reflection;
using static SpartaDungeonBattle.Program;

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
        public static List<Monster> monsters = new();
        public static Job[] jobs = new Job[]
        {
            new Warrior(),
            new Mage()
        };

        //아이템 정렬관련 변수 선언
        public static int sort = 0;
        public static bool order = true;

        public class Character
        {
            public string Name { get; set; }
            public string Job { get; set; }
            public int Level { get; set; }
            public double Atk { get; set; }
            public double Def { get; set; }
            public int Hp { get; set; }
            public int MaxHP { get; set; }
            public int Mp { get; set; }
            public int MaxMP { get; set; }
            public int Gold { get; set; }
            public int Exp { get; set; }
            public int DungeonFloor { get; set; }
            public bool Initialized { get; set; }


            public void InitializePlayer()
            {
                Level = 1;
                Exp = 0;
                Hp = 100;
                Mp = 50;
                Atk = 0;
                Def = 0;
                Gold = 1000;
                DungeonFloor = 1;
            }
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

        public class Monster
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
            
            [JsonConstructor]
            public Monster(string name, int level, int hp)
            {
                Name = name;
                Level = level;
                HP = hp;
            }

            // 복사 생성자
            public Monster(Monster other)
            {
                Name = other.Name;
                Level = other.Level;
                HP = other.HP;
            }
        }

        public class Job
        {
            public string JobName { get; set; }
            public int BaseHp{ get; set; }
            public int BaseMp { get; set; }
            public int BaseAtk { get; set; }
            public int BaseDef { get; set; }
        }

        public class Warrior : Job
        {
            public Warrior()
            {
                JobName = "전사";
                BaseHp = 100;
                BaseMp = 30;
                BaseAtk = 10;
                BaseDef = 5;
            }

        }

        public class Mage : Job
        {
            public Mage()
            {
                JobName = "마법사";
                BaseHp = 80;
                BaseMp = 50;
                BaseAtk = 15;
                BaseDef = 3;
            }
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

            // 몬스터 정보 세팅
            string jsonMonster = File.ReadAllText($"{savePath}/monsterData.json");
            monsters = JsonConvert.DeserializeObject<List<Monster>>(jsonMonster);

            // 아이템 장착 여부 검증
            foreach (Item item in myItem)
            {
                bool equipment = item.Equipment;
                if (equipment) myEquipment[(int)item.Type] = item.ItemId;
            }

            //장비 추가 스텟 적용
            AddStat();
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

        public static string CheckValidNameInput()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }

                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
        }
    }
}
