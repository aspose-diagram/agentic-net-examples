using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        // Prepare a cancellation token source for user‑initiated abort.
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        // Start a background task that waits for the user to type 'c' to cancel.
        Task.Run(() =>
        {
            Console.WriteLine("Press 'c' then Enter to cancel the auto‑spacing operation...");
            string line = Console.ReadLine();
            if (!string.IsNullOrEmpty(line) && line.Trim().Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                cts.Cancel();
                Console.WriteLine("Cancellation requested.");
            }
        });

        try
        {
            // Load the Visio diagram from the input file.
            Diagram diagram;
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                diagram = new Diagram(stream);
            }

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options.
            AutoSpaceOptions options = new AutoSpaceOptions();
            // Set horizontal and vertical distances (in inches).
            options.DistanceInHorizontal = 0.5;
            options.DistanceInVertical = 0.5;

            // Run the auto‑spacing operation in a cancellable task.
            Task autoSpaceTask = Task.Run(() =>
            {
                // Throw if cancellation was requested before starting.
                token.ThrowIfCancellationRequested();

                // Perform auto‑spacing on all shapes of the page.
                page.AutoSpaceShapes(page.Shapes, options);
            }, token);

            // Wait for the task to complete or be cancelled.
            try
            {
                autoSpaceTask.Wait(token);
                Console.WriteLine("Auto‑spacing completed successfully.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Auto‑spacing was cancelled by the user.");
                // Skip saving the diagram when cancelled.
                return;
            }

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}