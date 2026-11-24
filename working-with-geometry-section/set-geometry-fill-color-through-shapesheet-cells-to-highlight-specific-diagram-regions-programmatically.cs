using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Aspose.Diagram.Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Example condition: highlight only shapes with a specific name prefix (optional)
                        // If no specific condition is needed, this block will apply to all shapes.
                        // Uncomment and adjust the condition as required.
                        // if (shape.NameU != null && shape.NameU.StartsWith("Highlight"))
                        // {
                        //     // Apply highlight color
                        // }

                        // Set fill pattern to solid (value 1)
                        shape.Fill.FillPattern.Value = 1;

                        // Set foreground fill color to a bright yellow (hex format)
                        shape.Fill.FillForegnd.Value = "#FFFF00";

                        // Optional: ensure background color is transparent (no background)
                        shape.Fill.FillBkgnd.Value = "#FFFFFF";
                    }
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}