
using Framework;

namespace SpartaDungeon
{
    public class Scene_Main : GameNode
    {
        #region Member Variables
        private string[]? _textMainGame = null;
        #endregion

        #region Parent Methods
        public override void Start()
        {
            base.Start();

            this.InitResource();
        }

        public override void Update()
        {
            base.Update();

            this.PrintTextMainGame();
            this.SelectMainGameMenu();
        }

        public override void Reset()
        {
            base.Reset();

            _textMainGame = null;
        }
        #endregion

        #region Initializer
        public void InitResource()
        {
            string mainText = Manager.Instance.Resource.GetTextResource(ResourceKeys.MainGameMenuText);

            _textMainGame = mainText.Split(new[] { "\n" }, StringSplitOptions.None);
        }

        public void LoadPlayerData(Player player)
        {
            if(!Manager.Instance.Data.IsLoadPlayerComplete() && player != null)
            {
                
            }
        }
        #endregion

        #region Main Methods
        private void PrintTextMainGame()
        {
            Console.Clear();

            Manager.Instance.UI.DrawUIMessageBox();

            const int START_POS = 1;
            for(int i = 0; i < _textMainGame?.Length; ++i)
            {
                ConsoleColor color = ConsoleColor.Gray;
                if (i < 4 && i >= 0) color = ConsoleColor.Yellow;
                else if (i < 8 && i >= 4) color = ConsoleColor.Cyan;

                if (_textMainGame[i].Contains("3.")) color = ConsoleColor.Blue;
                else if (_textMainGame[i].Contains("4.")) color = ConsoleColor.Red;
                else if (_textMainGame[i].Contains("0.")) color = ConsoleColor.Magenta;

                Manager.Instance.UI.PrintTextAlignCenter(_textMainGame[i], i + START_POS, color);
            }

            Manager.Instance.UI.PrintTextBoxMessage("메뉴를 선택 해주세요. (엔터키를 누를 필요 없습니다)");
        }

        private void SelectMainGameMenu()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:        // 상태 보기
                    //Manager.Instance.Player.PrintPlayerInfo();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:        // 인벤토리
                    //Manager.Instance.Player.GetInventory().PrintInventory();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    //GameManager.Instance.Scene.LoadScene(Utilities.TITLE_SCENE_IDX);
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                case ConsoleKey.Escape:
                    Console.Clear();
                    Manager.Instance.UI.PrintTextAlignCenterToCenter("프로그램이 종료됩니다.");
                    Manager.Instance.IsGameEnd = true;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
