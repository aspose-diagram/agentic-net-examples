using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class DiagramUpdater
{
    // Updates a Visio diagram by refreshing its data sources with retry logic.
    // inputPath  - path to the source diagram file.
    // outputPath - path where the updated diagram will be saved.
    // maxRetries - maximum number of refresh attempts.
    // initialDelay - initial wait time before retrying; defaults to 2 seconds.
    public static void UpdateDiagramWithRetry(string inputPath, string outputPath, int maxRetries = 3, TimeSpan? initialDelay = null)
    {
        // Load the diagram using the provided constructor (lifecycle rule).
        Diagram diagram = new Diagram(inputPath);

        int attempt = 0;
        TimeSpan delay = initialDelay ?? TimeSpan.FromSeconds(2);

        while (true)
        {
            try
            {
                // Refresh all DataRecordSets in the diagram (feature rule).
                diagram.Refresh();

                // Refresh succeeded; exit the retry loop.
                break;
            }
            catch (DiagramException ex)
            {
                attempt++;

                if (attempt > maxRetries)
                {
                    // Exceeded allowed retries – rethrow as a more descriptive exception.
                    throw new InvalidOperationException(
                        $"Failed to refresh diagram after {maxRetries} attempts.", ex);
                }

                // Wait before the next retry.
                Thread.Sleep(delay);

                // Exponential back‑off for subsequent attempts.
                delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * 2);
            }
        }

        // Save the updated diagram using the appropriate Save method (lifecycle rule).
        // Adjust the format as needed (e.g., VDX, VSDX, etc.).
        diagram.Save(outputPath, SaveFileFormat.Vdx);
    }

    // Example entry point.
    static void Main()
    {
        string sourceFile = @"C:\Diagrams\SourceDiagram.vsdx";
        string targetFile = @"C:\Diagrams\UpdatedDiagram.vsdx";

        try
        {
            UpdateDiagramWithRetry(sourceFile, targetFile, maxRetries: 5);
            Console.WriteLine("Diagram updated and saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating diagram: {ex.Message}");
        }
    }
}
