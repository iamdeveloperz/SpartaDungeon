using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public class GameManager : Singleton<GameManager>
    {
        #region Member Variables
        private const string PLAYER_DATA_PATH = "Player.json";

        private static SceneManager? _scene = null;
        private static UIManager? _ui = null;

        private static Player? _player = null;
        private static LinkedList<Item>? _items = null;

        public bool IsGameExit { get; set; } = false;
        public bool IsResourceLoad { get; set; } = false;
        #endregion

        #region Properties
        public SceneManager Scene {get { return (_scene == null) ? _scene = new SceneManager() : _scene; } } 
        public UIManager UI { get { return (_ui == null) ? _ui = new UIManager() : _ui; } }
        public Player Player { get { return _player; } }
        #endregion

        public GameManager()
        {
            _scene = new SceneManager();
            _ui = new UIManager();

            _player = new Player();
            _items = new LinkedList<Item>();
        }

        #region Main Methods
        public void CreatePlayer(string playerName)
        {
            // 플레이어 초기 데이터 설정
            if (_player == null) throw new NullReferenceException("Player Not Allocated.");

            _player.CreatePlayer(playerName);
        }

        public bool LoadPlayer()
        {
            if (_player == null) throw new NullReferenceException("Player Not Allocated.");

            return _player.LoadPlayer();
        }

        // 아이템 정보 불러오기
        public bool LoadItemsData()
        {
            LinkedList<ItemData> itemDatas = new LinkedList<ItemData>();
            itemDatas = Utilities.LoadFromJsonToList<ItemData>(Utilities.ITEM_JSON_PATH);
            foreach(var itemData in itemDatas)
            {
                Item item = new Item();
                item.ApplyItemData(itemData);
                _items.AddLast(item);
            }

            return (itemDatas.Count != 0) ? true : false;
        }

        public Item GetItems(string itemName)
        {
            foreach(Item item in _items)
            {
                if (item.GetItemData().ItemName == itemName)
                    return item;
            }

            return null;
        }
        #endregion
    }
}
