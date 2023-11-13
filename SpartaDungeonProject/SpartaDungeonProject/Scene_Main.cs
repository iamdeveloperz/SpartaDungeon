using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public class Scene_Main : GameNode
    {

        #region Member Variables


        #endregion

        #region Main Methods
        public override void Start()
        {
            base.Start();

            if (!GameManager.Instance.LoadPlayer())
                this.CreatePlayerName();
            Console.CursorVisible = false;
        }

        public override void Update()
        {
            base.Update();

            Console.Clear();
            GameManager.Instance.UI.CreateUIMessageBox();
            PrintMainMessage();
        }
        #endregion

        #region Helper Methods
        public void PrintMainMessage()
        {
            int heightIdx = 1;
            GameManager.Instance.UI.PrintCenterAlignString("스파르타 마을에 오신 여러분 환영합니다.", heightIdx++, ConsoleColor.Red);
            GameManager.Instance.UI.PrintCenterAlignString("던전으로 들어가기 전 활동들을 할 수 있습니다.", heightIdx++, ConsoleColor.Yellow);
            GameManager.Instance.UI.PrintCenterAlignString("1. 상태보기", heightIdx += 4, ConsoleColor.White);
            GameManager.Instance.UI.PrintCenterAlignString("2. 인벤토리", heightIdx += 2, ConsoleColor.White);
            GameManager.Instance.UI.PrintCenterAlignString("3. 상    점", heightIdx += 2, ConsoleColor.Cyan);
            GameManager.Instance.UI.PrintCenterAlignString("4. 던    전", heightIdx += 2, ConsoleColor.Red);
            GameManager.Instance.UI.PrintCenterAlignString("5. 휴식하기", heightIdx += 2, ConsoleColor.White);
            GameManager.Instance.UI.PrintCenterAlignString("6. 타이틀로", heightIdx += 2, ConsoleColor.White);
            GameManager.Instance.UI.PrintCenterAlignString("0. 종    료 (ESC)", heightIdx += 2, ConsoleColor.Magenta);

            GameManager.Instance.UI.PrintUIBoxMessage("원하는 메뉴를 선택 해주세요. (엔터키는 누를 필요 없습니다) ");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch(keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:        // 상태 보기
                    GameManager.Instance.Player.PrintPlayerInfo();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:        // 인벤토리
                    GameManager.Instance.Player.GetInventory().PrintInventory();
                    Console.WriteLine("출력");
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
                    GameManager.Instance.Scene.LoadScene(Utilities.TITLE_SCENE_IDX);
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                case ConsoleKey.Escape:
                    Console.Clear();
                    GameManager.Instance.UI.PrintCenterAlignString("프로그램이 종료됩니다.");
                    GameManager.Instance.IsGameExit = true;
                    break;
                default:
                    break;
            }
        }

        public void CreatePlayerName()
        {
            Console.Clear();
            GameManager.Instance.UI.CreateUIMessageBox();

            int heightIdx = 1;
            GameManager.Instance.UI.PrintCenterAlignString("새로온 플레이어시군요...", heightIdx++, ConsoleColor.Red);
            GameManager.Instance.UI.PrintCenterAlignString("새로 캐릭터를 만드는 중입니다...", heightIdx++, ConsoleColor.Yellow);
            GameManager.Instance.UI.PrintCenterAlignString("타이틀로 돌아가실려면 [B] 을 입력해주세요.", heightIdx++, ConsoleColor.Cyan);
            GameManager.Instance.UI.PrintCenterAlignString("Message Box를 참고하여 주세요.", heightIdx += 3);

            while (true)
            {
                GameManager.Instance.UI.ClearUIMessageBox();
                GameManager.Instance.UI.PrintUIBoxMessage("이름을 입력해주세요 >>  ");

                Console.CursorVisible = true;

                string? playerName = Console.ReadLine();
                if(playerName == "B" || playerName == "b")
                {
                    GameManager.Instance.Scene.LoadScene(Utilities.TITLE_SCENE_IDX);
                    break;
                }

                GameManager.Instance.UI.PrintUIBoxMessage("입력하신 이름이 맞습니까? [Y/N] >>  ", 1);
                string? select = Console.ReadLine();

                if (select == "Y" || select == "y")
                {
                    GameManager.Instance.CreatePlayer(playerName);
                    break;
                }
                else if (select == "N" || select == "n")
                    GameManager.Instance.UI.ClearUIMessageBox();
                else if ((select == "B" || select == "b"))
                {
                    GameManager.Instance.Scene.LoadScene(Utilities.TITLE_SCENE_IDX);
                    break;
                }
                else
                {
                    GameManager.Instance.UI.ClearUIMessageBox();
                    GameManager.Instance.UI.PrintUIBoxMessage("올바른 값을 입력해주세요. (아무키나 누르세요)", 0);
                    Console.ReadKey(true);
                }
            }
        }
        #endregion
    }
}