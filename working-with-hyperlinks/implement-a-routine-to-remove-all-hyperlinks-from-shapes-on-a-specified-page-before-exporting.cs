using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, page index
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <pageIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pageIndexArg = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine($"Invalid page index: {pageIndexArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page index range
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specified page
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Hyperlinks collection exists and contains items
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Remove all hyperlinks from the shape
                        shape.Hyperlinks.Clear();
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Hyperlinks removed from page {pageIndex} and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}