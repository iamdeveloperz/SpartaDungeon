
using Framework;

namespace SpartaDungeon
{
    public class Manager : SingletonBase<Manager>
    {
        #region Member Variables

        private static Manager_Resource? _resource = null;
        private static Manager_Scene? _scene = null;
        private static Manager_UI? _uI = null;

        public bool IsGameEnd { get; set; } = false;

        #endregion

        #region Properties
        public Manager_Resource Resource { get { return _resource ??  (_resource = new Manager_Resource()); } }
        public Manager_Scene Scene { get { return _scene ?? (_scene = new Manager_Scene()); } }
        public Manager_UI UI { get { return _uI ?? (_uI = new Manager_UI()); } }
        #endregion

        #region Constructor & Initialize Managers
        public Manager()
        {
            _resource = new Manager_Resource();
            _scene = new Manager_Scene();
            _uI = new Manager_UI();
        }
        #endregion
    }
}
