
using System;
using System.Runtime.InteropServices;
using Framework;

namespace SpartaDungeon
{
    internal class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        static void Main(string[] args)
        {
            MessageBox(IntPtr.Zero, "Command-line message box", "Attention!", 0);
            // Window Console Resize
            //WindowAPI.ConsoleWindowResize();

            //// Resources Load
            //Manager.Instance.Resource.LoadAllResources();

            //Player player = new Player();
            //player.InitPlayerData();

            //player.PlayerData.Name = "hh23i";

            //Manager.Instance.Data.SavePlayer(player, ResourceKeys.Player);
            //if (Manager.Instance.Resource.IsComplete)
            //{
            //    Manager.Instance.Scene.LoadSceneIdx(Utilities.MAIN_SCENE_IDX);
            //    Manager.Instance.Scene.Run();
            //}
        }
    }
}