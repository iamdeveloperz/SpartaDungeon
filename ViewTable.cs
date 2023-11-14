
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
            for(i = 0; i < textInventory.Length; ++i)
            {
                ConsoleColor color = ConsoleColor.Gray;
                if (i < 3) color = ConsoleColor.Yellow;
                else if (i >= 3 && i < 5) color = ConsoleColor.Cyan;

                Manager.Instance.UI.PrintTextAlignCenter(textInventory[i], i + START_POS, color);
            }

            // 테이블
            player?.Inventory?.DrawTable(i + 1);

            Manager.Instance.UI.DrawUIMessageBox();
            Manager.Instance.UI.PrintTextBoxMessage(" [ 1. 장착관리 ]  [ 0. 나가기 ]", 0, ConsoleColor.Blue);

            int selectNumber = Utilities.CheckValidInput(0, 1);
            switch (selectNumber)
            {
                case 0:
                    break;
                case 1:
                    player?.Inventory?.DrawTable(i + 1, true);
                    break;
            }
        }
        #endregion
    }
}