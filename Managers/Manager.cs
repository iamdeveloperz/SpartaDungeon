
using Framework;

namespace SpartaDungeon.Managers
{
    public class Manager : SingletonBase<Manager>
    {
        #region Member Variables

        private static Manager_Resource? _resource = null;
        private static Manager_Scene? _scene = null;
        private static Manager_Data? _data = null;
        private static Manager_UI? _uI = null;
        private static PlayerManager? _playerManager = null;

        public bool IsGameEnd { get; set; } = false;

        #endregion

        #region Properties
        public Manager_Resource Resource { get { return _resource ?? (_resource = new Manager_Resource()); } }
        public Manager_Scene Scene { get { return _scene ?? (_scene = new Manager_Scene()); } }
        public Manager_Data Data { get { return _data ?? (_data = new Manager_Data()); } }
        public Manager_UI UI { get { return _uI ?? (_uI = new Manager_UI()); } }

        public PlayerManager PM { get { return _playerManager ?? (_playerManager = new PlayerManager()); } }
        #endregion

        #region Constructor & Initialize Managers
        public Manager()
        {
            _resource = new Manager_Resource();
            _scene = new Manager_Scene();
            _data = new Manager_Data();
            _uI = new Manager_UI();

            _playerManager = new PlayerManager();
        }
        #endregion
    }
}
