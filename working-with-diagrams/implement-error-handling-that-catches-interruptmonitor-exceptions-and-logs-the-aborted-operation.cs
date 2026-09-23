using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the Visio file to load
            string inputPath = "sample.vsdx";
            // Path to the output file (if load succeeds)
            string outputPath = "output.pdf";

            // Create an InterruptMonitor instance
            InterruptMonitor monitor = new InterruptMonitor();

            // Start a background thread that will interrupt the operation after a short delay
            Thread interrupter = new Thread(() =>
            {
                // Wait for 2 seconds before interrupting
                Thread.Sleep(2000);
                Console.WriteLine("Interrupt requested.");
                monitor.Interrupt();
            });
            interrupter.Start();

            try
            {
                // Prepare load options and assign the monitor
                LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
                loadOptions.InterruptMonitor = monitor;

                // Load the diagram (this operation can be aborted)
                Diagram diagram = new Diagram(inputPath, loadOptions);
                Console.WriteLine("Diagram loaded successfully.");

                // Example operation: save to PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Catch any exception caused by the interrupt monitor
                Console.WriteLine($"Operation aborted: {ex.Message}");
            }
            finally
            {
                // Ensure the interrupter thread has finished
                interrupter.Join();
                Console.WriteLine("Processing completed.");
            }
        }
    }