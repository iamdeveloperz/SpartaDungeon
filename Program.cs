
using Framework;

namespace SpartaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Window Console Resize
            WindowAPI.ConsoleWindowResize();

            // Resources Load
            Manager.Instance.Resource.LoadAllResources();
            if (Manager.Instance.Resource.IsComplete)
            {
                Manager.Instance.Scene.LoadSceneIdx(Utilities.TITLE_SCENE_IDX);
                Manager.Instance.Scene.Run();
            }
        }
    }
}