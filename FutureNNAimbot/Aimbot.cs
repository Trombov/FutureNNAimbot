using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    internal class Aimbot
    {
        private readonly GameProcess gp;
        private NeuralNet nn;
        private Settings s;
        private DrawHelper dh;
        private GController gc;

        bool Enabled = true;
        bool isRunning = false;

        public Aimbot()
        {
            gp = MainApp.gameProcess;
            nn = MainApp.nNet;
            s = MainApp.settings;
            dh = MainApp.drawHelper;
            gc = MainApp.gameController;
        }

        public void Start()
        {
            Console.WriteLine(dh.ObjToString(s, System.Environment.NewLine, "*"));
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("running Aimbot :)");
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    ReadKeys();

                }
            }).Start();

            while (true)
            {

                if (Enabled && isRunning)
                {
                    var bitmap = gc.ScreenCapture();
                    var items = nn.GetItems(bitmap);
                    RenderItems(items);

                    dh.DrawPlaying(s, items, FireMode, PauseMode);

                }
                else
                {
                    dh.DrawDisabled();
                    Thread.Sleep(500);
                }
            }
        }


        static bool lastMDwnState = false;
        static bool FireMode = false;
        static bool PauseMode = false;
        static long lastTick = DateTime.Now.Ticks;
        static int numClicks = 0;

        public void RenderItems(IEnumerable<Alturos.Yolo.Model.YoloItem> items)
        {
            var isMdwn = s.ShootKeys.Any(x => User32.GetAsyncKeyState(x) != 0);
            var timeout = lastTick + (s.AutoAimDelayMs * 10000);// 10,000 ticks = 1 ms
            if (isMdwn || DateTime.Now.Ticks > timeout)
            {
                FireMode = isMdwn || lastMDwnState;
                lastMDwnState = isMdwn;
                lastTick = DateTime.Now.Ticks;
            }

            if (PauseMode)
            {
                if (s.ShootKeys.Any(x => User32.GetAsyncKeyState(x) != 0))
                {
                    while (s.ShootKeys.Any(x => User32.GetAsyncKeyState(x) != 0))
                        Thread.Sleep(100);
                    numClicks++;
                }
                if (numClicks > 1)
                    PauseMode = false;
                else
                    FireMode = false;
            }

            if (items.Count() > 0 && FireMode)
            {
                Shooting(ref items);
            }
        }

        void Shooting(ref IEnumerable<Alturos.Yolo.Model.YoloItem> items)
        {
            if (s.Head)
            {
                Alturos.Yolo.Model.YoloItem nearestEnemy = items.OrderBy(x => DistanceBetweenCross(x.X + Convert.ToInt32(x.Width / 2.9) + (x.Width / 3) / 2, x.Y + (x.Height / 7) / 2)).FirstOrDefault();

                Rectangle nearestEnemyHead = Rectangle.Create(nearestEnemy.X + Convert.ToInt32(nearestEnemy.Width / 2.9), nearestEnemy.Y, Convert.ToInt32(nearestEnemy.Width / 3), nearestEnemy.Height / 7);

                if (s.SmoothAim <= 0)
                {
                    VirtualMouse.Move(Convert.ToInt32(((nearestEnemyHead.Left - s.SizeX / 2) + (nearestEnemyHead.Width / 2))),
                        Convert.ToInt32((nearestEnemyHead.Top - s.SizeY / 2 + nearestEnemyHead.Height / 3)));
                }
                else//not smoothAim
                {

                    if (s.SizeX / 2 < nearestEnemyHead.Left | s.SizeX / 2 > nearestEnemyHead.Right
                        | s.SizeY / 2 < nearestEnemyHead.Top | s.SizeY / 2 > nearestEnemyHead.Bottom)
                    {
                        VirtualMouse.Move(Convert.ToInt32(((nearestEnemyHead.Left - s.SizeX / 2) + (nearestEnemyHead.Width / 2)) * s.SmoothAim),
                            Convert.ToInt32((nearestEnemyHead.Top - s.SizeY / 2 + nearestEnemyHead.Height / 7) * s.SmoothAim));
                    }
                }
            }
            else // aim 2 body
            {

                Alturos.Yolo.Model.YoloItem nearestEnemy = items.OrderBy(x => DistanceBetweenCross(x.X + Convert.ToInt32(x.Width / 6) + (x.Width / 1.5f) / 2, x.Y + x.Height / 6 + (x.Height / 3) / 2)).First();

                Rectangle nearestEnemyBody = Rectangle.Create(nearestEnemy.X + Convert.ToInt32(nearestEnemy.Width / 6), nearestEnemy.Y + nearestEnemy.Height / 6, Convert.ToInt32(nearestEnemy.Width / 1.5f), nearestEnemy.Height / 3);
                if (s.SmoothAim <= 0)
                {
                    VirtualMouse.Move(Convert.ToInt32(((nearestEnemyBody.Left - s.SizeX / 2) + (nearestEnemyBody.Width / 2))), Convert.ToInt32((nearestEnemyBody.Top - s.SizeY / 2 + nearestEnemyBody.Height / 7)));
                }
                else
                {

                    if (s.SizeX / 2 < nearestEnemyBody.Left | s.SizeX / 2 > nearestEnemyBody.Right
                        | s.SizeY / 2 < nearestEnemyBody.Top | s.SizeY / 2 > nearestEnemyBody.Bottom)
                    {
                        VirtualMouse.Move(Convert.ToInt32(((nearestEnemyBody.Left - s.SizeX / 2) + (nearestEnemyBody.Width / 2)) * s.SmoothAim), Convert.ToInt32((nearestEnemyBody.Top - s.SizeY / 2 + nearestEnemyBody.Height / 7) * s.SmoothAim));
                    }
                }
            }

            if (s.SimpleRCS)
                VirtualMouse.Move(0, s.SimpleRCSvalue);

        }

        void ReadKeys()
        {
            if (User32.GetAsyncKeyState(s.DisableKey) != 0)
            {
                Enabled = !Enabled;
                if (Enabled)
                {
                    gc.SetHandle();
                    Console.Beep(750, 125);
                    Console.Beep(1700, 200);
                }
                else
                {
                    gc.Dispose();
                    Console.Beep(1700, 125);
                    Console.Beep(750, 200);
                }
            }

            if (User32.GetAsyncKeyState(s.PauseKey) != 0)
            {
                PauseMode = true;
                numClicks = 0;
            }

            if (User32.GetAsyncKeyState(Keys.Up) == -32767)
            {
                s.SmoothAim = s.SmoothAim >= 1 ? s.SmoothAim : s.SmoothAim + 0.05f;
            }

            if (User32.GetAsyncKeyState(Keys.Down) == -32767)
            {
                s.SmoothAim = s.SmoothAim <= 0 ? s.SmoothAim : s.SmoothAim - 0.05f;
            }

            if (User32.GetAsyncKeyState(Keys.Delete) == -32767)
            {
                s.Head = s.Head == true ? false : true;
            }

            if (User32.GetAsyncKeyState(Keys.Home) == -32767)
            {
                s.SimpleRCS = s.SimpleRCS == true ? false : true;
            }

            isRunning = MainApp.gameProcess.IsForeground();

            Thread.Sleep(100);
        }

        public float DistanceBetweenCross(float X, float Y)
        {
            float ydist = (Y - s.SizeY / 2);
            float xdist = (X - s.SizeX / 2);
            float Hypotenuse = (float)Math.Sqrt(Math.Pow(ydist, 2) + Math.Pow(xdist, 2));
            return Hypotenuse;
        }





    }
}
