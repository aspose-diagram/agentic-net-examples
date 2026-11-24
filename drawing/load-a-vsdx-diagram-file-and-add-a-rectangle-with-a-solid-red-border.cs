using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVsdxPath> [outputVsdxPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the VSDX diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Draw a rectangle on the page (pinX, pinY, width, height)
                page.DrawRectangle(1, 2, 2, 4);

                // Retrieve the shape that was just added (last shape in the collection)
                // Shapes collection is 1‑based; using Count as the index gets the last shape
                Shape rectangleShape = page.Shapes[page.Shapes.Count];

                // Set a solid red border for the rectangle
                rectangleShape.Line.LineColor.Value = "#FF0000";

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}