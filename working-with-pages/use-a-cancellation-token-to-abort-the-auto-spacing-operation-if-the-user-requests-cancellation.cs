using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a cancellation token source that can be triggered by the user.
        CancellationTokenSource cts = new CancellationTokenSource();

        // Start a background task that waits for the user to type 'c' to cancel.
        Task.Run(() =>
        {
            Console.WriteLine("Press 'c' then Enter to cancel the auto‑spacing operation...");
            string line = Console.ReadLine();
            if (!string.IsNullOrEmpty(line) && line.Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                cts.Cancel();
            }
        });

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (adjust as needed).
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options.
            AutoSpaceOptions options = new AutoSpaceOptions();
            // Set horizontal and vertical distances (example values).
            options.DistanceInHorizontal = 0.5;
            options.DistanceInVertical = 0.5;

            // Check for cancellation before starting the operation.
            if (cts.Token.IsCancellationRequested)
            {
                Console.WriteLine("Operation cancelled before auto‑spacing started.");
                return;
            }

            // Perform auto‑spacing on all shapes of the page.
            // Aspose.Diagram does not support mid‑operation cancellation,
            // so we only honor cancellation before the call.
            page.AutoSpaceShapes(page.Shapes, options);

            // Check for cancellation before saving.
            if (cts.Token.IsCancellationRequested)
            {
                Console.WriteLine("Operation cancelled before saving the diagram.");
                return;
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Auto‑spacing completed. Diagram saved to: {outputPath}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Auto‑spacing operation was cancelled by the user.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}