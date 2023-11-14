
using Newtonsoft.Json;
using Framework;
using SpartaDungeon.Managers;

namespace SpartaDungeon
{
    #region Serialization Data
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
        public InventoryItem? Weapon { get; set; }
        public InventoryItem? Armor { get; set; }
        public InventoryItem? Accessories { get; set; }
    }

    #region Player Class
    public class Player
    {
        #region Member Variables
        private PlayerData? _playerData = null;
        private Inventory? _inventory = null;
        private Equipment? _equipment = null;

        public PlayerData? PlayerData { get { return _playerData; } set { _playerData = value; } }
        public Inventory? Inventory { get { return _inventory; } set { _inventory = value; } }
        public Equipment? Equipment { get { return _equipment; } set { _equipment = value; } }
        #endregion

        #region Main Methods
        public Player()
        {
            _playerData = new PlayerData();
            _inventory = new Inventory();
            _equipment = new Equipment();
        }

        public void SettingPlayerInfo(PlayerData playerData, Inventory inventory, Equipment equipment)
        {
            _playerData = playerData;
            _inventory = inventory;
            _equipment = equipment;
        }

        public void InitPlayerData(string playerName)
        {
            if (_playerData == null)
                throw new NullReferenceException("Null Reference : PlayerData");
            _playerData.Name = playerName;
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

            _inventory.AddItemToInventory(Manager.Instance.Data.GetItemData("w001"));
            _inventory.AddItemToInventory(Manager.Instance.Data.GetItemData("w002"));
            _inventory.AddItemToInventory(Manager.Instance.Data.GetItemData("a001"));
        }
        #endregion

        #region Helper Methods
        public void DrawCharacterStatus(int posY)
        {
            string formattedName = "[ " + _playerData?.Name + " ] 님의 스테이터스입니다.";
            Manager.Instance.UI.PrintTextAlignCenter(formattedName, ++posY, ConsoleColor.Yellow);

            string formattedLevel = _playerData?.Level.ToString("D2");
            string formattedJob = _playerData?.Jobs switch
            {
                E_JOBS.NONE => "무직",
                E_JOBS.WARRIOR => "전사",
                E_JOBS.ARCHER => "궁수",
                E_JOBS.GUNNER => "거너",
                E_JOBS.ASSASSIN => "암살자",
                E_JOBS.MAGICIAN => "견습 마법사",
                _ => ""
            };

            string formattedBaseDamge = _playerData.BaseDamage.ToString();
            string formattedBaseArmor = _playerData.BaseArmor.ToString();
            if (_equipment?.Weapon != null)
                formattedBaseDamge += $" ( +{_equipment.Weapon.Item.Status} )";
            if (_equipment?.Armor != null)
                formattedBaseArmor += $" ( +{_equipment.Armor.Item.Status} )";

            posY += 2;
            Manager.Instance.UI.PrintTextAlignLeftRight("LEVEL  .  " + formattedLevel, true, 3, posY++, ConsoleColor.Magenta);
            Manager.Instance.UI.PrintTextAlignLeftRight("직업 : [ " + formattedJob + " ]", true, 3, posY++);
            Manager.Instance.UI.PrintTextAlignLeftRight("기본 공격력 : " + _playerData.BaseDamage, true, 3, posY++);
            if (_equipment?.Weapon != null)
                Manager.Instance.UI.PrintTextToColor($" ( +{_equipment.Weapon.Item.Status} )", ConsoleColor.Yellow);
            Manager.Instance.UI.PrintTextAlignLeftRight("종합 공격력 : " + _playerData.AttackDamage, true, 3, posY++);
            Manager.Instance.UI.PrintTextAlignLeftRight("기본 방어력 : " + _playerData.BaseArmor, true, 3, posY++);
            if (_equipment?.Armor != null)
                Manager.Instance.UI.PrintTextToColor($" ( +{_equipment.Armor.Item.Status} )", ConsoleColor.Yellow);
            Manager.Instance.UI.PrintTextAlignLeftRight("종합 방어력 : " + _playerData.DefenseArmor, true, 3, posY++);

            posY += 2;
            string foramttedValue = " [ " + _playerData.CurrentHp.ToString() + " ] [ " + _playerData.MaxHp.ToString() + " ] ";
            Manager.Instance.UI.PrintTextAlignLeftRight("HP  " + foramttedValue, true, 3, posY++, ConsoleColor.Red);
            foramttedValue = " [ " + _playerData.CurrentSt.ToString() + " ] [ " + _playerData.MaxSt.ToString() + " ] ";
            Manager.Instance.UI.PrintTextAlignLeftRight("ST  " + foramttedValue, true, 3, posY++, ConsoleColor.Blue);
            foramttedValue = " [ " + _playerData.CurrentExp.ToString() + " ] [ " + _playerData.MaxExp.ToString() + " ] ";
            Manager.Instance.UI.PrintTextAlignLeftRight("EXP  " + foramttedValue, true, 3, posY++, ConsoleColor.Green);
            Manager.Instance.UI.PrintTextAlignLeftRight("GOLD  [ " + _playerData.Gold.ToString() + "G ] ", true, 3, ++posY, ConsoleColor.Yellow);
        }
        #endregion
    }
    #endregion

    #region Player Manager
    public class PlayerManager
    {
        #region Player
        private Player? _player = null;
        #endregion

        #region Main Methods
        public void CreatePlayerCharacter()
        {
            if (_player == null)
                throw new NullReferenceException("Player not setting. -Player Manager-");

            this.DrawCreatePlayerPopup();
        }

        public void Equip(InventoryItem item)
        {
            if (_player?.Equipment == null)
                throw new NullReferenceException("Equipment Not Allocated.");

            if (item.IsEquipped)
            {
                Unequip(item.Item.ItemType);
                item.IsEquipped = false;
            }
            else
            {
                switch (item.Item.ItemType)
                {
                    case E_ITYPE.WEAPON:
                        if (_player.Equipment.Weapon != null)
                            Unequip(E_ITYPE.WEAPON);
                        _player.Equipment.Weapon = item;
                        _player.PlayerData.AttackDamage += _player.Equipment.Weapon.Item.Status;
                        _player.Equipment.Weapon.IsEquipped = true;
                        break;
                    case E_ITYPE.ARMOR:
                        if (_player.Equipment.Armor != null)
                            Unequip(E_ITYPE.ARMOR);
                        _player.Equipment.Armor = item;
                        _player.PlayerData.DefenseArmor += _player.Equipment.Armor.Item.Status;
                        _player.Equipment.Armor.IsEquipped = true;
                        break;
                    case E_ITYPE.ACCESSORIES:
                        if (_player.Equipment.Accessories != null)
                            Unequip(E_ITYPE.ACCESSORIES);
                        _player.Equipment.Accessories = item;
                        _player.Equipment.Accessories.IsEquipped = true;
                        break;
                }
            }
            this.SavePlayerData();
        }

        private void Unequip(E_ITYPE itemType)
        {
            if (_player?.Equipment == null)
                throw new NullReferenceException("Equipment Not Allocated.");

            switch (itemType)
            {
                case E_ITYPE.WEAPON:
                    if (_player.Equipment.Weapon != null)
                    {
                        _player.PlayerData.AttackDamage -= _player.Equipment.Weapon.Item.Status;
                        _player.Equipment.Weapon.IsEquipped = false;
                        _player.Equipment.Weapon = null;
                    }
                    break;
                case E_ITYPE.ARMOR:
                    if (_player.Equipment.Armor != null)
                    {
                        _player.PlayerData.DefenseArmor -= _player.Equipment.Armor.Item.Status;
                        _player.Equipment.Armor.IsEquipped = false;
                        _player.Equipment.Armor = null;
                    }
                    break;
                case E_ITYPE.ACCESSORIES:
                    if (_player.Equipment.Accessories != null)
                    {
                        _player.Equipment.Accessories.IsEquipped = false;
                        _player.Equipment.Accessories = null;
                    }
                    break;
            }

            this.SavePlayerData();
        }

        private void SavePlayerData()
        {
            Manager.Instance.Data.SavePlayer(_player, ResourceKeys.Player);
        }
        #endregion

        #region Getter & Setter
        public void SetPlayer(Player? player) { _player = player; }
        public Player? GetPlayer() { return _player; }
        #endregion

        #region Helper Methods
        private void DrawCreatePlayerPopup()
        {
            Console.Clear();

            Manager.Instance.UI.ClearUIMessageBox();
            Manager.Instance.UI.DrawUIMessageBox();

            string createTexts = Manager.Instance.Resource.GetTextResource(ResourceKeys.CreatePlayerText);
            if (string.IsNullOrEmpty(createTexts))
                throw new Exception($"Resource Is Empty {createTexts}");
            string[] lines = createTexts.Split(new[] { "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; ++i)
            {
                ConsoleColor color = ConsoleColor.Gray;

                if (lines[i].Contains("처음")) color = ConsoleColor.Cyan;
                Manager.Instance.UI.PrintTextAlignCenter(lines[i], i + 1, color);
            }

            while(true)
            {
                Manager.Instance.UI.ClearUIMessageBox();
                Manager.Instance.UI.PrintTextBoxMessage("이름을 입력해주세요 >> ");
                Console.CursorVisible = true;
                string? playerName = Console.ReadLine();

                Manager.Instance.UI.PrintTextBoxMessage("입력하신 이름이 맞습니까? [Y/N] >> ", 1);
                string? select = Console.ReadLine();

                if (select == "Y" || select == "y")
                {
                    _player?.InitPlayerData(playerName);
                    this.SavePlayerData();
                    break;
                }
                else if (select == "N" || select == "n")
                    Manager.Instance.UI.ClearUIMessageBox();
                else
                {
                    Manager.Instance.UI.ClearUIMessageBox();
                    Manager.Instance.UI.PrintTextBoxMessage("올바른 값을 입력하세요. (아무키나 누르시오)", 0);
                    Console.ReadKey();
                }
            }
        }
        #endregion
    }
    #endregion
}