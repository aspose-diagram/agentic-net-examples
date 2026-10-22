using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Assume the triangle shape is the first shape on the first page.
            // Adjust the page name or shape ID as needed for your document.
            Page page = diagram.Pages[0];
            Shape triangle = page.Shapes.GetShape(1);

            // Set line weight to 2 points (Visio stores line weight in inches).
            // 1 point = 1/72 inch, so 2 points = 2/72 inches.
            triangle.Line.LineWeight.Value = 2.0 / 72.0;

            // Set line color to navy using a hex color code.
            triangle.Line.LineColor.Value = "#000080";

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}