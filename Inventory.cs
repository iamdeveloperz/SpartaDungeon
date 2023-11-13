
using Newtonsoft.Json;

namespace SpartaDungeon
{
    public class InventoryItem
    {
        [JsonProperty(PropertyName = "Data")] public Item Item { get; set; }
        public int ItemQuantities { get; set; }
        public bool IsEquipped { get; set; }
    }

    public class Inventory
    {
        #region Member Variables

        private Dictionary<string, InventoryItem> _items;

        [JsonProperty(PropertyName = "InventoryItems")] public Dictionary<string, InventoryItem> Items { get { return _items; } }

        #endregion

        #region Main Methods

        #endregion
    }
}
