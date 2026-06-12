using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <shapeId> [output.vsdx]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        string outputPath = args.Length >= 3 ? args[2] : "output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0]; // assuming shape is on the first page
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Enable dynamic gluing for the shape
            shape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with gluing enabled on shape {shapeId} to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}