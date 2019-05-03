using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureNNAimbot
{
    public class DrawHelper
    {
        private Settings s;
        GraphicWindow mainWnd;
        GraphicWindow textWnd;

        public DrawHelper()
        {
            s = MainApp.settings;
            mainWnd = new GraphicWindow(s.SizeX, s.SizeY);
            textWnd = new GraphicWindow(s.SizeX, s.SizeY);
            textWnd.window.X = 0;
            textWnd.window.Y = 0;
        }



        public void DrawPlaying(Settings settings, IEnumerable<Alturos.Yolo.Model.YoloItem> items, bool firemode)
        {
            if (s.CursorToCenter)
            {
                var curMousPos = System.Windows.Forms.Cursor.Position;
                mainWnd.window.X = (int)curMousPos.X - s.SizeX / 2;
                mainWnd.window.Y = (int)curMousPos.Y - s.SizeY / 2;
            }
            else
            {
                mainWnd.window.X = MainApp.gc.screen_x;
                mainWnd.window.Y = MainApp.gc.screen_y;
            }

            mainWnd.graphics.BeginScene();
            mainWnd.graphics.ClearScene();

            textWnd.graphics.BeginScene();
            textWnd.graphics.ClearScene();

            if (s.DrawAreaRectangle)
                mainWnd.graphics.DrawRectangle(mainWnd.graphics.csb, 0, 0, s.SizeX, s.SizeY, 2);

            mainWnd.graphics.FillRectangle(firemode ? mainWnd.graphics.csfmb : mainWnd.graphics.csb,
                Rectangle.Create(s.SizeX / 2, s.SizeY / 2, 4, 4));

            //draw main text
            if (s.DrawText)
            {
                textWnd.graphics.WriteText($"SmoothAim {Math.Round(settings.SmoothAim, 2)}; Head {settings.Head}; SimpleRCS {settings.SimpleRCS}");
            }


            foreach (var item in items)
            {
                DrawItem(item);
            }

            mainWnd.graphics.EndScene();
            textWnd.graphics.EndScene();
        }

        private void DrawItem(Alturos.Yolo.Model.YoloItem item)
        {
            var shooting = 0;

            Rectangle head = Rectangle.Create(item.X + Convert.ToInt32(item.Width / 2.9), item.Y, Convert.ToInt32(item.Width / 3), item.Height / 7);
            Rectangle body = Rectangle.Create(item.X + Convert.ToInt32(item.Width / 6), item.Y + item.Height / 6, Convert.ToInt32(item.Width / 1.5f), item.Height / 3);


            mainWnd.graphics.DrawRectangle(mainWnd.graphics.hcb, Rectangle.Create(item.X, item.Y, item.Width, item.Height), 2);

            if (s.Head)
            {

                mainWnd.graphics.DrawRectangle(mainWnd.graphics.hcb, head, 2);
                mainWnd.graphics.DrawCrosshair(mainWnd.graphics.hcb, head.Left + head.Width / 2, head.Top + head.Height / 2 + Convert.ToInt32(1 * shooting), 2, 2, CrosshairStyle.Cross);
                mainWnd.graphics.DrawLine(mainWnd.graphics.hcb, s.SizeX / 2, s.SizeY / 2, head.Left + head.Width / 2, head.Top + head.Height / 2 + Convert.ToInt32(1 * shooting), 2);

            }
            else
            {
                mainWnd.graphics.DrawRectangle(mainWnd.graphics.bcb, body, 2);
                mainWnd.graphics.DrawCrosshair(mainWnd.graphics.bcb, body.Left + body.Width / 2, body.Top + body.Height / 2 + Convert.ToInt32(1 * shooting), 2, 2, CrosshairStyle.Cross);
                mainWnd.graphics.DrawLine(mainWnd.graphics.bcb, s.SizeX / 2, s.SizeY / 2, body.Left + body.Width / 2, body.Top + body.Height / 2 + Convert.ToInt32(1 * shooting), 2);

            }
        }

        public void DrawDisabled()
        {
            mainWnd.window.X = 0;
            mainWnd.window.Y = 0;
            mainWnd.graphics.BeginScene();
            mainWnd.graphics.ClearScene();
            mainWnd.graphics.EndScene();

            textWnd.graphics.BeginScene();
            textWnd.graphics.ClearScene();
            textWnd.graphics.WriteText($"Aimbot Disabled!");
            textWnd.graphics.EndScene();
        }

        public void DrawTraining(System.Drawing.Rectangle trainBox, Settings settings, string selectedObject, bool screenshotMode)
        {
            mainWnd.graphics.BeginScene();
            mainWnd.graphics.ClearScene();

            if (s.DrawAreaRectangle)
                mainWnd.graphics.DrawRectangle(mainWnd.graphics.csb, 0, 0, s.SizeX, s.SizeY, 2);

            mainWnd.graphics.WriteText("Training mode. Object: " + selectedObject + Environment.NewLine
                + "ScreenshotMode: " + (screenshotMode == true ? "following" : "centered") + Environment.NewLine + $"CursorToCenter: {settings.CursorToCenter}");
            mainWnd.graphics.DrawRectangle(mainWnd.graphics.csb, Rectangle.Create(trainBox.X, trainBox.Y, trainBox.Width, trainBox.Height), 1);
            mainWnd.graphics.DrawRectangle(mainWnd.graphics.csb, Rectangle.Create(trainBox.X + Convert.ToInt32(trainBox.Width / 2.9), trainBox.Y, Convert.ToInt32(trainBox.Width / 3), trainBox.Height / 7), 2);

            mainWnd.graphics.EndScene();
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
