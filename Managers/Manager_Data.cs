
using Framework;
using Newtonsoft.Json;
using System.Numerics;

namespace SpartaDungeon.Managers
{
    public class Manager_Data
    {
        #region Member Variables
        private List<Item>? _items;
        #endregion

        public Manager_Data() 
        {
            _items = new List<Item>();
        }

        #region Player
        public void SavePlayer(Player player, ResourceKeys key)
        {
            var playerPath = Path.Combine(Utilities.GetResourceFolderPath(), Utilities.PLAYER_JSON_PATH); 

            string json = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText(playerPath, json);
        }

        public bool LoadPlayer(Player player, ResourceKeys key)
        {
            var playerText = Manager.Instance.Resource.GetTextResource(key);

            if (!string.IsNullOrEmpty(playerText))
            {
                try
                {
                    Player? loadedPlayer = JsonConvert.DeserializeObject<Player>(playerText);
                    if (loadedPlayer != null)
                    {
                        player.SettingPlayerInfo(loadedPlayer.PlayerData, loadedPlayer.Inventory, loadedPlayer.Equipment);
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Player Load Failed. : {ex.Message}");
                }
            }
            return false;
        }
        #endregion

        #region Inventory
        public void SaveInventory(Inventory inventory, string path)
        {
            string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
            File.WriteAllText(Utilities.PLAYER_JSON_PATH, json);
        }
        #endregion

        #region Item
        public Item? GetItemData(string itemId)
        {
            return _items.FirstOrDefault(item => item.ItemID == itemId);
        }

        public void LoadItemData()
        {
            List<Item>? items;
            items = LoadJsonFromItemList<Item>();

            if (items == null)
                throw new NullReferenceException($"Items is null {items}");
            foreach (var item in items)
            {
                Item newItem = new Item();
                newItem.ApplyItemData(item);
                _items.Add(newItem);
            }
        }

        public List<Item>? LoadJsonFromItemList<Item>()
        {
            string itemText = Manager.Instance.Resource.GetTextResource(ResourceKeys.ItemList);

            List<Item>? items = null;
            try
            {
                if(!string.IsNullOrEmpty(itemText))
                {
                    items = new List<Item>();
                    items = JsonConvert.DeserializeObject<List<Item>>(itemText);
                }
            }
            catch(Exception ex)
            {
                Console.Write($"Error!! ItemList.json {ex.Message}");
            }

            return items;
        }

        public List<Item>? GetItemList() { return _items; }
        #endregion
    }
}
