using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonProject
{
    public class SceneManager
    {
        #region Member Variables
        /* Literal */
        public const int PROGRESS_SLEEP_TIME = 100;

        //private List<GameNode> _scenes = new List<GameNode>();
        private GameNode? _currentScene;
        #endregion

        #region Main Methods
        public SceneManager()
        {
            //_scenes.Add(new Scene_Title());
            //_scenes.Add(new Scene_Main());

            //_scenes[Utilities.TITLE_SCENE_IDX].IsRunning = false;
            //_scenes[Utilities.MAIN_SCENE_IDX].IsRunning = false;

            _currentScene = null;
        }

        public void Run()
        {
            while (!GameManager.Instance.IsGameExit)
            {
                if (_currentScene == null)
                    throw new NullReferenceException($"Scene Null Reference : {_currentScene}");

                _currentScene.Update();
            }
        }

        public void LoadScene(int sceneId)
        {
            ResourceLoad();

            if (_currentScene != null)
            {
                _currentScene.IsRunning = false;
                _currentScene.StopUpdate();
            }

            FakeLoading();
            switch(sceneId)
            {
                case Utilities.TITLE_SCENE_IDX:
                    _currentScene = new Scene_Title();
                    break;
                case Utilities.MAIN_SCENE_IDX:
                    _currentScene = new Scene_Main();
                    break;
                //case Utilities.CREDIT_SCENE_IDX:
                //    break;
            }
            
            _currentScene.Start();
        }

        private void ResourceLoad()
        {
            if (!GameManager.Instance.IsResourceLoad)
            {
                if (GameManager.Instance.LoadItemsData())
                {
                    Console.WriteLine("성공적으로 리소스를 로드했습니다. (아무키나 누르세요)");
                    GameManager.Instance.IsResourceLoad = true;
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("ERROR! 리소스 로드에 실패했습니다. 프로그램을 종료합니다.");
                    return;
                }
            }
        }

        private void FakeLoading()
        {
            int progressTotal = 10;
            for (int i = 0; i < progressTotal; ++i)
            {
                Console.Clear();
                ConsoleColor color;

                float progress = (float)i / progressTotal;
                int progressBarWidth = (int)(Console.WindowWidth * 0.6);
                int progressBarLength = (int)(progressBarWidth * progress);

                string progressBar = new string('■', progressBarLength) + new string('□', progressBarWidth - progressBarLength);
                string loadingText = $"[ {progressBar} ] [{i * 10}%]";

                if (i >= 0 && i < 4)
                    color = ConsoleColor.Red;
                else if (i >= 4 && i < 7)
                    color = ConsoleColor.Yellow;
                else
                    color = ConsoleColor.Green;

                GameManager.Instance.UI.PrintCenterAlignWHString(loadingText, color);

                Thread.Sleep(PROGRESS_SLEEP_TIME);
            }

            Console.Clear();
        }
        #endregion
    }
}
