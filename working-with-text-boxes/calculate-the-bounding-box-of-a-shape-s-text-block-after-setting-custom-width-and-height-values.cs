using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <customTextWidth> <customTextHeight> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse custom width and height for the text block.
        if (!double.TryParse(args[1], out double customWidth) || !double.TryParse(args[2], out double customHeight))
        {
            Console.Error.WriteLine("Invalid width or height values. Provide numeric values.");
            return;
        }

        // Determine output path (optional argument).
        string outputPath = args.Length >= 4 ? args[3] : "output.vsdx";

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page and one shape.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page firstPage = diagram.Pages[0];
                if (firstPage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Access the first shape on the first page.
                Shape shape = firstPage.Shapes[0];

                // Set custom width and height for the shape's text block.
                shape.TextXForm.TxtWidth.Value = customWidth;
                shape.TextXForm.TxtHeight.Value = customHeight;

                // Retrieve the resulting bounding box dimensions.
                double textBlockWidth = shape.TextXForm.TxtWidth.Value;
                double textBlockHeight = shape.TextXForm.TxtHeight.Value;

                // Output the bounding box information.
                Console.WriteLine($"Text Block Bounding Box:");
                Console.WriteLine($"  Width : {textBlockWidth}");
                Console.WriteLine($"  Height: {textBlockHeight}");

                // Optionally, save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}