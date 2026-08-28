using System;
using System.Threading;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the source diagram file
            const string inputPath = "input.vsdx";
            // Path for the output diagram file
            const string outputPath = "output.vsdx";

            // Create an InterruptMonitor to allow interruption of long-running operations
            InterruptMonitor monitor = new InterruptMonitor();

            // Configure load options to use the interrupt monitor
            LoadOptions loadOptions = new LoadOptions();
            loadOptions.InterruptMonitor = monitor;

            Diagram diagram = null;
            Thread interrupter = null;

            try
            {
                // Load the diagram with interrupt monitoring enabled
                diagram = new Diagram(inputPath, loadOptions);

                // Start a background thread that will trigger an interrupt after a short delay
                interrupter = new Thread(() =>
                {
                    // Wait briefly before interrupting
                    Thread.Sleep(100);
                    monitor.Interrupt();
                });
                interrupter.Start();

                // Perform a save operation that can be interrupted
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Log the aborted operation
                Console.WriteLine($"Operation aborted: {ex.Message}");
            }
            finally
            {
                // Ensure the interrupter thread has finished
                interrupter?.Join();

                // Clean up the diagram object
                diagram?.Dispose();
            }
        }
    }