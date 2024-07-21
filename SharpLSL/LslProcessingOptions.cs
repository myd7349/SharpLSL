using System;

using SharpLSL.Interop;

namespace SharpLSL
{
    [Flags]
    public enum LslProcessingOptions
    {
        None = lsl_processing_options_t.proc_none,
        ClockSync = lsl_processing_options_t.proc_clocksync,
        Dejitter = lsl_processing_options_t.proc_dejitter,
        Monotonize = lsl_processing_options_t.proc_monotonize,
        ThreadSafe = lsl_processing_options_t.proc_threadsafe,
        All = ClockSync | Dejitter | Monotonize | ThreadSafe,
    }
}
