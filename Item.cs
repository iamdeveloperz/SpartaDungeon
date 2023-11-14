
using Newtonsoft.Json;

namespace SpartaDungeon
{
    public enum E_ITYPE
    {
        WEAPON,
        ARMOR,
        ACCESSORIES
    }

    public class Item
    {
        [JsonProperty(PropertyName = "UniqueID")] public string ItemID { get; set; }
        [JsonProperty(PropertyName = "ItemName")] public string ItemName { get; set; }
        [JsonProperty(PropertyName = "ItemType")] public E_ITYPE ItemType { get; set; }
        [JsonProperty(PropertyName = "Status")] public int Status { get; set; }
        [JsonProperty(PropertyName = "IsUpgrade")] public bool IsUpgrade { get; set; }
        [JsonProperty(PropertyName = "IsEnchant")] public bool IsEnchant { get; set; }
        [JsonProperty(PropertyName = "ItemDescription")] public string? ItemDescription { get; set; }
        [JsonProperty(PropertyName = "PurchasePrice")] public int PricePurchase { get; set; }
        [JsonProperty(PropertyName = "SellPrice")] public int PriceSell { get; set; }

        public void ApplyItemData(Item item)
        {
            // 모든 속성을 다른 Item 객체의 속성으로 설정
            this.ItemID = item.ItemID;
            this.ItemName = item.ItemName;
            this.ItemType = item.ItemType;
            this.Status = item.Status;
            this.IsUpgrade = item.IsUpgrade;
            this.IsEnchant = item.IsEnchant;
            this.ItemDescription = item.ItemDescription;
            this.PricePurchase = item.PricePurchase;
            this.PriceSell = item.PriceSell;
        }
    }
}
