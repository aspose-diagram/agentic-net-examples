using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file to be loaded
        string inputPath = "input.vsdx";

        // Create an InterruptMonitor to allow cancellation of the load operation
        InterruptMonitor monitor = new InterruptMonitor();

        // Configure LoadOptions with the desired format and assign the monitor
        LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
        loadOptions.InterruptMonitor = monitor;

        Diagram diagram = null;
        try
        {
            // Load the diagram using the custom LoadOptions
            diagram = new Diagram(inputPath, loadOptions);
            Console.WriteLine("Diagram loaded successfully.");
        }
        catch (Exception ex)
        {
            // Handle interruption or any other loading errors
            Console.WriteLine("Loading was interrupted or failed: " + ex.Message);
        }
        finally
        {
            // Ensure resources are released
            diagram?.Dispose();
        }
    }
}
