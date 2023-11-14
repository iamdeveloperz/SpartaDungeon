
using Framework;
using Newtonsoft.Json;
using SpartaDungeon.Managers;
using System.Drawing;

namespace SpartaDungeon
{
    public class InventoryItem
    {
        [JsonProperty(PropertyName = "Data")] public Item Item { get; set; }
        public int ItemQuantities { get; set; }
        public bool IsEquipped { get; set; }

        public InventoryItem(Item item)
        {
            Item = item;
            ItemQuantities = 1;
            IsEquipped = false;
        }
    }

    public class Inventory
    {
        #region Member Variables

        private Dictionary<string, InventoryItem> _items;

        [JsonProperty(PropertyName = "InventoryItems")] public Dictionary<string, InventoryItem> Items { get { return _items; } }

        #endregion

        #region Main Methods
        public Inventory() {
            _items = new Dictionary<string, InventoryItem>();
        }

        public void AddItemToInventory(Item item)
        {
            string? itemId = item.ItemID;

            if (_items.ContainsKey(itemId))
            {
                _items[itemId].ItemQuantities++;
            }
            else
            {
                InventoryItem inventoryItem = new InventoryItem(item);
                _items[itemId] = inventoryItem;
            }
        }

        public void RemoveItem(Item item)
        {
            string? itemId = item.ItemID;

            if (_items.ContainsKey(itemId))
            {
                _items[itemId].ItemQuantities--;
                if (_items[itemId].ItemQuantities <= 0)
                    _items.Remove(itemId);
            }
        }

        public InventoryItem GetItemByIndex(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                var item = _items.Values.ElementAt(index);
                return item;
            }
            else
                throw new IndexOutOfRangeException("Inventory : ITEM index out of range.");
        }

        #endregion

        #region Helper Methods
        public void DrawTable(int posY, bool isEquipMode = false)
        {
            int consoleWidth = Manager.Instance.UI.consoleWidth;
            int numRows = _items.Count;
            int consoleDiv = consoleWidth / 10;

            // Column Width Setting
            int widthID = 14;
            int widthName = 20;
            int widthTypeStat = 20;
            int widthQuantities = 6;
            int widthDescription = consoleWidth - consoleDiv * 2 - widthID - widthName - widthTypeStat - widthQuantities - 1;

            Manager.Instance.UI.ClearRowToRow(9, Manager.Instance.UI.boxPosY - 2);

            // Table Header
            string headerRow = "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("식별번호", widthID - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("아이템 이름", widthName - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("아이템타입/스탯", widthTypeStat - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("수량", widthQuantities - 1) + "│";
            headerRow += Manager.Instance.UI.GetPaddingToMessage("설명", widthDescription - 1) + "│";

            // Table Border
            Console.SetCursorPosition(consoleDiv, posY);
            Manager.Instance.UI.PrintTextToColor(new string('─', consoleWidth - consoleDiv * 2), ConsoleColor.Cyan);

            // Table Header Columns
            Console.SetCursorPosition(consoleDiv, posY + 1);
            Manager.Instance.UI.PrintTextToColor(headerRow, ConsoleColor.Cyan);

            Console.SetCursorPosition(consoleDiv, posY + 2);
            Manager.Instance.UI.PrintTextToColor(new string('─', consoleWidth - consoleDiv * 2), ConsoleColor.Cyan);

            int i = 0;
            foreach (var invenItem in _items.Values)
            {
                string dataRow = "│";
                if (isEquipMode)
                    dataRow = $"{(i + 1)}. │";
                string itemType = invenItem.Item.ItemType.ToString();
                string itemStat = invenItem.Item.Status.ToString();

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

                string formattedItemName = invenItem.Item.ItemName;
                if (invenItem.IsEquipped)
                    formattedItemName += " (E)";
                dataRow += Manager.Instance.UI.GetPaddingToMessage(invenItem.Item.ItemID, widthID - 1) + "│";
                dataRow += Manager.Instance.UI.GetPaddingToMessage(formattedItemName, widthName - 1) + "│";
                dataRow += Manager.Instance.UI.GetPaddingToMessage($"{itemType}", widthTypeStat - 1) + "│";
                dataRow += Manager.Instance.UI.GetPaddingToMessage(invenItem.ItemQuantities.ToString(), widthQuantities - 1) + "│";
                dataRow += Manager.Instance.UI.GetPaddingToMessage(invenItem.Item.ItemDescription, widthDescription - 1) + "│";

                Console.SetCursorPosition(consoleDiv, posY + 3 + i);
                if (isEquipMode)
                    Console.SetCursorPosition(consoleDiv - 3, posY + 3 + i);
                Manager.Instance.UI.PrintTextToColor(dataRow, ConsoleColor.Gray);
                ++i;
            }

            // Table Border
            Console.SetCursorPosition(consoleDiv, posY + numRows + 3);
            Manager.Instance.UI.PrintTextToColor(new string('─', consoleWidth - consoleDiv * 2), ConsoleColor.Gray);

            if (isEquipMode)
            {
                Manager.Instance.UI.ClearUIMessageBox();
                Manager.Instance.UI.PrintTextBoxMessage("[장착]할 아이템 번호를 입력하세요.", 0, ConsoleColor.Blue);
                Manager.Instance.UI.PrintTextBoxMessage("0번을 누르면 장착관리 모드를 해제합니다.", 1, ConsoleColor.Gray);

                int selectNumber = Utilities.CheckValidInput(0, i);
                InventoryItem equipmentItem;

                if (selectNumber == 0)
                    ViewTable.PrintInventoryMenu(Manager.Instance.PM.GetPlayer());
                else
                {
                    equipmentItem = this.GetItemByIndex(selectNumber - 1);
                    Manager.Instance.PM.Equip(equipmentItem);
                    this.DrawTable(posY, true);
                }   
            }
        }
        #endregion
    }
}
