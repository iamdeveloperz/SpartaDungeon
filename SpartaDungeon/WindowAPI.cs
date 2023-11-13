
using System.Runtime.InteropServices;

namespace Framework
{
    public class WindowAPI
    {
        #region WINDOWS API BASE
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        /*  Literals  */
        private const string KernelDll = "kernel32.dll";
        private const string UserDll = "user32.dll";

        public const int STD_INTPUT_HANDLE = -10;
        public const int STD_OUTPUT_HANDLE = -11;

        public const int SWP_HIDEWINDOW = 0x128;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_DRAWFRAME = 0x0020;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_NOREDRAW = 0x0008;

        public const int SW_NORMAL = 1;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOW = 5;
        public const int SW_RESTORE = 9;

        public const int INIT_WINDOW_TIME = 2000;


        /*  Variables  */
        public static IntPtr handlerWindow = GetForegroundWindow();
        public static IntPtr handlerConsole = GetConsoleWindow();
        public static IntPtr handlerStdOutput = GetStdHandle(STD_OUTPUT_HANDLE);


        /*  Extern Function. 
         *  여기서 잠깐! extern은 외부 파일을 불러올 때 쓰는 키워드이다.  */

        // Handlers
        [DllImport(UserDll)] public static extern IntPtr GetForegroundWindow();
        [DllImport(KernelDll)] public static extern IntPtr GetConsoleWindow();
        [DllImport(KernelDll)] public static extern IntPtr GetStdHandle(int nStdHandle);


        // Functions
        [DllImport(UserDll)] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport(UserDll)] public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport(UserDll)] public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport(UserDll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int posX, int posY, int width, int height, uint uFlags);

        #region UserMethods By WinAPI
        public static void ConsoleWindowResize()
        {
            RECT wRect;
            ShowWindow(handlerWindow, SW_MAXIMIZE);
            GetWindowRect(handlerWindow, out wRect);

            //Console.WriteLine($"LEFT(pos X) : {wRect.Left}, TOP(pos Y) : {wRect.Top} " +
            //    $"RIGHT(width) : {wRect.Right}, BOTTOM(height) : {wRect.Bottom}");

            int originWidth = (wRect.Right - wRect.Left);
            int originHeight = (wRect.Bottom - wRect.Top);

            int posX = originWidth / 8;
            int posY = originHeight / 8;
            int nWidth = originWidth - (posX * 2);
            int nHeight = originHeight - (posY * 2);

            SetWindowPos(handlerWindow, IntPtr.Zero, posX, posY, nWidth, nHeight, SWP_SHOWWINDOW | SWP_NOREDRAW);
            ShowWindow(handlerWindow, SW_SHOW);

            Console.WriteLine("화면을 재구성 중입니다. 조금만 기다려주세요.");
            Console.CursorVisible = false;
            Thread.Sleep(INIT_WINDOW_TIME);
            Console.Clear();
        }
        #endregion

        #endregion
    }
}
