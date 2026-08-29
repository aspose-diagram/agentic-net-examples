using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional output path (second argument) or default to "output.vsdx"
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Apply a custom fill color (red) to the shape
                    shape.Fill.FillForegnd.Value = "#FF0000";

                    // Apply a custom line color (green) to the shape
                    shape.Line.LineColor.Value = "#00FF00";

                    // Validate that the fill foreground color (used as a theme indicator) is not null or empty
                    if (string.IsNullOrEmpty(shape.Fill.FillForegnd.Value))
                    {
                        // Report validation failure with shape identification details
                        Console.Error.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has a null or empty FillForegnd value.");
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}