using GameOverlay.Drawing;

namespace FutureNNAimbot
{
    public class GraphicsEx : Graphics
    {
        public static string DefaultFontstr { get; set; } = "Arial";
        public static int DefaultFontSize { get; set; } = 12;

        public static readonly Point StartPoint = new Point(0, 0);

        Font DefaultFont;
        public SolidBrush acb; //Area Color
        public SolidBrush tfb; //Text Color
        public SolidBrush csb; //Blue brush
        public SolidBrush csfmb; //Red brush
        public SolidBrush hcb; //Head Color
        public SolidBrush bcb; //Body Color


        public void Setup(Settings s)
        {
            base.Setup();

            GameOverlay.Drawing.Color AreaColor = new GameOverlay.Drawing.Color(0, 255, 0, s.Transparency);
            GameOverlay.Drawing.Color TextColor = new GameOverlay.Drawing.Color(120, 255, 255, s.Transparency);
            GameOverlay.Drawing.Color BodyColor = new GameOverlay.Drawing.Color(0, 255, 0, s.Transparency);
            GameOverlay.Drawing.Color HeadColor = new GameOverlay.Drawing.Color(255, 0, 0, s.Transparency);
            
            DefaultFont = CreateFont(DefaultFontstr, DefaultFontSize);
            acb = AreaColor.GetSolidBrush(this);
            tfb = TextColor.GetSolidBrush(this);
            csb = CreateSolidBrush(Color.Blue);
            csfmb = CreateSolidBrush(Color.Red);
            hcb = HeadColor.GetSolidBrush(this);
            bcb = BodyColor.GetSolidBrush(this);
        }

        public void WriteText(string text, Font f = null)
        {

            f = f ?? DefaultFont;
            DrawText(DefaultFont, tfb, StartPoint, text);

        }

    }

    public static class ColorHelper
    {
        public static SolidBrush GetSolidBrush(this Color c, Graphics graphics)
        {
            return graphics.CreateSolidBrush(c);
        }

    }

}
