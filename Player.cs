
using Newtonsoft.Json;
using Framework;

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
        public Item Weapon { get; set; }
        public Item Armor { get; set; }
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

        }

        public void InitPlayerData()
        {
            if (_playerData == null)
                throw new NullReferenceException("Null Reference : PlayerData");
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

            
        }
        #endregion

        #region Getter & Setter
        public void SetPlayer(Player? player) { _player = player; }
        public Player? GetPlayer() { return _player; }
        #endregion

        #region Helper Methods
        private void CreatePlayerName()
        {
            Console.Clear();

            Manager.Instance.UI.ClearUIMessageBox();

            string createTexts = Manager.Instance.Resource.GetTextResource(ResourceKeys.CreatePlayerText);
            if (string.IsNullOrEmpty(createTexts))
                throw new Exception($"Resource Is Empty {createTexts}");
            string[] lines = createTexts.Split(new[] { "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; ++i)
            {
                ConsoleColor color = ConsoleColor.Gray;

                if (lines[i].Contains("타이틀")) color = ConsoleColor.Cyan;
                Manager.Instance.UI.PrintTextAlignCenter(lines[i], i + 1, color);
            }

            while(true)
            {
                Console.CursorVisible = true;
                Manager.Instance.UI.ClearUIMessageBox();
                Manager.Instance.UI.PrintTextBoxMessage("이름을 입력해주세요 >> ");
            }
        }
        #endregion
    }
    #endregion
}