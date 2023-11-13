
using Framework;

namespace SpartaDungeon
{
    public class Manager_Scene
    {
        #region Member Variables

        public const int PROGRESS_SLEEP_TIME = 200;

        private List<GameNode>? _scenes = null;
        private GameNode? _currentScene = null;

        #endregion

        #region Constructor & Initalize
        public Manager_Scene()
        {
            this.Initialize();
        }
        private void Initialize()
        {
            _scenes = new List<GameNode>
            {
                new Scene_Title(),
                new Scene_Main()
            };

            _scenes[Utilities.TITLE_SCENE_IDX].Reset();
            _scenes[Utilities.MAIN_SCENE_IDX].Reset();
        }
        #endregion

        #region Main Methods
        public void Run()
        {
            while (!Manager.Instance.IsGameEnd && _currentScene != null)
                _currentScene.Update();
        }

        public void LoadSceneIdx(int sceneIdx)
        {
            if (_currentScene != null)
                _currentScene.Reset();

            this.FakeLoading();

            switch(sceneIdx)
            {
                case Utilities.TITLE_SCENE_IDX:
                    _currentScene = _scenes?[sceneIdx];
                    break;
                case Utilities.MAIN_SCENE_IDX:
                    _currentScene = _scenes?[sceneIdx];
                    break;
            }

            _currentScene?.Start();
        }
        #endregion

        #region Helper Methods
        // 씬 전환할 때 쓰이는 페이크 로딩 메소드
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

                Manager.Instance.UI.PrintTextAlignCenterToCenter(loadingText, color);
                Thread.Sleep(PROGRESS_SLEEP_TIME);
            }

            Console.Clear();
        }
        #endregion
    }
}
