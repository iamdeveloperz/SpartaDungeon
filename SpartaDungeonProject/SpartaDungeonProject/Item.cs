using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public class ItemData
    {
        public enum E_ITYPE
        {
            WEAPON,
            ARMOR,
            ACCESSORIES
        }
        [JsonProperty(PropertyName = "ItemName")] public string? ItemName { get; set; }
        [JsonProperty(PropertyName = "ItemType")] public E_ITYPE ItemType { get; set; }
        [JsonProperty(PropertyName = "ItemStat")] public int ItemStat { get; set; }
        [JsonProperty(PropertyName = "ItemDescription")] public string? ItemDesc { get; set; }
        [JsonProperty(PropertyName = "PurchasePrice")] public int PricePurchase { get; set; }
        [JsonProperty(PropertyName = "SellPrice")] public int PriceSell { get; set; }
        //[JsonProperty(PropertyName = "Quantities")] public int Quantities { get; set; }
    }

    public class Item
    {
        [JsonProperty(PropertyName = "ItemData")] private ItemData? _itemData;

        public Item()
        {
            _itemData = new ItemData();
        }

        public void ApplyItemData(ItemData itemData)
        {
            _itemData = itemData;
        }

        public ItemData? GetItemData() { return _itemData; }
    }
}
