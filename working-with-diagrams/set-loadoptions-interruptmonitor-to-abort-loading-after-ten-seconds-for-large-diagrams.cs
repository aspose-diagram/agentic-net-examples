using System;
using Aspose.Diagram;

// Custom interrupt monitor that aborts after a specified time interval
class TimeBasedInterruptMonitor : AbstractInterruptMonitor
{
    private readonly DateTime _startTime;
    private readonly TimeSpan _maxDuration;

    public TimeBasedInterruptMonitor(TimeSpan maxDuration)
    {
        _maxDuration = maxDuration;
        _startTime = DateTime.UtcNow;
    }

    // Returns true when the elapsed time exceeds the allowed duration
    public override bool IsInterruptionRequested
    {
        get { return (DateTime.UtcNow - _startTime) > _maxDuration; }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Create LoadOptions and assign the custom interrupt monitor (10 seconds)
            LoadOptions loadOptions = new LoadOptions();
            loadOptions.InterruptMonitor = new TimeBasedInterruptMonitor(TimeSpan.FromSeconds(10));

            // Load the diagram using the LoadOptions with the interrupt monitor
            Diagram diagram = new Diagram("input.vsd", loadOptions);

            // Example: save the diagram after successful load (optional)
            diagram.Save("output.vsd", SaveFileFormat.Vsd);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}