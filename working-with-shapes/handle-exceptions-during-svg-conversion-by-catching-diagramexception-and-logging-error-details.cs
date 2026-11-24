using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputSvgPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Use BOOL.False to check the deletion flag
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No visible shape found on the first page.");
                    return;
                }

                // Prepare SVG save options (default options are sufficient for basic export)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the selected shape to SVG
                targetShape.ToSvg(outputSvgPath, svgOptions);

                Console.WriteLine($"Shape successfully exported to SVG: {outputSvgPath}");
            }
        }
        catch (DiagramException dex)
        {
            // Handle Aspose.Diagram specific exceptions
            Console.Error.WriteLine($"DiagramException occurred: {dex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}