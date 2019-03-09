using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public class Aimbot
    {
        private GameProcess gp;
        private NeuralNet nn;
        private Settings s;
        private DrawHelper dh;
        int selectedObject = 0;
        private int shooting = 0;
        private System.Drawing.Point coordinates;
        private bool Enabled = true;

        public Aimbot(Settings settings, GameProcess gameProcess, NeuralNet neuralNet)
        {
            gp = gameProcess;
            nn = neuralNet;
            s = settings;
            dh = new DrawHelper(settings);
        }

        public void Start()
        {
            Console.WriteLine("running Aimbot :)");
            var gc = new gController(s);
            bool Running = true;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    ReadKeys();

                }
            }).Start();

            while (Running)
            {

                if (Enabled)
                {
                    coordinates = Cursor.Position;
                    var bitmap = gc.ScreenCapture(true, coordinates);
                    var items = nn.GetItems(bitmap);
                    RenderItems(items);

                    dh.DrawPlaying(coordinates, nn.TrainingNames?[selectedObject], s, items,Firemode);

                }
                else
                {
                    dh.DrawDisabled();
                }
            }
        }


        static bool lastMDwnState = false;
        static bool Firemode = false;
        static long lastTick = DateTime.Now.Ticks;

        public void RenderItems(IEnumerable<Alturos.Yolo.Model.YoloItem> items)
        {
            shooting = 0;

            var isMdwn = User32.GetAsyncKeyState(Keys.RButton) == -32767 || User32.GetAsyncKeyState(Keys.LButton) == -32767;
            if (isMdwn || DateTime.Now.Ticks > lastTick + 20000000)
            {               
                Firemode = isMdwn  || lastMDwnState;
                lastMDwnState = isMdwn;
                lastTick = DateTime.Now.Ticks;
            }

            if (items.Count() > 0 && Firemode)
            {
                Shooting(ref items);
            }
        }

        void Shooting(ref IEnumerable<Alturos.Yolo.Model.YoloItem> items)
        {
            if (s.Head)
            {
                Alturos.Yolo.Model.YoloItem nearestEnemy = items.Where(x => x.Type == nn.TrainingNames[selectedObject])
                    .OrderBy(x => DistanceBetweenCross(x.X + Convert.ToInt32(x.Width / 2.9) + (x.Width / 3) / 2, x.Y + (x.Height / 7) / 2))
                    .FirstOrDefault();

                Rectangle nearestEnemyHead = Rectangle.Create(nearestEnemy.X + Convert.ToInt32(nearestEnemy.Width / 2.9), nearestEnemy.Y, Convert.ToInt32(nearestEnemy.Width / 3), nearestEnemy.Height / 7 + (float)2 * shooting);

                if (s.SmoothAim <= 0)
                {
                    VirtualMouse.Move(Convert.ToInt32(((nearestEnemyHead.Left - s.SizeX / 2) + (nearestEnemyHead.Width / 2))),
                        Convert.ToInt32((nearestEnemyHead.Top - s.SizeY / 2 + nearestEnemyHead.Height / 3 + 1 * shooting)));

                    //VirtualMouse.Move(Convert.ToInt32(((nearestEnemyHead.Left - s.SizeX / 2) + (nearestEnemyHead.Width / 2))),
                    //    Convert.ToInt32((nearestEnemyHead.Top - s.SizeY / 2 + nearestEnemyHead.Height / 7 + 1 * shooting)));

                    if (s.SimpleRCS) shooting += 2;
                }
                else//not smoothAim
                {

                    if (s.SizeX / 2 < nearestEnemyHead.Left | s.SizeX / 2 > nearestEnemyHead.Right
                        | s.SizeY / 2 < nearestEnemyHead.Top | s.SizeY / 2 > nearestEnemyHead.Bottom)
                    {
                        VirtualMouse.Move(Convert.ToInt32(((nearestEnemyHead.Left - s.SizeX / 2) + (nearestEnemyHead.Width / 2)) * s.SmoothAim),
                            Convert.ToInt32((nearestEnemyHead.Top - s.SizeY / 2 + nearestEnemyHead.Height / 7 + 1 * shooting) * s.SmoothAim));
                    }
                    else
                    {
                        if (s.SimpleRCS) shooting += 2;
                    }
                }
            }
            else // aim 2 body
            {

                var nearestEnemy = items.Where(x => x.Type == nn.TrainingNames[selectedObject])
                    .OrderBy(x => DistanceBetweenCross(x.X + Convert.ToInt32(x.Width / 6) + (x.Width / 1.5f / 2), x.Y + (x.Height / 6) + (x.Height / 3) / 2))
                    .First();

                Rectangle nearestEnemyBody = Rectangle.Create(nearestEnemy.X + Convert.ToInt32(nearestEnemy.Width / 6), nearestEnemy.Y + nearestEnemy.Height / 6 + (float)2 * shooting, Convert.ToInt32(nearestEnemy.Width / 1.5f), nearestEnemy.Height / 3 + (float)2 * shooting);
                if (s.SmoothAim <= 0)
                {
                    VirtualMouse.Move(Convert.ToInt32(((nearestEnemyBody.Left - s.SizeX / 2) + (nearestEnemyBody.Width / 2))), Convert.ToInt32((nearestEnemyBody.Top - s.SizeY / 2 + nearestEnemyBody.Height / 7 + 1 * shooting)));

                    if (s.SimpleRCS) shooting += 2;
                }
                else
                {

                    if (s.SizeX / 2 < nearestEnemyBody.Left | s.SizeX / 2 > nearestEnemyBody.Right
                        | s.SizeY / 2 < nearestEnemyBody.Top | s.SizeY / 2 > nearestEnemyBody.Bottom)
                    {
                        VirtualMouse.Move(Convert.ToInt32(((nearestEnemyBody.Left - s.SizeX / 2) + (nearestEnemyBody.Width / 2)) * s.SmoothAim), Convert.ToInt32((nearestEnemyBody.Top - s.SizeY / 2 + nearestEnemyBody.Height / 7 + 1 * shooting) * s.SmoothAim));
                    }
                    else
                    {
                        if (s.SimpleRCS) shooting += 2;
                    }
                }
            }

            if (s.SimpleRCS)
                VirtualMouse.Move(0, shooting);

        }

        void ReadKeys()
        {
            if (isKeyToggled(Keys.PageUp))
            {
                selectedObject = (selectedObject + 1) % nn.TrainingNames.Count();
            }

            if (isKeyToggled(Keys.Up))
            {
                s.SmoothAim = Math.Min(s.SmoothAim + 0.05f, 1);
            }

            if (isKeyToggled(Keys.Down))
            {
                s.SmoothAim = Math.Max(s.SmoothAim - 0.05f, 0);
            }

            if (isKeyToggled(Keys.Delete))
            {
                s.Head = !s.Head;
            }

            if (isKeyToggled(Keys.Home))
            {
                shooting = 0;
                s.SimpleRCS = !s.SimpleRCS;
            }

            //if (isKeyToggled(Keys.End))
            //{
            //    s.AutoShoot = !s.AutoShoot;
            //}

            if (isKeyToggled(Keys.PageDown))
            {
                selectedObject = (selectedObject - 1 + nn.TrainingNames.Count()) % nn.TrainingNames.Count();
            }

        }

        public float DistanceBetweenCross(float X, float Y)
        {
            float ydist = (Y - s.SizeY / 2);
            float xdist = (X - s.SizeX / 2);
            float Hypotenuse = (float)Math.Sqrt(Math.Pow(ydist, 2) + Math.Pow(xdist, 2));
            return Hypotenuse;
        }

        static bool isKeyPressed(Keys key)
        {
            return User32.GetAsyncKeyState(key) != 0;
        }

        static bool isKeyToggled(Keys key)
        {
            return User32.GetAsyncKeyState(key) == -32767;
        }



    }
}
