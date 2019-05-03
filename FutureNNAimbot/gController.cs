using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public class GController : IDisposable
    {
        private IntPtr phnd;
        private IntPtr hdcSrc;
        private User32.RECT windowRect;
        private IntPtr hdcDest;
        private IntPtr hBitmap;
        private IntPtr hOld;

        private readonly int width;
        private readonly int height;

        private int screen_width;
        private int screen_height;

        public int screen_x;
        public int screen_y;
        readonly string game;

        public GController()
        {
            width = MainApp.settings.SizeX;
            height = MainApp.settings.SizeY;
            game = MainApp.settings.Game;
            SetHandle();
        }

        public void SetHandle()
        {
            var p = Process.GetProcessesByName(game).FirstOrDefault();
            phnd = p?.MainWindowHandle ?? IntPtr.Zero;

            while (p == null)
            {
                Console.WriteLine("waiting for game");
                Console.ReadLine();
                p = Process.GetProcessesByName(game).FirstOrDefault();
                if (p == null) continue;
                phnd = Process.GetProcessesByName(game).FirstOrDefault().MainWindowHandle;
            }

            ////set screencapture handles
            hdcSrc = User32.GetWindowDC(phnd);
            windowRect = new User32.RECT();
            User32.GetWindowRect(phnd, ref windowRect);
            screen_width = windowRect.right - windowRect.left;
            screen_height = windowRect.bottom - windowRect.top;
            screen_x = screen_width / 2 - width / 2;
            screen_y = screen_height / 2 - height / 2;
            hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            hOld = GDI32.SelectObject(hdcDest, hBitmap);
        }

        //added fix by caching capture object references
        public System.Drawing.Image ScreenCapture()
        {
            var size = new System.Drawing.Point(width, height);


            if (MainApp.settings.FollowMouse)
            {
                System.Drawing.Point coordinates = Cursor.Position;
                GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, coordinates.X - width / 2, coordinates.Y - height / 2, GDI32.SRCCOPY);
            }
            else
                GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, screen_x, screen_y, GDI32.SRCCOPY);

            try
            {
                return System.Drawing.Image.FromHbitmap(hBitmap);
            }
            catch (Exception)
            {
                SetHandle();
                return System.Drawing.Image.FromHbitmap(hBitmap);
            }
        }

        public System.Drawing.Image CaptureWindow(string name, bool followMouse, System.Drawing.Point coordinates)
        {
            var size = new System.Drawing.Point(width, height);

            if (Process.GetProcessesByName(name).Count() == 0)
            {
                MessageBox.Show($"Looks like you closed {name}...");
                Process.GetCurrentProcess().Kill();
            }
            IntPtr handle = Process.GetProcessesByName(name)[0].MainWindowHandle;
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            screen_width = windowRect.right - windowRect.left;
            screen_height = windowRect.bottom - windowRect.top;
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, size.X, size.Y);
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            if (followMouse)
            {
                GDI32.BitBlt(hdcDest, 0, 0, size.X, size.Y, hdcSrc, coordinates.X - size.X / 2, coordinates.Y - size.Y / 2, GDI32.SRCCOPY);
            }
            else GDI32.BitBlt(hdcDest, 0, 0, size.X, size.Y, hdcSrc, screen_width / 2 - size.X / 2, screen_height / 2 - size.Y / 2, GDI32.SRCCOPY);
            GDI32.SelectObject(hdcDest, hOld);
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            System.Drawing.Image img = System.Drawing.Image.FromHbitmap(hBitmap);
            GDI32.DeleteObject(hBitmap);
            return img;
        }

        public void SaveCapture(string path)
        {
            ScreenCapture().Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void Dispose()
        {
            GDI32.SelectObject(hdcDest, hOld);
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(phnd, hdcSrc);
            GDI32.DeleteObject(hBitmap);
        }
    }
}
