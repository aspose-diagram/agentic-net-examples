using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Retry parameters for handling file lock issues
        const int maxRetries = 3;
        const int delayMilliseconds = 1000;
        int attempt = 0;
        Diagram diagram = null;

        // Attempt to load the diagram with retry logic
        while (attempt < maxRetries)
        {
            try
            {
                // Load the diagram (no using block because Diagram does not implement IDisposable)
                diagram = new Diagram(inputPath);
                // Successful load, exit the retry loop
                break;
            }
            catch (IOException ex) when (ex.Message.Contains("being used by another process"))
            {
                attempt++;
                Console.Error.WriteLine($"Attempt {attempt} failed due to file lock. Retrying in {delayMilliseconds} ms...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }
        }

        // If diagram is still null after retries, abort
        if (diagram == null)
        {
            Console.Error.WriteLine("Unable to load the diagram after multiple attempts.");
            return;
        }

        try
        {
            // Verify that the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page
            Page firstPage = diagram.Pages[0];

            // Verify that the first page contains at least one shape
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Access the first shape on the page
            Shape shape = firstPage.Shapes[0];

            // Ensure the shape has at least one paragraph; add one if none exist
            if (shape.Paras.Count == 0)
            {
                shape.Paras.Add(new Para());
            }

            // Example: set paragraph formatting (optional)
            Para para = shape.Paras[0];
            // Set horizontal alignment to left (requires HorzAlignValue enum)
            // para.HorzAlign.Value = HorzAlignValue.LeftAlign; // Uncomment if needed and HorzAlignValue is available

            // Clear existing text runs
            shape.Text.Value.Clear();

            // Add a new text run with the desired content
            shape.Text.Value.Add(new Txt("Updated paragraph text"));

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}