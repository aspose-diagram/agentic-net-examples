using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Verify the diagram has at least three pages (page index is zero‑based)
            if (diagram.Pages.Count < 3)
            {
                Console.Error.WriteLine("The diagram does not contain a third page to delete.");
                return;
            }

            // Retrieve the third page (index 2)
            Page pageThree = diagram.Pages[2];

            // Check whether the page contains any shapes
            if (pageThree.Shapes.Count > 0)
            {
                Console.WriteLine("Page three contains shapes and will not be deleted to preserve data integrity.");
                return;
            }

            // Page is empty – proceed to delete it
            // The PageCollection does not support RemoveAt, but it does support Remove(Page)
            diagram.Pages.Remove(pageThree);
            Console.WriteLine("Empty page three has been removed.");

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}