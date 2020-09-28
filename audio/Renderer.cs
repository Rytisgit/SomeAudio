using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace audio
{
    public class Renderer
    {
        public Image Render(string selectedFile, Settings settings)
        {
            using (var reader = new AudioFileReader(selectedFile))
            {
                int bytesPerSample = (reader.WaveFormat.BitsPerSample / 8);
                var samples = reader.Length / (bytesPerSample);
                settings.Width = (int)samples;
                //var samplesPerPixel = settings.Width;
                //var stepSize = settings.PixelsPerPeak;
                //settings.Width = settings.PixelsPerPeak * samples;
                var peakGetter = new MaxPeakGetter();
                peakGetter.Init(reader, 1);
                return Render(peakGetter, settings);
            }
        }

        private static Image Render(MaxPeakGetter peakProvider, Settings settings)
        {

            var b = new Bitmap(settings.Width, settings.TopHeight + settings.BottomHeight);

            using (var g = Graphics.FromImage(b))
            {
                
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, b.Width, b.Height);
                var midPoint = settings.TopHeight;

                int x = 0;
                PeakInfo currentPeak;
                while ((currentPeak = peakProvider.GetNextPeak()) != null)
                {
                    for (int n = 0; n < settings.PixelsPerPeak; n++)
                    {
                        var lineHeight = settings.TopHeight * currentPeak.Max;
                        g.DrawLine(settings.TopPeakPen, x, midPoint, x, midPoint - lineHeight);
                        lineHeight = settings.BottomHeight * currentPeak.Min;
                        g.DrawLine(settings.BottomPeakPen, x, midPoint, x, midPoint - lineHeight);
                        x++;
                    }
                }
            }
            return b;
        }


    }
}

