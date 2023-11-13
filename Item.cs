
using Newtonsoft.Json;

namespace SpartaDungeon
{
    public class Item
    {
        public enum E_ITYPE
        {
            WEAPON,
            ARMOR,
            ACCESSORIES
        }
        [JsonProperty(PropertyName = "UniqueID")] public string? ItemID { get; set; }
        [JsonProperty(PropertyName = "ItemName")] public string? ItemName { get; set; }
        [JsonProperty(PropertyName = "ItemType")] public E_ITYPE ItemType { get; set; }
        [JsonProperty(PropertyName = "Status")] public int Status { get; set; }
        [JsonProperty(PropertyName = "IsUpgrade")] public bool IsUpgrade { get; set; }
        [JsonProperty(PropertyName = "IsEnchant")] public bool IsEnchant { get; set; }
        [JsonProperty(PropertyName = "ItemDescription")] public string? ItemDescription { get; set; }
        [JsonProperty(PropertyName = "PurchasePrice")] public int PricePurchase { get; set; }
        [JsonProperty(PropertyName = "SellPrice")] public int PriceSell { get; set; }
    }
}
