using System;
using System.Diagnostics;
using Aspose.Diagram;

// Custom interrupt monitor that signals interruption after a specified time span
class TimedInterruptMonitor : AbstractInterruptMonitor
{
    private readonly Stopwatch _stopwatch;
    private readonly TimeSpan _maxDuration;

    public TimedInterruptMonitor(TimeSpan maxDuration)
    {
        _maxDuration = maxDuration;
        _stopwatch = Stopwatch.StartNew(); // Start timing immediately
    }

    // Aspose.Diagram checks this property during loading
    public override bool IsInterruptionRequested
    {
        get
        {
            // Request interruption if elapsed time exceeds the allowed duration
            return _stopwatch.Elapsed > _maxDuration;
        }
    }
}

class Program
{
    static void Main()
    {
        // Initialize load options
        var loadOptions = new LoadOptions();

        // Assign the interrupt monitor to abort loading after 10 seconds
        loadOptions.InterruptMonitor = new TimedInterruptMonitor(TimeSpan.FromSeconds(10));

        try
        {
            // Load the diagram using the configured load options
            var diagram = new Diagram("largeDiagram.vsd", loadOptions);

            // Diagram loaded successfully; proceed with further processing if needed
        }
        catch (Exception ex)
        {
            // Loading was interrupted (or another error occurred)
            Console.WriteLine("Diagram loading was interrupted or failed: " + ex.Message);
        }
    }
}