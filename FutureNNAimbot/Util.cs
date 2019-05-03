using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace FutureNNAimbot
{
    internal static class Util
    {
        public static bool IsKeyPressed(Keys key)
        {
            return User32.GetAsyncKeyState(key) != 0;
        }

        public static bool IsKeyToggled(Keys key)
        {
            return User32.GetAsyncKeyState(key) == -32767;
        }
    }

    internal static class VirtualMouse
    {
        [DllImport("user32.dll")]
        static extern void Mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void Move(int xDelta, int yDelta)
        {
            Mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }
        public static void MoveTo(int x, int y)
        {
            Mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
        }
        public static void LeftClick()
        {
            
            Mouse_event(MOUSEEVENTF_LEFTDOWN, MousePosition.X, MousePosition.Y, 0, 0);
            Mouse_event(MOUSEEVENTF_LEFTUP, MousePosition.X, MousePosition.Y, 0, 0);
        }

        public static void LeftDown()
        {
            Mouse_event(MOUSEEVENTF_LEFTDOWN, MousePosition.X, MousePosition.Y, 0, 0);
        }

        public static void LeftUp()
        {
            Mouse_event(MOUSEEVENTF_LEFTUP, MousePosition.X, MousePosition.Y, 0, 0);
        }

        public static void RightClick()
        {
            Mouse_event(MOUSEEVENTF_RIGHTDOWN, MousePosition.X, MousePosition.Y, 0, 0);
            Mouse_event(MOUSEEVENTF_RIGHTUP, MousePosition.X, MousePosition.Y, 0, 0);
        }

        public static void RightDown()
        {
            Mouse_event(MOUSEEVENTF_RIGHTDOWN, MousePosition.X, MousePosition.Y, 0, 0);
        }

        public static void RightUp()
        {
            Mouse_event(MOUSEEVENTF_RIGHTUP, MousePosition.X, MousePosition.Y, 0, 0);
        }
    }

}
