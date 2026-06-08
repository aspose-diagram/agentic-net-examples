using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

public class DiagramUpdater
{
    /// <summary>
    /// Loads a Visio diagram, refreshes its data recordsets with retry logic,
    /// and returns the updated Diagram instance.
    /// </summary>
    /// <param name="filePath">Path to the Visio file to load.</param>
    /// <param name="maxRetries">Maximum number of retry attempts.</param>
    /// <param name="initialDelay">Initial delay before the first retry. Subsequent retries use exponential back‑off.</param>
    /// <returns>The refreshed Diagram object.</returns>
    public Diagram LoadAndRefreshWithRetry(string filePath, int maxRetries = 3, TimeSpan? initialDelay = null)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path must be provided.", nameof(filePath));

        // Load the diagram using the constructor that accepts a file name.
        Diagram diagram = new Diagram(filePath);

        // Determine the base delay.
        TimeSpan delay = initialDelay ?? TimeSpan.FromSeconds(2);
        int attempt = 0;

        while (true)
        {
            try
            {
                // Refresh all DataRecordSets in the diagram.
                diagram.Refresh();
                // If refresh succeeds, exit the loop.
                break;
            }
            catch (DiagramException dex)
            {
                attempt++;

                if (attempt > maxRetries)
                {
                    // All retries exhausted – rethrow the exception.
                    throw new InvalidOperationException(
                        $"Failed to refresh diagram after {maxRetries} attempts.", dex);
                }

                // Optionally log the exception (placeholder for real logging).
                Console.WriteLine($"Refresh attempt {attempt} failed: {dex.Message}");
                Console.WriteLine($"Waiting {delay.TotalSeconds} seconds before retry...");

                // Wait before the next retry.
                Thread.Sleep(delay);

                // Exponential back‑off for subsequent attempts.
                delay = TimeSpan.FromSeconds(delay.TotalSeconds * 2);
            }
            catch (Exception ex)
            {
                // Non‑DiagramException errors are considered fatal.
                throw new InvalidOperationException("Unexpected error during diagram refresh.", ex);
            }
        }

        return diagram;
    }

    /// <summary>
    /// Example usage: loads a diagram, refreshes it with retries, and saves the result.
    /// </summary>
    public void Example()
    {
        string sourcePath = @"C:\Diagrams\MyDiagram.vsdx";
        string destinationPath = @"C:\Diagrams\MyDiagram_Updated.vsdx";

        // Load and refresh with retry logic.
        Diagram updatedDiagram = LoadAndRefreshWithRetry(sourcePath, maxRetries: 5, initialDelay: TimeSpan.FromSeconds(1));

        // Save the updated diagram using the Save method that accepts a file name.
        updatedDiagram.Save(destinationPath, SaveFileFormat.Vsdx);

        // Dispose when done.
        updatedDiagram.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramUpdater();
            obj.Example();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
