using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: input file must exist.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: output directory must exist (create if missing).
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Construct the output file name for the current page.
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.eps");

                    // TODO: Aspose.Diagram does not provide direct EPS export.
                    // Implement EPS export here if/when the API adds support.
                    // For now, this placeholder indicates where EPS saving logic would go.
                    Console.WriteLine($"[TODO] Export page {i + 1} to EPS: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}