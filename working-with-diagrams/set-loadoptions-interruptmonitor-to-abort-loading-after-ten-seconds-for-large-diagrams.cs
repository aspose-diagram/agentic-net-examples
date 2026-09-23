using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create an interrupt monitor to allow aborting the load operation
        InterruptMonitor monitor = new InterruptMonitor();

        // Schedule the interrupt after ten seconds
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            monitor.Interrupt();
        });

        // Assign the monitor to LoadOptions
        LoadOptions loadOptions = new LoadOptions();
        loadOptions.InterruptMonitor = monitor;

        try
        {
            // Load the diagram with the configured options
            Diagram diagram = new Diagram("largeDiagram.vsdx", loadOptions);
            Console.WriteLine("Diagram loaded successfully.");
            // Additional processing can be done here
        }
        catch (Exception ex)
        {
            // Loading was aborted or failed
            Console.WriteLine($"Diagram loading aborted: {ex.Message}");
        }
    }
}
