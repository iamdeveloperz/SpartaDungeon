
using Framework;
using SpartaDungeon.Managers;

namespace SpartaDungeon
{
    public class Scene_Title : GameNode
    {
        #region Member Variables

        public const int MENU_TOP_ROW = 16;
        public E_TITLE_MENU _menuSelector;

        private ConsoleColor[]? _colorArray;
        private int _colorIndex;

        private string[]? _titleText;

        private object? _lockable;

        #endregion

        #region Parent Methods
        public override void Start()
        {
            base.Start();
            SettingFPS(2);

            this.InitResource();
            this.Initialize();
            StartAsyncUpdate();
        }

        public override void Update()
        {
            base.Update();

            this.DrawSelectUpdate();
        }

        protected override void AsyncUpdate(object? state)
        {
            base.AsyncUpdate(state);

            this.DrawTitleText();
        }

        public override void Reset()
        {
            base.Reset();
            _colorArray = null;
            _colorIndex = 0;
            _titleText = null;
            _menuSelector = E_TITLE_MENU.GAME_START;

            _lockable = null;
        }
        #endregion

        #region Initializer
        private void Initialize()
        {
            Console.CursorVisible = false;

            _colorArray = new ConsoleColor[]
            {
                ConsoleColor.Red,
                ConsoleColor.Yellow,
                ConsoleColor.Gray,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.White
            };

            _menuSelector = E_TITLE_MENU.GAME_START;

            _lockable = new object();
        }

        private void InitResource()
        {
            string titleText = Manager.Instance.Resource.GetTextResource(ResourceKeys.TitleText);

            _titleText = titleText.Split(new[] { "\n" }, StringSplitOptions.None);
        }
        #endregion

        #region Main Methods
        private void DrawSelectUpdate()
        {
            while (!Console.KeyAvailable)
                this.DrawSelectMenuItem();
            if (Console.KeyAvailable)
                Manager.Instance.UI.ClearRowToRow(
                    MENU_TOP_ROW, Manager.Instance.UI.consoleHeight - 1);

            this.SelectMenuItem();
        }
        private void DrawSelectMenuItem()
        {
            string[] menuItemText = { 
                "S  T  A  R  T  !", 
                "C  R  E  D  I  T", 
                "E    X    I    T" };

            if (_lockable == null)
                throw new NullReferenceException($"Null reference : {_lockable}");
            lock (_lockable)
            {
                for (int idx = 0; idx < (int)menuItemText.Length; ++idx)
                {
                    ConsoleColor color = ConsoleColor.Gray;
                    if (idx == ((int)_menuSelector))
                    {
                        menuItemText[idx] = "▶          " + menuItemText[idx] + "          ◀";
                        color = ConsoleColor.Blue;
                    }

                    Manager.Instance.UI.PrintTextAlignCenter(menuItemText[idx], MENU_TOP_ROW + (idx * 3), color);
                }
            }
        }

        private void SelectMenuItem()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch(keyInfo.Key)
            {
            case ConsoleKey.UpArrow:
                    _menuSelector = (_menuSelector - 1 < 0) ?
                        (E_TITLE_MENU)(Enum.GetValues(typeof(E_TITLE_MENU)).Length - 1) :
                        _menuSelector - 1;
                    break;
            case ConsoleKey.DownArrow:
                    _menuSelector = (_menuSelector + 1 >= (E_TITLE_MENU)Enum.GetValues(typeof(E_TITLE_MENU)).Length) ?
                        E_TITLE_MENU.GAME_START :
                        _menuSelector + 1;
                    break;
            case ConsoleKey.Spacebar:
            case ConsoleKey.Enter:
                    if (_menuSelector == E_TITLE_MENU.GAME_START)
                        Manager.Instance.Scene.LoadSceneIdx(Utilities.MAIN_SCENE_IDX);
                    else if (_menuSelector == E_TITLE_MENU.GAME_CREDIT)
                        Manager.Instance.Scene.LoadSceneIdx(Utilities.CREDIT_SCENE_IDX);
                    else
                        Manager.Instance.IsGameEnd = true;
                    break;
            }
        }
        #endregion

        // 비동기 업데이트동안 실행될 메소드
        #region Async Methods
        public void DrawTitleText()
        {
            const int START_ROW_POS = 1;
            const int TOP_CURSOR = 11;

            if (_lockable == null)
                throw new NullReferenceException($"Null reference : {_lockable}");
            lock(_lockable)
            {
                Console.Clear();
                for (int row = 0; row < _titleText?.Length; ++row)
                {
                    ConsoleColor currentColor = _colorArray?[_colorIndex % _colorArray.Length] ?? ConsoleColor.Gray;
                    Manager.Instance.UI.PrintTextAlignCenter(_titleText[row], row + START_ROW_POS, currentColor);

                    if (row % 2 == 0 && row != 0)
                        ++_colorIndex;
                }

                Manager.Instance.UI.PrintTextAlignCenter("[ 상/하 방향키를 사용하여 이동하세요. ]", TOP_CURSOR, ConsoleColor.Cyan);
                Manager.Instance.UI.PrintTextAlignCenter("[ 선택은 (Space | Enter)를 눌러 할 수 있습니다. ]", TOP_CURSOR + 1, ConsoleColor.Cyan);
            }
        }
        #endregion
    }
}