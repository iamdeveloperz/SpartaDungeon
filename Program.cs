
using Framework;
using SpartaDungeon.Managers;

namespace SpartaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Window Console Resize
            WindowAPI.ConsoleWindowResize();

            // 의존성 주입 형태로 할려다가 일단 다음에... 지금은 싱글톤으로 관리
            Player player = new Player();
            Manager.Instance.PM.SetPlayer(player);

            // Resources Load
            Manager.Instance.Resource.LoadAllResources();
            Manager.Instance.Data.LoadItemData();

            if (Manager.Instance.Resource.IsComplete)
            {
                Manager.Instance.Scene.LoadSceneIdx(Utilities.MAIN_SCENE_IDX);
                Manager.Instance.Scene.Run();
            }
        }
    }
}