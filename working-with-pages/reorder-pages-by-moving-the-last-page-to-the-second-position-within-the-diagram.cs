using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> [outputFilePath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or auto‑generated).
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_reordered.vsdx");

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;

                // If there are fewer than two pages, no reordering is needed.
                if (pageCount < 2)
                {
                    Console.WriteLine("Diagram contains fewer than two pages; no reordering performed.");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to: {outputPath}");
                    return;
                }

                // Retrieve the last page in the collection.
                Page lastPage = diagram.Pages[pageCount - 1];

                // Move the last page to the second position (index 1).
                // The MoveTo method repositions the page within the diagram.
                lastPage.MoveTo(1);

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Pages reordered successfully. Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}