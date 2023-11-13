using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpartaDungeonProject
{
    #region Player Data Serialize
    public enum E_JOBS
    {
        NONE = 0,
        WARRIOR,
        ARCHER,
        MAGICIAN,
        GUNNER,
        ASSASSIN
    }

    public class PlayerData
    {
        [JsonProperty(PropertyName = "Name")] public string? Name { get; set; }
        [JsonProperty(PropertyName = "Level")] public int Level { get; set; }
        [JsonProperty(PropertyName = "Jobs")] public E_JOBS Jobs { get; set; }
        [JsonProperty(PropertyName = "BaseDamage")] public float BaseDamage { get; set; }
        [JsonProperty(PropertyName = "AttackDamage")] public float AttackDamage { get; set; }
        [JsonProperty(PropertyName = "BaseArmor")] public float BaseArmor { get; set; }
        [JsonProperty(PropertyName = "DefenseArmor")] public float DefenseArmor { get; set; }
        [JsonProperty(PropertyName = "CurrentHp")] public float CurrentHp { get; set; }
        [JsonProperty(PropertyName = "MaxHp")] public float MaxHp { get; set; }
        [JsonProperty(PropertyName = "CurrentSt")] public float CurrentSt { get; set; }
        [JsonProperty(PropertyName = "MaxSt")] public float MaxSt { get; set; }
        [JsonProperty(PropertyName = "CurrentExp")] public float CurrentExp { get; set; }
        [JsonProperty(PropertyName = "MaxExp")] public float MaxExp { get; set; }
        [JsonProperty(PropertyName = "Gold")] public int Gold { get; set; }
    }
    #endregion

    public class Equipment
    {
        public Item Weapon { get; set; }
        public Item Armor { get; set; }
    }

    #region Player Class
    public class Player
    {
        #region Member Variables

        private PlayerData? _playerData;
        private Inventory? _inventory;
        private Equipment? _equipment;

        #endregion

        #region Main Methods
        public Player()
        {
            _playerData = new PlayerData();
            _inventory = new Inventory();
            _equipment = new Equipment();
        }

        public void CreatePlayer(string playerName)
        {
            if (_playerData == null || _inventory == null)
                throw new NullReferenceException("Player | Inventory is NullRefernce");

            // 플레이어 초기 데이터 설정
            _playerData.Name = playerName;
            this.InitPlayerData();

            Utilities.SaveToJson<PlayerData>(_playerData, Utilities.PLAYER_JSON_PATH);
            Utilities.SaveToJson<Inventory>(_inventory, Utilities.INVENTORY_JSON_PATH);
        }

        public bool LoadPlayer()
        {
            if (Utilities.IsJsonFile(Utilities.PLAYER_JSON_PATH) &&
                Utilities.IsJsonFile(Utilities.INVENTORY_JSON_PATH))
            {
                _playerData = Utilities.LoadFromJson<PlayerData>(Utilities.PLAYER_JSON_PATH);
                _inventory = Utilities.LoadFromJson<Inventory>(Utilities.INVENTORY_JSON_PATH);

                return true;
            }
            else
                return false;
        }

        private void InitPlayerData()
        {
            _playerData.Level = 1;
            _playerData.BaseDamage = 50;
            _playerData.AttackDamage = _playerData.BaseDamage;
            _playerData.BaseArmor = 5;
            _playerData.DefenseArmor = _playerData.BaseArmor;
            _playerData.Jobs = E_JOBS.NONE;
            _playerData.MaxHp = 1000;
            _playerData.CurrentHp = _playerData.MaxHp;
            _playerData.MaxSt = 250;
            _playerData.CurrentSt = _playerData.MaxSt;
            _playerData.MaxExp = 1000;
            _playerData.CurrentExp = 0;
            _playerData.Gold = 10000;

            _inventory.AddItemToInventory(Utilities.BEGINNER_ARMOR,
                GameManager.Instance.GetItems(Utilities.BEGINNER_ARMOR));
            _inventory.AddItemToInventory(Utilities.BEGINNER_WEAPON,
                GameManager.Instance.GetItems(Utilities.BEGINNER_WEAPON));
        }

        public void ApplyPlayerData(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public static string GetJobName(E_JOBS job)
        {
            switch (job)
            {
                case E_JOBS.NONE:
                    return "무직";
                case E_JOBS.WARRIOR:
                    return "전사";
                case E_JOBS.ARCHER:
                    return "궁수";
                case E_JOBS.MAGICIAN:
                    return "마법사";
                case E_JOBS.GUNNER:
                    return "건너";
                case E_JOBS.ASSASSIN:
                    return "암살자";
                default:
                    return "알 수 없음";
            }
        }

        public void PrintPlayerInfo()
        {
            Console.Clear();
            GameManager.Instance.UI.CreateUIMessageBox();

            int height = 1;
            GameManager.Instance.UI.PrintCenterAlignString("P L A Y E R   I N F O", height++, ConsoleColor.Blue);
            GameManager.Instance.UI.PrintCenterAlignString("▶  캐 릭 터   정 보  ◀", height++, ConsoleColor.White);
            string formattedName = "[ " + _playerData.Name + " ] 님의 스테이터스입니다.";
            GameManager.Instance.UI.PrintCenterAlignString(formattedName, ++height, ConsoleColor.Yellow);

            string formattedLevel = _playerData.Level.ToString("D2");
            height += 3;
            GameManager.Instance.UI.PrintAlignSetterString("LEVEL  .  " + formattedLevel, true, 3, height++, ConsoleColor.Magenta);
            GameManager.Instance.UI.PrintAlignSetterString("직업 : [ " + GetJobName(_playerData.Jobs) + "]", true, 3, height++);
            GameManager.Instance.UI.PrintAlignSetterString("기본 공격력 : " + _playerData.BaseDamage, true, 3, height++);
            GameManager.Instance.UI.PrintAlignSetterString("종합 공격력 : " + _playerData.AttackDamage, true, 3, height++);
            GameManager.Instance.UI.PrintAlignSetterString("기본 방어력 : " + _playerData.BaseArmor, true, 3,height++);
            GameManager.Instance.UI.PrintAlignSetterString("종합 방어력 : " + _playerData.DefenseArmor, true, 3, height++);

            height += 2;
            string foramttedValue = " [ " + _playerData.CurrentHp.ToString() + " ] [ " + _playerData.MaxHp.ToString() + " ] ";
            GameManager.Instance.UI.PrintAlignSetterString("HP  " + foramttedValue, true, 3, height++, ConsoleColor.Red);
            foramttedValue = " [ " + _playerData.CurrentSt.ToString() + " ] [ " + _playerData.MaxSt.ToString() + " ] ";
            GameManager.Instance.UI.PrintAlignSetterString("ST  " + foramttedValue, true, 3, height++, ConsoleColor.Blue);
            foramttedValue = " [ " + _playerData.CurrentExp.ToString() + " ] [ " + _playerData.MaxExp.ToString() + " ] ";
            GameManager.Instance.UI.PrintAlignSetterString("EXP  " + foramttedValue, true, 3, height++, ConsoleColor.Green);
            GameManager.Instance.UI.PrintAlignSetterString("GOLD  [ " + _playerData.Gold.ToString() + "G ] ", true, 3, ++height, ConsoleColor.Yellow);

            height += 3;
            GameManager.Instance.UI.PrintAlignSetterString("0. 나가기", true, 3, height);

            GameManager.Instance.UI.PrintUIBoxMessage("원하시는 메뉴를 선택하세요. (메뉴에 없는 내용은 동작하지 않습니다)");
            GameManager.Instance.UI.PrintUIBoxMessage("나가기 버튼은 '0'외에도 ESC를 눌러 나갈 수 있습니다. ", 1);

            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                if (ConsoleKey.Escape == keyInfo.Key || 
                    ConsoleKey.D0 == keyInfo.Key || 
                    ConsoleKey.NumPad0 == keyInfo.Key)
                    break;
            }
        }

        public void EquipItem(string itemName)
        {
            if (_inventory.ItemData.ContainsKey(itemName))
            {
                var iItem = _inventory.ItemData[itemName];
                iItem.IsEquipped = true;

                // 이후 아이템의 타입에 따라 무기 또는 갑옷을 장착합니다.
                if (iItem.Item.GetItemData().ItemType == ItemData.E_ITYPE.WEAPON)
                {
                    EquipWeapon(iItem.Item);
                }
                else if (iItem.Item.GetItemData().ItemType == ItemData.E_ITYPE.ARMOR)
                {
                    EquipArmor(iItem.Item);
                }

                Utilities.SaveToJson<Inventory>(_inventory, Utilities.INVENTORY_JSON_PATH);
            }
        }

        public void UnequipItem(string itemName)
        {
            if (_inventory.ItemData.ContainsKey(itemName))
            {
                var iItem = _inventory.ItemData[itemName];
                iItem.IsEquipped = false;

                // 이후 아이템의 타입에 따라 무기 또는 갑옷을 장착합니다.
                if (iItem.Item.GetItemData().ItemType == ItemData.E_ITYPE.WEAPON)
                {
                    UnequipWeapon();
                }
                else if (iItem.Item.GetItemData().ItemType == ItemData.E_ITYPE.ARMOR)
                {
                    UnequipArmor();
                }

                Utilities.SaveToJson<Inventory>(_inventory, Utilities.INVENTORY_JSON_PATH);
            }
        }

        public void EquipWeapon(Item item)
        {
            _equipment.Weapon = item;
        }

        public void EquipArmor(Item item)
        {
            _equipment.Armor = item;
        }

        public void UnequipWeapon()
        {
            _equipment.Weapon = null;
        }

        public void UnequipArmor()
        {
            _equipment.Armor = null;
        }

        public Inventory GetInventory() { return _inventory; }
        //public PlayerData? GetPlayerData() { return _playerData; }
        #endregion
    }
    #endregion
}
