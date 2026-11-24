using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, page index (0‑based), output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <pageIndex> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string outputPath = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine($"Invalid page index: {pageIndexArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API guidelines
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Guard: ensure the requested page index is within the diagram's page collection
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target page
                Page targetPage = diagram.Pages[pageIndex];

                // Iterate over all shapes on the target page
                foreach (Shape shape in targetPage.Shapes)
                {
                    // Ensure the shape is not logically deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // If the shape has a Hyperlinks collection, clear it
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        shape.Hyperlinks.Clear();
                    }
                }

                // Export the modified diagram to the specified output path (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Hyperlinks removed from page {pageIndex}. Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}