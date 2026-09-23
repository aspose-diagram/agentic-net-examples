using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram (lifecycle rule)
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 1.0; // horizontal gap in inches
            options.DistanceInVertical = 1.0;   // vertical gap in inches

            // Set up cancellation support
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            // Listen for user input to cancel the operation
            Task.Run(() =>
            {
                Console.WriteLine("Press 'c' then Enter to cancel auto‑spacing...");
                while (true)
                {
                    string? line = Console.ReadLine();
                    if (line != null && line.Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        cts.Cancel();
                        break;
                    }
                }
            });

            try
            {
                // Check for cancellation before starting the auto‑spacing
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Auto‑spacing was cancelled before it started.");
                }
                else
                {
                    // Perform auto‑spacing; this call is atomic, so we only check before it runs
                    page.AutoSpaceShapes(page.Shapes, options);
                    Console.WriteLine("Auto‑spacing completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during auto‑spacing: {ex.Message}");
            }

            // Save the modified diagram (lifecycle rule)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
