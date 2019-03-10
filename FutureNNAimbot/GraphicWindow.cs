using GameOverlay.Windows;

namespace FutureNNAimbot
{
    public class GraphicWindow
    {
        public OverlayWindow window;
        public GraphicsEx graphics;



        public GraphicWindow(int w, int h, Settings s)
        {
            window = new OverlayWindow(0, 0, w, h)
            {
                IsTopmost = true,
                IsVisible = true
            };
            window.CreateWindow();

            graphics = new GraphicsEx()
            {
                MeasureFPS = true,
                Height = window.Height,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = true, //testing this
                VSync = true,
                Width = window.Width,
                WindowHandle = window.Handle
            };
            graphics.Setup(s);
        }

    }
}
