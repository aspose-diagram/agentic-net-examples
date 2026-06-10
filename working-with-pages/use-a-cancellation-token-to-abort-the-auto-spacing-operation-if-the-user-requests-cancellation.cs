using System.IO;
using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Configure auto-space options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 2,
                DistanceInVertical = 2
            };

            // Create a cancellation token source
            using CancellationTokenSource cts = new CancellationTokenSource();

            // Example: cancel after 1 second (replace with real user input handling)
            Timer timer = new Timer(_ => cts.Cancel(), null, 1000, Timeout.Infinite);

            try
            {
                // Check for cancellation before starting the operation
                if (cts.Token.IsCancellationRequested)
                    throw new OperationCanceledException();

                // Perform auto-spacing
                page.AutoSpaceShapes(page.Shapes, options);
                Console.WriteLine("Auto-spacing completed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Auto-spacing was cancelled by the user.");
            }
            finally
            {
                timer.Dispose();
            }

            // Save the diagram only if the operation was not cancelled
            if (!cts.Token.IsCancellationRequested)
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
