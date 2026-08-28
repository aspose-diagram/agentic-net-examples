using System;
using System.Diagnostics;
using Aspose.Diagram;

// Custom interrupt monitor that requests interruption after 10 seconds
class MyInterruptMonitor : AbstractInterruptMonitor
{
    private readonly Stopwatch _watch;

    public MyInterruptMonitor()
    {
        _watch = Stopwatch.StartNew();
    }

    // Returns true when more than 10 seconds have elapsed
    public override bool IsInterruptionRequested
    {
        get { return _watch.Elapsed.TotalSeconds > 10; }
    }
}

class Program
{
    static void Main()
    {
        // Create load options and assign the custom interrupt monitor
        LoadOptions loadOptions = new LoadOptions();
        loadOptions.InterruptMonitor = new MyInterruptMonitor();

        try
        {
            // Load the diagram using the options; loading will abort after 10 seconds
            Diagram diagram = new Diagram("largeDiagram.vsd", loadOptions);
            // Diagram is now loaded (if it completed within the time limit)
        }
        catch (Exception ex)
        {
            // Handle the interruption or any other loading errors
            Console.WriteLine($"Loading aborted: {ex.Message}");
        }
    }
}