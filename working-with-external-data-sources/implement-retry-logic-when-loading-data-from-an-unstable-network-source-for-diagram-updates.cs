using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

public class DiagramUpdater
{
    // Maximum number of retry attempts when refreshing data
    private const int MaxRetryAttempts = 5;
    // Initial delay between retries (in milliseconds)
    private const int InitialDelayMs = 500;

    /// <summary>
    /// Loads a Visio diagram, refreshes its data with retry logic, and saves the updated diagram.
    /// </summary>
    /// <param name="inputFilePath">Path to the source Visio file.</param>
    /// <param name="outputFilePath">Path where the updated Visio file will be saved.</param>
    public void UpdateDiagram(string inputFilePath, string outputFilePath)
    {
        // Load the diagram using the provided constructor (lifecycle rule)
        Diagram diagram = new Diagram(inputFilePath);

        int attempt = 0;
        int delay = InitialDelayMs;

        while (true)
        {
            try
            {
                // Refresh all DataRecordSets in the diagram (refreshes linked shapes)
                diagram.Refresh();

                // If refresh succeeds, exit the retry loop
                break;
            }
            catch (DiagramException ex)
            {
                attempt++;

                if (attempt >= MaxRetryAttempts)
                {
                    // Re‑throw after exceeding max attempts
                    throw new InvalidOperationException(
                        $"Failed to refresh diagram data after {MaxRetryAttempts} attempts.", ex);
                }

                // Wait before next retry (exponential back‑off)
                Thread.Sleep(delay);
                delay *= 2; // double the wait time for next attempt
            }
            catch (Exception ex)
            {
                // Non‑diagram specific errors are also retried
                attempt++;

                if (attempt >= MaxRetryAttempts)
                {
                    throw new InvalidOperationException(
                        $"Unexpected error during diagram refresh after {MaxRetryAttempts} attempts.", ex);
                }

                Thread.Sleep(delay);
                delay *= 2;
            }
        }

        // Save the updated diagram using the provided Save method (lifecycle rule)
        diagram.Save(outputFilePath, SaveFileFormat.Vsdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramUpdater();
            obj.UpdateDiagram("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
