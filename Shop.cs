
using Framework;
using SpartaDungeon.Managers;

namespace SpartaDungeon
{
    public static class Shop
    {
        // 이 부분도 인벤토리 Table과 겹치는 부분들을 통합해 UIManager로 빼버리자..
        public static void DrawShopTable(int posY, Player player)
        {
            var itemList = Manager.Instance.Data.GetItemList();
            if (itemList == null)
                throw new NullReferenceException("아이템 리스트가 없습니다. 리소스가 로드문제!");

            int consoleWidth = Manager.Instance.UI.consoleWidth;
            int numRows = itemList.Count;
            int consoleDiv = consoleWidth / 8;

            int StartPosY = 12;
            int EndPosY = Manager.Instance.UI.boxPosY - 2;

            // Column Width Setting
            int widthName = 20;
            int widthTypeStat = 20;
            int widthPurchase = 12;
            int widthDescription = consoleWidth - consoleDiv * 2 - widthName - widthTypeStat - widthPurchase - 1;

            Manager.Instance.UI.ClearRowToRow(9, EndPosY);
            Manager.Instance.UI.DrawUIMessageBox();
            Manager.Instance.UI.PrintTextBoxMessage("[보유 골드] ");
            Manager.Instance.UI.PrintTextToColor($"  [ {player.PlayerData.Gold} G ]", ConsoleColor.Yellow);
            Manager.Instance.UI.PrintTextBoxMessage("[1]. 아이템 구매 / [0]. 나가기", 1);

            // Table Header
            string headerRow = "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("아이템 이름", widthName - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("아이템타입/스탯", widthTypeStat - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("가격", widthPurchase - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("설명", widthDescription - 1) + "│";

            // Table Border
            Console.SetCursorPosition(consoleDiv, posY);
            Manager.Instance.UI.PrintTextToColor(new string('─', consoleWidth - consoleDiv * 2), ConsoleColor.Cyan);

            // Table Header Columns
            Console.SetCursorPosition(consoleDiv, posY + 1);
            Manager.Instance.UI.PrintTextToColor(headerRow, ConsoleColor.Cyan);

            Console.SetCursorPosition(consoleDiv, posY + 2);
            Manager.Instance.UI.PrintTextToColor(new string('─', consoleWidth - consoleDiv * 2), ConsoleColor.Cyan);

            int itemsPerPage = 9;
            int currentPage = 0;

            while (true)
            {
                int startIndex = currentPage * itemsPerPage;
                int endIndex = startIndex + itemsPerPage;
                if (endIndex >= numRows)
                {
                    endIndex = numRows;
                }

                int endPosY = posY + 3 + endIndex - startIndex;

                for (int i = startIndex; i < endIndex; ++i)
                {
                    var item = itemList[i];
                    string dataRow = "│";
                    string itemType = item.ItemType.ToString();
                    string itemStat = item.Status.ToString();

                    if (itemType == "WEAPON")
                    {
                        string message = "공격력 + " + itemStat;
                        itemType = message;
                    }
                    else if (itemType == "ARMOR")
                    {
                        string message = "방어력 + " + itemStat;
                        itemType = message;
                    }

                    dataRow += Manager.Instance.UI.GetPaddingToMessage(item.ItemName, widthName - 1) + "│";
                    dataRow += Manager.Instance.UI.GetPaddingToMessage(itemType, widthTypeStat - 1) + "│";
                    dataRow += Manager.Instance.UI.GetPaddingToMessage(item.PricePurchase.ToString(), widthPurchase - 1) + "│";
                    dataRow += Manager.Instance.UI.GetPaddingToMessage(item.ItemDescription, widthDescription - 1) + "│";

                    Console.SetCursorPosition(consoleDiv, posY + 3 + (i - startIndex));
                    Manager.Instance.UI.PrintTextToColor(dataRow, ConsoleColor.Gray);
                }
                // Table Border Bottom
                Console.SetCursorPosition(consoleDiv, endPosY);
                Manager.Instance.UI.PrintTextToColor(new string('─', consoleWidth - consoleDiv * 2), ConsoleColor.Gray);

                Manager.Instance.UI.ClearRow(EndPosY);
                Manager.Instance.UI.PrintTextAlignCenter(
                    $"페이지 {currentPage + 1}/{(numRows - 1) / itemsPerPage + 1}", EndPosY, ConsoleColor.Gray);
                Manager.Instance.UI.PrintTextToColor(" [ prev : Q / next : E ]", ConsoleColor.Yellow);

                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.E)
                {
                    currentPage++;
                    if (currentPage * itemsPerPage >= numRows)
                        currentPage = 0;
                }
                else if (key == ConsoleKey.Q)
                {
                    currentPage--;
                    if (currentPage < 0)
                        currentPage = (numRows - 1) / itemsPerPage;
                }
                else if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                {
                    
                }
                else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                    break;

                Manager.Instance.UI.ClearRowToRow(StartPosY, EndPosY);
            }
        }

        public static void PurchaseMode(Player player)
        {

        }

    }
}
