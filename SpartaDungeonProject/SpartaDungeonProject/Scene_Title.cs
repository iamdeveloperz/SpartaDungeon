using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public enum E_MENU_ITEM
    {
        GAME_START,
        GAME_CREDIT,
        GAME_EXIT
    }

    public class Scene_Title : GameNode
    {
        #region Member Variables

        
        public const int MENU_TOP_ROW = 16;
        public E_MENU_ITEM menuItemSelector = E_MENU_ITEM.GAME_START;

        private ConsoleColor[] _myColors = new ConsoleColor[]
        {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Gray,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.White
        };

        private int _currentColorIndex = 0;
        private string[]? _titleText;

        private readonly object consoleLock = new object();

        #endregion

        #region Main Methods
        public override void Start()
        {
            base.Start();
            SettingFPS(2);

            _titleText = Utilities.ReadTextFileByLines(Utilities.TITLE_PATH);
            Console.CursorVisible = false;

            StartAsyncUpdate();
        }

        public override void Update()
        {
            base.Update();

            while (!Console.KeyAvailable)
                this.DrawSelectMenu();
            if(Console.KeyAvailable)
                GameManager.Instance.UI.ClearWidthToWidth(MENU_TOP_ROW, Console.WindowHeight - 1);

            this.SelectMenu();
        }

        protected override void AsyncUpdate(object? state)
        {
            base.AsyncUpdate(state);

            this.DrawTitleText();
        }
        #endregion

        #region Helper Methods
        public void DrawTitleText()     // 비동기 업데이트로 실행
        {
            const int START_ROW_POS = 1;

            lock (consoleLock)
            {
                Console.Clear();

                for (int i = 0; i < _titleText.Length; ++i)
                {
                    ConsoleColor currentColor = _myColors[_currentColorIndex % _myColors.Length];
                    GameManager.Instance.UI.PrintCenterAlignString(_titleText[i], i + START_ROW_POS, currentColor);

                    if (i % 2 == 0 && i != 0)
                        ++_currentColorIndex;
                }

                GameManager.Instance.UI.PrintCenterAlignString("[ 상/하 방향키를 사용하여 이동하세요. ]", 11, ConsoleColor.Cyan);
                GameManager.Instance.UI.PrintCenterAlignString("[ 선택은 (Space | Enter)를 눌러 할 수 있습니다. ]", 12, ConsoleColor.Cyan);
            }
        }

        public void DrawSelectMenu()
        {
            string[] menuItemString = { "S  T  A  R  T  !", "C  R  E  D  I  T", "E    X    I    T" };

            lock (consoleLock)
            {
                for (int idx = 0; idx < (int)menuItemString.Length; ++idx)
                {
                    ConsoleColor color = ConsoleColor.Gray;
                    if (idx == ((int)menuItemSelector))
                    {
                        menuItemString[idx] = "▶          " + menuItemString[idx] + "          ◀";
                        color = ConsoleColor.Blue;
                    }
                    GameManager.Instance.UI.PrintCenterAlignString(menuItemString[idx], MENU_TOP_ROW + (idx * 3), color);
                }
            }
        }

        public void SelectMenu()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    menuItemSelector = (menuItemSelector - 1 < E_MENU_ITEM.GAME_START) ?
                        E_MENU_ITEM.GAME_START : --menuItemSelector;
                    break;
                case ConsoleKey.DownArrow:
                    menuItemSelector = (menuItemSelector + 1 > E_MENU_ITEM.GAME_EXIT) ?
                        E_MENU_ITEM.GAME_EXIT : ++menuItemSelector;
                    break;
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    if (menuItemSelector == E_MENU_ITEM.GAME_START)
                        GameManager.Instance.Scene.LoadScene(Utilities.MAIN_SCENE_IDX);
                    else if (menuItemSelector == E_MENU_ITEM.GAME_CREDIT)
                        GameManager.Instance.Scene.LoadScene(Utilities.CREDIT_SCENE_IDX);
                    else
                        GameManager.Instance.IsGameExit = true;
                    break;
            }
}
        #endregion
    }
}
