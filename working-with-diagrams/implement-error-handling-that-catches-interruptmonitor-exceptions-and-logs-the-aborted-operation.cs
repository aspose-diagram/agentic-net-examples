using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the input Visio file
            string inputPath = "input.vsdx";
            // Path to the output PDF file
            string outputPath = "output.pdf";

            // Create an InterruptMonitor to allow operation interruption
            InterruptMonitor interruptMonitor = new InterruptMonitor();

            try
            {
                // Configure load options with the interrupt monitor
                LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsdx);
                loadOptions.InterruptMonitor = interruptMonitor;

                // Load the diagram using the configured options
                Diagram diagram = new Diagram(inputPath, loadOptions);
                // Assign the same monitor to the diagram for post‑load operations
                diagram.InterruptMonitor = interruptMonitor;

                // Perform the save operation (PDF export in this example)
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                Console.WriteLine("Diagram processed and saved successfully.");
            }
            catch (Exception ex)
            {
                // Log the aborted operation caused by an interrupt or any other error
                Console.WriteLine($"Operation aborted: {ex.Message}");
            }
        }
    }