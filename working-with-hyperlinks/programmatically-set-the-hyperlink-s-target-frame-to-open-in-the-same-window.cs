using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile> [shapeNameU]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetShapeName = args.Length >= 3 ? args[2] : null;

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // If a specific shape name was provided, filter by NameU.
                    if (targetShapeName != null && shape.NameU != targetShapeName)
                        continue;

                    // Ensure the shape has a Hyperlinks collection.
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate over each hyperlink attached to the shape.
                    for (int linkIndex = 0; linkIndex < shape.Hyperlinks.Count; linkIndex++)
                    {
                        Hyperlink link = shape.Hyperlinks[linkIndex];

                        // Set the hyperlink to open in the same window (NewWindow = false).
                        link.NewWindow.Value = BOOL.False;
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}