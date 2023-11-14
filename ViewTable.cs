
using Framework;
using SpartaDungeon.Managers;

namespace SpartaDungeon
{
    public static class ViewTable
    {

        #region Status Menu
        public static void PrintCharacterStatus(Player player)
        {
            string statusText = Manager.Instance.Resource.GetTextResource(ResourceKeys.StatusMenuText);
            string[] textStatus = statusText.Split(new[] { "\n" }, StringSplitOptions.None);

            Console.Clear();

            const int START_POS = 1;
            int i;
            for (i = 0; i < textStatus.Length; ++i)
            {
                ConsoleColor color = ConsoleColor.Gray;
                if (i < 3) color = ConsoleColor.Yellow;
                else if (i >= 3 && i < 5) color = ConsoleColor.Cyan;

                Manager.Instance.UI.PrintTextAlignCenter(textStatus[i], i + START_POS, color);
            }

            player?.DrawCharacterStatus(i);

            Manager.Instance.UI.DrawUIMessageBox();
            Manager.Instance.UI.PrintTextBoxMessage("[ 0. 나가기 ]", 0, ConsoleColor.Blue);

            int selectNumber = Utilities.CheckValidInput(0, 0);
        }
        #endregion

        #region Inventory Menu
        public static void PrintInventoryMenu(Player player)
        {
            string inventoryText = Manager.Instance.Resource.GetTextResource(ResourceKeys.InventoryMenuText);
            string[] textInventory = inventoryText.Split(new[] { "\n" }, StringSplitOptions.None);

            Console.Clear();

            const int START_POS = 1;
            int i;
            for (i = 0; i < textInventory.Length; ++i)
            {
                ConsoleColor color = ConsoleColor.Gray;
                if (i < 3) color = ConsoleColor.Yellow;
                else if (i >= 3 && i < 5) color = ConsoleColor.Cyan;

                Manager.Instance.UI.PrintTextAlignCenter(textInventory[i], i + START_POS, color);
            }

            // 테이블
            player?.Inventory?.DrawTable(i + 1);

            Manager.Instance.UI.DrawUIMessageBox();
            Manager.Instance.UI.PrintTextBoxMessage(" [ 1. 장착관리 ]  [ 2. 정렬 ]  [ 0. 나가기 ]", 0, ConsoleColor.Blue);

            int selectNumber = Utilities.CheckValidInput(0, 2);
            switch (selectNumber)
            {
                case 0:
                    break;
                case 1:
                    player?.Inventory?.DrawTable(i + 1, true);
                    break;
                case 2:
                    Manager.Instance.UI.ClearUIMessageBox();
                    Manager.Instance.UI.PrintTextBoxMessage(" -- 어떤 정렬을 할 것인지 선택하세요 --", 0, ConsoleColor.Blue);
                    Manager.Instance.UI.PrintTextBoxMessage("[1. 이름]  [2. 장착여부]  [3. 공격력]  [4. 방어력]", 1, ConsoleColor.Gray);
                    Manager.Instance.UI.PrintTextBoxMessage("[0]. 나가기", 4, ConsoleColor.Red);
                    selectNumber = Utilities.CheckValidInput(0, 4);
                    if (selectNumber == 0) PrintInventoryMenu(player);
                    else
                    {
                        Action<Player, E_SORTING_TYPE>? sortingAction = selectNumber switch
                        {
                            1 => (p, s) => p?.Inventory?.SortingInventory(s),
                            2 => (p, s) => p?.Inventory?.SortingInventory(s),
                            3 => (p, s) => p?.Inventory?.SortingInventory(s),
                            4 => (p, s) => p?.Inventory?.SortingInventory(s),
                            _ => null
                        };

                        sortingAction?.Invoke(player, GetSortingType(selectNumber));
                    }
                    PrintInventoryMenu(player);
                    break;
            }
        }

        private static E_SORTING_TYPE GetSortingType(int select)
        {
            return select switch
            {
                1 => E_SORTING_TYPE.NAME,
                2 => E_SORTING_TYPE.EQUIP,
                3 => E_SORTING_TYPE.TYPEATK,
                4 => E_SORTING_TYPE.TYPEDEF,
                _ => throw new ArgumentException("Invalid selectNumber")
            };
        }
        #endregion
    }
}