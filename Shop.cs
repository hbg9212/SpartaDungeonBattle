using System.Text;
using static SpartaDungeonBattle.Program;
using static SpartaDungeonBattle.Common;

namespace SpartaDungeonBattle
{
    internal class Shop
    {
        /// <summary>상점 화면 출력</summary>
        public static void DisplayShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{player.Gold}");
            Console.ResetColor();
            Console.Write(" G\n");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (Item item in shop)
            {

                // 한글 한글자당 2자리를 차지하여 공백을 먼저 계산 해야 함
                // 일이삼사오육칠팔구십, 일B삼D오F칠H구J, ABCDEFGHIJ 등 다양한 경우에서 모두 동일한 공백을 갖도록 계산
                string strName = item.ItemName;
                string strDescribed = item.Described;
                int namePadLen = 0;
                int describedPadLen = 0;
                for (int i = 0; i < strName.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(strName.Substring(i, 1)).Length == 3)
                        namePadLen++;
                }
                for (int i = 0; i < strDescribed.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(strDescribed.Substring(i, 1)).Length == 3)
                        describedPadLen++;
                }

                namePadLen = 20 - namePadLen;
                describedPadLen = 60 - describedPadLen;

                //구매완료 텍스트 색 변경
                if (myItem.FindIndex(i => i.ItemId.Equals(item.ItemId)) > -1) Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write($" - {strName.PadRight(namePadLen)} | {item.ItemAbilitys[0].Ability} +{item.ItemAbilitys[0].Stat.ToString().PadRight(2)} | {strDescribed.PadRight(describedPadLen)} | ");
                Console.WriteLine($"{(myItem.FindIndex(i => i.ItemId.Equals(item.ItemId)) > -1 ? "구매완료" : item.Price + " G")}");
                if (item.ItemAbilitys.Count > 1)
                {
                    for (int i = 1; i < item.ItemAbilitys.Count; i++)
                    {
                        string whiteSpace = "";
                        Console.WriteLine($"{whiteSpace.PadRight(23, ' ')} | {item.ItemAbilitys[i].Ability} +{item.ItemAbilitys[i].Stat.ToString().PadRight(2)} |");
                    }
                }
                if (myItem.FindIndex(i => i.ItemId.Equals(item.ItemId)) > -1) Console.ResetColor();

            }
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    DisplayPurchase("");
                    break;
                case 2:
                    DisplaySale();
                    break;
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        /// <summary>상점 구매 화면 출력</summary>
        static void DisplayPurchase(string msg)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{player.Gold}");
            Console.ResetColor();
            Console.Write(" G\n");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            int itemNumber = 1;
            int itemOver = shop.Count > 10 ? 1 : 0;
            foreach (Item item in shop)
            {

                // 한글 한글자당 2자리를 차지하여 공백을 먼저 계산 해야 함
                // 일이삼사오육칠팔구십, 일B삼D오F칠H구J, ABCDEFGHIJ 등 다양한 경우에서 모두 동일한 공백을 갖도록 계산
                string strName = item.ItemName;
                string strDescribed = item.Described;
                int namePadLen = 0;
                int describedPadLen = 0;
                for (int i = 0; i < strName.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(strName.Substring(i, 1)).Length == 3)
                        namePadLen++;
                }
                for (int i = 0; i < strDescribed.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(strDescribed.Substring(i, 1)).Length == 3)
                        describedPadLen++;
                }

                namePadLen = 20 - namePadLen;
                describedPadLen = 60 - describedPadLen;

                //인벤토리의 아이템이 10개 이상 존재하면, itemNumber 여백 필요
                //itemOver 변수 사용
                string strItemNumber = itemNumber++ + "";

                //구매완료 텍스트 색 변경
                if (myItem.FindIndex(i => i.ItemId.Equals(item.ItemId)) > -1) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($" - {strItemNumber.PadRight(1 + itemOver)} {strName.PadRight(namePadLen)} | {item.ItemAbilitys[0].Ability} +{item.ItemAbilitys[0].Stat.ToString().PadRight(2)} | {strDescribed.PadRight(describedPadLen)} | ");
                Console.WriteLine($"{(myItem.FindIndex(i => i.ItemId.Equals(item.ItemId)) > -1 ? "구매완료" : item.Price + " G")}");
                if (item.ItemAbilitys.Count > 1)
                {
                    for (int i = 1; i < item.ItemAbilitys.Count; i++)
                    {
                        string whiteSpace = "";
                        Console.WriteLine($"{whiteSpace.PadRight(25, ' ')} | {item.ItemAbilitys[i].Ability} +{item.ItemAbilitys[i].Stat.ToString().PadRight(2)} |");
                    }
                }
                if (myItem.FindIndex(i => i.ItemId.Equals(item.ItemId)) > -1) Console.ResetColor();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(msg);
            int input = CheckValidInput(0, shop.Count());
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    Purchase(input);
                    break;
            }
        }

        static void Purchase(int index)
        {
            --index;
            string msg = "";
            if (myItem.FindIndex(i => i.ItemId.Equals(shop[index].ItemId)) > -1)
            {
                msg = "이미 구매한 아이템입니다.";
            }
            else
            {
                if (player.Gold < shop[index].Price)
                {
                    msg = "Gold 가 부족합니다.";
                }
                else
                {
                    player.Gold -= shop[index].Price;
                    myItem.Add(shop[index]);
                    msg = "구매를 완료했습니다.";

                }
            }
            DisplayPurchase(msg);
        }


        /// <summary>상점 판매 화면 출력</summary>
        static void DisplaySale()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{player.Gold}");
            Console.ResetColor();
            Console.Write(" G\n");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            int itemNumber = 1;
            int itemOver = myItem.Count > 10 ? 1 : 0;
            foreach (Item item in myItem)
            {

                // 한글 한글자당 2자리를 차지하여 공백을 먼저 계산 해야 함
                // 일이삼사오육칠팔구십, 일B삼D오F칠H구J, ABCDEFGHIJ 등 다양한 경우에서 모두 동일한 공백을 갖도록 계산
                string strName = item.ItemName;
                string strDescribed = item.Described;
                int namePadLen = 0;
                int describedPadLen = 0;
                for (int i = 0; i < strName.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(strName.Substring(i, 1)).Length == 3)
                        namePadLen++;
                }
                for (int i = 0; i < strDescribed.Length; i++)
                {
                    // 한글 한글자당 3바이트 씩 사용
                    if (Encoding.Default.GetBytes(strDescribed.Substring(i, 1)).Length == 3)
                        describedPadLen++;
                }

                namePadLen = 20 - namePadLen;
                describedPadLen = 60 - describedPadLen;

                //인벤토리의 아이템이 10개 이상 존재하면, itemNumber 여백 필요
                //itemOver 변수 사용
                string strItemNumber = itemNumber++ + "";

                Console.Write($" - {strItemNumber.PadRight(1 + itemOver)} {strName.PadRight(namePadLen)} | {item.ItemAbilitys[0].Ability} +{item.ItemAbilitys[0].Stat.ToString().PadRight(2)} | {strDescribed.PadRight(describedPadLen)} | ");

                //판매 금액 출력 
                //아이템의 가격의 85%
                Console.WriteLine($"{(int)(shop[shop.FindIndex(i => i.ItemId == item.ItemId)].Price * 0.85f)} G");
                if (item.ItemAbilitys.Count > 1)
                {
                    for (int i = 1; i < item.ItemAbilitys.Count; i++)
                    {
                        string whiteSpace = "";
                        Console.WriteLine($"{whiteSpace.PadRight(25, ' ')} | {item.ItemAbilitys[i].Ability} +{item.ItemAbilitys[i].Stat.ToString().PadRight(2)} |");
                    }
                }

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, myItem.Count());
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    Sale(input);
                    break;
            }
        }

        /// <summary>아이템 판매 메소드</summary>
        static void Sale(int index)
        {
            --index;
            if (myItem[index].Equipment) myEquipment[(int)myItem[index].Type] = 0;

            player.Gold += (int)(shop[shop.FindIndex(i => i.ItemId == myItem[index].ItemId)].Price * 0.85f);
            myItem.RemoveAt(index);
            AddStat();
            DisplaySale();
        }

    }
}
