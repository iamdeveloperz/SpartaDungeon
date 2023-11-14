
using Framework;
using SpartaDungeon.Managers;

namespace SpartaDungeon
{
    public static class ViewTable
    {
        #region Status Menu
        public static void PrintCharacterStatus(Player player)
        {
            int posY = Utilities.PrintTitle(ResourceKeys.StatusMenuText);

            player?.DrawCharacterStatus(posY);

            Manager.Instance.UI.DrawUIMessageBox();
            Manager.Instance.UI.PrintTextBoxMessage("[ 0. 나가기 ]", 0, ConsoleColor.Blue);

            int selectNumber = Utilities.CheckValidInput(0, 0);
        }
        #endregion

        #region Inventory Menu
        public static void PrintInventoryMenu(Player player)
        {
            int posY = Utilities.PrintTitle(ResourceKeys.InventoryMenuText);

            // 인벤토리 테이블을 그리는 메서드
            player?.Inventory?.DrawTable(posY + 1);

            Manager.Instance.UI.DrawUIMessageBox();
            Manager.Instance.UI.PrintTextBoxMessage(" [ 1. 장착관리 ]  [ 2. 정렬 ]  [ 0. 나가기 ]", 0, ConsoleColor.Blue);

            int selectNumber = Utilities.CheckValidInput(0, 2);
            switch (selectNumber)
            {
                case 0:
                    break;
                case 1:
                    player?.Inventory?.DrawTable(posY + 1, true);
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

        #region Shop Menu
        public static void PrintShopMenu(Player player)
        {
            int posY = Utilities.PrintTitle(ResourceKeys.ShopMenuText);

            Shop.DrawShopTable(posY + 1, player);
        }
        
        #endregion
    }
}