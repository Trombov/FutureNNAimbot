using GameOverlay.Drawing;

namespace FutureNNAimbot
{
    public class GraphicsEx : Graphics
    {
        public static Color AreaColor { get; set; } = new Color(0, 255, 0, 10); //red
        public static Color TextColor { get; set; } = new Color(120, 255, 255, 255);//turquoise
        public static Color BodyColor { get; set; } = new Color(0, 255, 0, 80);
        public static Color HeadColor { get; set; } = new Color(255, 0, 0, 80); // blueish

        public static string DefaultFontstr { get; set; } = "Arial";
        public static int DefaultFontSize { get; set; } = 10;

        public static readonly Point StartPoint = new Point(0, 0);

        Font DefaultFont;
        public SolidBrush acb; //Area Color
        public SolidBrush tfb; //Text Color
        public SolidBrush csb; //Blue brush
        public SolidBrush csfmb; //Red brush
        public SolidBrush hcb; //Head Color
        public SolidBrush bcb; //Body Color

        
        new public void  Setup()
        {
            base.Setup();

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
