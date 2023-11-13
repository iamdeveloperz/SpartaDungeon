using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public class InventoryItem
    {
        [JsonProperty(PropertyName = "Item")]
        public Item? Item { get; set; }

        [JsonProperty(PropertyName = "ItemQuantities")]
        public int ItemQuantities { get; set; }

        [JsonProperty(PropertyName = "isEquipped")]
        public bool IsEquipped { get; set; }
    }

    public class Inventory
    {
        private Dictionary<string, InventoryItem> _inventory;

        public Inventory()
        {
            _inventory = new Dictionary<string, InventoryItem>();
        }

        [JsonProperty(PropertyName = "Inventory")]
        public Dictionary<string, InventoryItem> ItemData { get { return _inventory; } }

        #region Main Methods
        public void AddItemToInventory(string itemName, Item item)
        {
            if (!_inventory.ContainsKey(itemName))
            {
                InventoryItem inventoryItem = new InventoryItem
                {
                    Item = item,
                    ItemQuantities = 1,
                    IsEquipped = false
                };

                _inventory[itemName] = inventoryItem;
            }
            else
            {
                _inventory[itemName].ItemQuantities++;
            }

            Utilities.SaveToJson<Inventory>(this, Utilities.INVENTORY_JSON_PATH);
        }

        public void RemoveItemFromInventory(string itemName)
        {
            if (_inventory.ContainsKey(itemName))
            {
                if (_inventory[itemName].ItemQuantities > 1)
                {
                    _inventory[itemName].ItemQuantities--;
                }
                else
                {
                    _inventory.Remove(itemName);
                }

                Utilities.SaveToJson<Inventory>(this, Utilities.INVENTORY_JSON_PATH);
            }
        }
        #endregion

        #region Helper Methods
        public void PrintInventory()
        {
            Console.Clear();
            GameManager.Instance.UI.CreateUIMessageBox();

            int height = 1;
            GameManager.Instance.UI.PrintCenterAlignString("■■ P L A Y E R   I N V E N T O R Y ■■", height++, ConsoleColor.Blue);
            GameManager.Instance.UI.PrintCenterAlignString("플레이어의 인벤토리 메뉴", height++, ConsoleColor.White);
            GameManager.Instance.UI.PrintCenterAlignString("[ 현재 보유 중인 아이템을 관리할 수 있습니다. ]", ++height, ConsoleColor.Yellow);

            height += 3;
            this.ShowTableListSetting(height);

            GameManager.Instance.UI.PrintUIBoxMessage("  [ 1. 장착 관리 ]   [ 0. 나가기 (ESC) ]", 0, ConsoleColor.Blue);
            GameManager.Instance.UI.PrintUIBoxMessage("메뉴를 선택 해주세요.", 1);

            while(true)
            {
                var keyInfo = Console.ReadKey();

                if (ConsoleKey.D0 == keyInfo.Key || ConsoleKey.NumPad0 == keyInfo.Key || ConsoleKey.Escape == keyInfo.Key)
                    break;
                else if (ConsoleKey.D1 == keyInfo.Key || ConsoleKey.NumPad1 == keyInfo.Key)
                {
                    this.EquipmentManageMode();
                    break;
                }
            }
        }

        private void ShowTableListSetting(int top)
        {
            List<string> columns = new List<string>
            {
                "ItemName",
                "ItemType",
                "ItemStat",
                "ItemDescription",
                "ItemQuantities",
            };

            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

            foreach (var inventoryItem in _inventory)
            {
                Item item = inventoryItem.Value.Item;
                Dictionary<string, string> rowData = new Dictionary<string, string>
                {
                    { "ItemName", item.GetItemData().ItemName },
                    { "ItemType", item.GetItemData().ItemType.ToString() },
                    { "ItemStat", item.GetItemData().ItemStat.ToString() },
                    { "ItemDescription", item.GetItemData().ItemDesc },
                    { "ItemQuantities", inventoryItem.Value.ItemQuantities.ToString() }
                };
                data.Add(rowData);
            }

            GameManager.Instance.UI.ShowTableList(columns, data, top);
        }

        public void EquipmentManageMode()
        {

        }
        #endregion
    }
}