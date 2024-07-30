using System;

using SharpLSL.Interop;

namespace SharpLSL
{
    /// <summary>
    /// Post-processing options for stream inlets.
    /// </summary>
    /// <seealso cref="StreamInlet.SetPostProcessingOptions(PostProcessingOptions)"/>
    [Flags]
    public enum PostProcessingOptions
    {
        /// <summary>
        /// No automatic post-processing is applied.
        /// </summary>
        /// <remarks>
        /// This option returns the ground-truth timestamps as they are, allowing
        /// for manual post-processing. It is the default behavior of the inlet.
        /// </remarks>
        None = lsl_processing_options_t.proc_none,

        /// <summary>
        /// Performs clock synchronization automatically.
        /// </summary>
        /// <remarks>
        /// This option indicates a time correction to the timestamps will be performed
        /// automatically. It is equivalent to manually adding the <seealso cref="StreamInlet.SetSmoothingHalfTime(float)"/>
        /// value to the received timestamps.
        /// </remarks>
        /// <seealso cref="StreamInlet.TimeCorrection(double)"/>
        /// <seealso cref="StreamInlet.TimeCorrection(ref double, ref double, double)"/>
        ClockSync = lsl_processing_options_t.proc_clocksync,

        /// <summary>
        /// Removes jitter from timestamps.
        /// </summary>
        /// <remarks>
        /// This will apply a smoothing algorithm to the received timestamps; the
        /// smoothing needs to see a minimum number of samples (30-120 seconds
        /// worst-case) until the remaining jitter is consistently below 1ms.
        /// </remarks>
        Dejitter = lsl_processing_options_t.proc_dejitter,

        /// <summary>
        /// Forces the timestamps to be monotonically ascending.
        /// </summary>
        /// <remarks>
        /// This option only makes sense if timestamps are dejittered.
        /// </remarks>
        Monotonize = lsl_processing_options_t.proc_monotonize,

        /// <summary>
        /// Indicates post-processing is thread-safe.
        /// </summary>
        /// <remarks>
        /// This option ensures that post-processing is thread-safe, allowing multiple
        /// threads to read from the same inlet concurrently. Note that this flag may
        /// increase CPU usage.
        /// </remarks>
        ThreadSafe = lsl_processing_options_t.proc_threadsafe,

        /// <summary>
        /// The combination of all possible post-processing options.
        /// </summary>
        All = ClockSync | Dejitter | Monotonize | ThreadSafe,
    }
}
