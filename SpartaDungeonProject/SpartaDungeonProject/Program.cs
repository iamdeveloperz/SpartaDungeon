using System.Diagnostics;
using System.Drawing;

namespace SpartaDungeonProject
{
    internal class Program
    {
        private static void Main()
        {
            // 윈도우 화면 크기 사이즈 조절
            FrameworkWinAPI.ConsoleWindowResize();

            GameManager.Instance.Scene.LoadScene(Utilities.TITLE_SCENE_IDX);
            // Scene Manager를 호출하여 업데이트
            GameManager.Instance.Scene.Run();
        }
    }
}