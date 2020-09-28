using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace audio
{
    public class MaxPeakGetter
    {
        protected ISampleProvider Provider { get; private set; }
        protected int SamplesPerPeak { get; private set; }
        protected float[] ReadBuffer { get; private set; }

        public void Init(ISampleProvider provider, int samplesPerPeak)
        {
            Provider = provider;
            SamplesPerPeak = samplesPerPeak;
            ReadBuffer = new float[samplesPerPeak];
        }

        public PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer, 0, ReadBuffer.Length);
            if (samplesRead == 0) return null;
            var max = ReadBuffer.Take(samplesRead).Max();
            var min = ReadBuffer.Take(samplesRead).Min();
            return new PeakInfo(min, max);
        }
    }
}
