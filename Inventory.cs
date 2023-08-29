using System.Text;
using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    internal class Inventory
    {
        /// <summary>인벤토리 화면 출력</summary>
        public static void DisplayInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in myItem)
            {

                // 한글 한글자당 2자리를 차지하여 공백을 먼저 계산 해야 함
                // 일이삼사오육칠팔구십, 일B삼D오F칠H구J, ABCDEFGHIJ 등 다양한 경우에서 모두 동일한 공백을 갖도록 계산
                string str = item.ItemName;
                int padLen = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(str.Substring(i, 1)).Length == 3)
                        padLen++;
                }

                padLen = 20 - padLen;

                if (item.Equipment) Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($" - {(item.Equipment ? "[E]" : "   ")}| {str.PadRight(padLen)} | {item.ItemAbilitys[0].Ability} +{item.ItemAbilitys[0].Stat.ToString().PadRight(2)} | {item.Described}");
                if (item.ItemAbilitys.Count > 1)
                {
                    for (int i = 1; i < item.ItemAbilitys.Count; i++)
                    {
                        string whiteSpace = "";
                        Console.WriteLine($"{whiteSpace.PadRight(28, ' ')} | {item.ItemAbilitys[i].Ability} +{item.ItemAbilitys[i].Stat.ToString().PadRight(2)} |");
                    }
                }

                if (item.Equipment) Console.ResetColor();
            }
            Console.WriteLine("1. 아이템 정렬");
            Console.WriteLine("2. 장착 관리");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    DisplayInventorySort();
                    break;
                case 2:
                    DisplayEquipment("");
                    break;
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        /// <summary>인벤토리 정렬 화면 출력</summary>
        static void DisplayInventorySort()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 정렬");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in myItem)
            {

                // 한글 한글자당 2자리를 차지하여 공백을 먼저 계산 해야 함
                // 일이삼사오육칠팔구십, 일B삼D오F칠H구J, ABCDEFGHIJ 등 다양한 경우에서 모두 동일한 공백을 갖도록 계산
                string str = item.ItemName;
                int padLen = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(str.Substring(i, 1)).Length == 3)
                        padLen++;
                }

                padLen = 20 - padLen;

                if (item.Equipment) Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($" - {(item.Equipment ? "[E]" : "   ")}| {str.PadRight(padLen)} | {item.ItemAbilitys[0].Ability} +{item.ItemAbilitys[0].Stat.ToString().PadRight(2)} | {item.Described}");
                if (item.ItemAbilitys.Count > 1)
                {
                    for (int i = 1; i < item.ItemAbilitys.Count; i++)
                    {
                        string whiteSpace = "";
                        Console.WriteLine($"{whiteSpace.PadRight(28, ' ')} | {item.ItemAbilitys[i].Ability} +{item.ItemAbilitys[i].Stat.ToString().PadRight(2)} |");
                    }
                }

                if (item.Equipment) Console.ResetColor();
            }
            Console.WriteLine("1. 이름");
            Console.WriteLine("2. 장착순");
            Console.WriteLine("3. 공격력");
            Console.WriteLine("4. 방어력");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    ChangeOrder(input);
                    break;

                case 0:
                    DisplayInventory();
                    break;
            }
        }

        /// <summary>인벤토리 정렬 메소드</summary>
        static void ChangeOrder(int type)
        {
            switch (type)
            {
                case 1:
                    if (sort == type)
                    {
                        if (order)
                        {
                            myItem = myItem.OrderByDescending(item => item.ItemName).ToList();
                            order = false;
                        }
                        else
                        {
                            myItem = myItem.OrderBy(item => item.ItemName).ToList();
                            order = true;
                        }
                    }
                    else
                    {
                        sort = type;
                        myItem = myItem.OrderBy(item => item.ItemName).ToList();
                        order = true;
                    }
                    break;
                case 2:
                    if (sort == type)
                    {
                        if (order)
                        {
                            myItem = myItem.OrderBy(item => item.Equipment).ToList();
                            order = false;
                        }
                        else
                        {
                            myItem = myItem.OrderByDescending(item => item.Equipment).ToList();
                            order = true;
                        }
                    }
                    else
                    {
                        sort = type;
                        myItem = myItem.OrderByDescending(item => item.Equipment).ToList();
                        order = true;
                    }
                    break;
                case 3:
                    if (sort == type)
                    {
                        if (order)
                        {
                            myItem = myItem.OrderBy(item => item.Stat).ToList();
                            order = false;
                        }
                        else
                        {
                            myItem = myItem.OrderByDescending(item => item.Stat).ToList();
                            order = true;
                        }
                    }
                    else
                    {
                        foreach (Item item in myItem)
                        {
                            item.Stat = -1;
                            foreach (ItemAbility itemAbility in item.ItemAbilitys)
                            {
                                if (itemAbility.Ability == Abilitys.공격력)
                                {
                                    item.Stat = itemAbility.Stat;
                                }
                            }
                        }
                        sort = type;
                        myItem = myItem.OrderByDescending(item => item.Stat).ToList();
                        order = true;
                    }
                    break;
                case 4:
                    if (sort == type)
                    {
                        if (order)
                        {
                            myItem = myItem.OrderBy(item => item.Stat).ToList();
                            order = false;
                        }
                        else
                        {
                            myItem = myItem.OrderByDescending(item => item.Stat).ToList();
                            order = true;
                        }
                    }
                    else
                    {
                        foreach (Item item in myItem)
                        {
                            item.Stat = -1;
                            foreach (ItemAbility itemAbility in item.ItemAbilitys)
                            {
                                if (itemAbility.Ability == Abilitys.방어력)
                                {
                                    item.Stat = itemAbility.Stat;
                                }
                            }
                        }
                        sort = type;
                        myItem = myItem.OrderByDescending(item => item.Stat).ToList();
                        order = true;
                    }
                    break;
                default:
                    break;
            }
            DisplayInventorySort();
        }

        /// <summary>인벤토리 장착 화면 출력</summary>
        static void DisplayEquipment(string msg)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            int itemNumber = 1;
            int itemOver = myItem.Count > 10 ? 1 : 0;
            foreach (Item item in myItem)
            {

                // 한글 한글자당 2자리를 차지하여 공백을 먼저 계산 해야 함
                // 일이삼사오육칠팔구십, 일B삼D오F칠H구J, ABCDEFGHIJ 등 다양한 경우에서 모두 동일한 공백을 갖도록 계산
                string str = item.ItemName;
                int padLen = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(str.Substring(i, 1)).Length == 3)
                        padLen++;
                }

                padLen = 20 - padLen;
                if (item.Equipment) Console.ForegroundColor = ConsoleColor.Green;
                //인벤토리의 아이템이 10개 이상 존재하면, itemNumber 여백 필요
                //itemOver 변수 사용
                string strItemNumber = itemNumber++ + "";
                Console.WriteLine($" - {strItemNumber.PadRight(1 + itemOver)} {(item.Equipment ? "[E]" : "   ")}| {str.PadRight(padLen)} | {item.ItemAbilitys[0].Ability} +{item.ItemAbilitys[0].Stat.ToString().PadRight(2)} | {item.Described}");
                if (item.ItemAbilitys.Count > 1)
                {
                    for (int i = 1; i < item.ItemAbilitys.Count; i++)
                    {
                        string whiteSpace = "";
                        Console.WriteLine($"{whiteSpace.PadRight(30 + itemOver, ' ')} | {item.ItemAbilitys[i].Ability} +{item.ItemAbilitys[i].Stat.ToString().PadRight(2)} |");
                    }
                }
                if (item.Equipment) Console.ResetColor();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(msg);
            int input = CheckValidInput(0, myItem.Count());
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                default:
                    ChangeEquipment(input);
                    break;
            }
        }

        /// <summary>장비 장착 변경 메소드</summary>
        static void ChangeEquipment(int index)
        {
            string msg = "";
            --index;

            switch (myItem[index].Type)
            {
                case ItemTypes.무기:
                case ItemTypes.방어구:
                case ItemTypes.방패:
                    if (myItem[index].Equipment)
                    {
                        myItem[index].Equipment = false;
                        myEquipment[(int)myItem[index].Type] = 0;
                    }
                    else
                    {
                        if (myEquipment[(int)myItem[index].Type] == 0)
                        {
                            myItem[index].Equipment = true;
                            myEquipment[(int)myItem[index].Type] = myItem[index].ItemId;
                        }
                        else
                        {
                            //착용중인 아이템 무조건 변경
                            Console.WriteLine(myEquipment[(int)myItem[index].Type]);
                            myItem[myItem.FindIndex(i => i.ItemId == myEquipment[(int)myItem[index].Type])].Equipment = false;
                            myItem[index].Equipment = true;
                            myEquipment[(int)myItem[index].Type] = myItem[index].ItemId;

                            //착용중에 변경 불가 시
                            //msg = $"{myItem[index].Type} 아이템은 이미 착용중인 입니다.";
                        }
                    }
                    break;

                default:
                    msg = "착용이 불가능한 아이템 입니다.";
                    break;

            }
            AddStat();
            DisplayEquipment(msg);
        }
    }
}
