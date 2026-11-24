using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

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

        // Retry parameters for handling file lock scenarios
        const int maxAttempts = 3;
        const int delayMilliseconds = 1000;
        Diagram diagram = null;

        // Attempt to load the diagram with retry logic
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                // Load the diagram inside a using block as required
                diagram = new Diagram(inputPath);
                break; // Success, exit the retry loop
            }
            catch (IOException ex)
            {
                // IOException may indicate the file is locked
                Console.Error.WriteLine($"Attempt {attempt} failed to load diagram: {ex.Message}");
                if (attempt == maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Exiting.");
                    return;
                }
                // Wait before the next attempt
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Any other exception should abort the operation
                Console.Error.WriteLine($"Unexpected error while loading diagram: {ex.Message}");
                return;
            }
        }

        // Ensure the diagram was loaded
        if (diagram == null)
        {
            Console.Error.WriteLine("Failed to load diagram.");
            return;
        }

        try
        {
            // Access the first page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Access the first shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("First page contains no shapes.");
                return;
            }
            Shape shape = page.Shapes[0];

            // Clear existing text runs
            shape.Text.Value.Clear();

            // Add new paragraph text
            shape.Text.Value.Add(new Txt("Hello, Aspose.Diagram!"));

            // Optionally, modify paragraph formatting (e.g., line spacing)
            // Ensure there is at least one paragraph to modify
            if (shape.Paras.Count > 0)
            {
                // Set line spacing to 1.5 lines
                shape.Paras[0].SpLine.Value = 1.5;
                // Set horizontal alignment to left
                shape.Paras[0].HorzAlign.Value = HorzAlignValue.LeftAlign;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}