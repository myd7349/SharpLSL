using System;
using System.Diagnostics;

namespace SharpLSL.Examples
{
    // We create a fake device that will generate data.The inner details are not
    // so important because typically it will be up to the real data source + SDK
    // to provide a way to get data.
    internal class FakeDevice
    {
        public FakeDevice(int channelCount, double srate)
        {
            _channelCount = channelCount;
            _srate = srate;
            _patternSamples = (int)(srate - 0.5) + 1; // Truncate OK
            _head = 0;

            // Pre-allocate entire test pattern. The data could be generated on the fly
            // for a much smaller memory hit, but we also use this example application
            // to test LSL Outlet performance so we want to reduce out-of-LSL CPU
            // utilization.
            var magnitude = short.MaxValue;
            var offset0 = magnitude / 2;
            var offsetStep = magnitude / _channelCount;

            _pattern = new short[_patternSamples * _channelCount];

            for (int s = 0; s < _patternSamples; ++s)
            {
                for (int c = 0; c < _channelCount; ++c)
                {
                    // sin(2*pi*f*t), where f cycles from 1 Hz to Nyquist: srate / 2
                    double f = (c + 1) % (int)(srate / 2);
                    var sample = (short)(
                        offset0 + c * offsetStep +
                        magnitude * Math.Sin(2 * Math.PI * f * s / srate));
                    _pattern[s * _channelCount + c] = sample;
                }
            }

            _stopwatch = Stopwatch.StartNew();
            _lastTimePoint = 0.0;
        }

        public short[] GetData()
        {
            var currentTimePoint = _stopwatch.Elapsed.TotalMilliseconds;
            var elapsedMilliseconds = currentTimePoint - _lastTimePoint;
            var elapsedSamples = (long)(elapsedMilliseconds * _srate * 1e-3);

            var result = new short[elapsedSamples * _channelCount];
            var samplesRead = GetData(result);

            if (samplesRead == elapsedSamples)
            {
                return result;
            }
            else
            {
                var output = new short[samplesRead * _channelCount];
                Array.Copy(result, output, output.Length);
                return output;
            }
        }

        public int GetData(short[] buffer, bool noData = false)
        {
            ArgumentNullException.ThrowIfNull(buffer);

            if (buffer.Length % _channelCount != 0)
                throw new ArgumentException(nameof(buffer));

            var currentTimePoint = _stopwatch.Elapsed.TotalMilliseconds;
            var elapsedMilliseconds = currentTimePoint - _lastTimePoint;
            var elapsedSamples = Math.Min(
                (int)(elapsedMilliseconds * _srate * 1e-3),
                buffer.Length / _channelCount);

            if (noData)
            {
                // The fastest but no patterns.
                // Array.Fill(buffer, 23);
            }
            else
            {
                var endSample = _head + elapsedSamples;
                var noWrapSamples = Math.Min(_patternSamples - _head, elapsedSamples);

                Array.Copy(_pattern, _head, buffer, 0, noWrapSamples);
                if (endSample > _patternSamples)
                    Array.Copy(_pattern, 0, buffer, noWrapSamples, elapsedSamples - noWrapSamples);
            }

            _head = (_head + elapsedSamples) % _patternSamples;
            _lastTimePoint = currentTimePoint;

            return elapsedSamples;
        }

        private readonly double _srate;
        private readonly int _channelCount;
        private readonly int _patternSamples;
        private readonly short[] _pattern;
        private readonly Stopwatch _stopwatch;

        private double _lastTimePoint;
        private int _head;
    }
}
