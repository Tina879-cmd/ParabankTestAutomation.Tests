public enum LogLevel
{
    Trace, // Log everything, take screenshots at every step, enable Playwright tracing
    Info,  // Log important info, take screenshots only on failure
    Error  // Log only errors, take screenshots only on failure
}