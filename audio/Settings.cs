using System.Drawing;
using System.Drawing.Drawing2D;

namespace audio
{
    public class Settings
    {
        public Settings()
        {
            Width = 800;
            TopHeight = 100;
            BottomHeight = 100;
            PixelsPerPeak = 5;
            PixelsPerPeak = 1;
            TopPeakPen = Pens.Maroon;
            BottomPeakPen = Pens.Peru;
        }

        public string Name { get; set; }

        public int Width { get; set; }

        public int TopHeight { get; set; }
        public int BottomHeight { get; set; }
        public int PixelsPerPeak { get; set; }
        public Pen TopPeakPen { get; set; }
        public Pen BottomPeakPen { get; set; }

    }
}