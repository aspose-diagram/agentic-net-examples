using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <outputJsonPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string outputJsonPath = args[1];
        string shapeIdArg = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse shape ID.
        if (!int.TryParse(shapeIdArg, out int shapeId) || shapeId <= 0)
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Use the first page (index 0). Adjust as needed for other pages.
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);

            // Verify the shape is not logically deleted.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Extract line properties.
            var lineInfo = new
            {
                Pattern = (int)shape.Line.LinePattern.Value,          // enum to int
                Weight = shape.Line.LineWeight.Value,                // double (in inches)
                Color = shape.Line.LineColor.Value,                  // string (hex or color name)
                BeginArrow = (int)shape.Line.BeginArrow.Value,       // enum to int
                EndArrow = (int)shape.Line.EndArrow.Value            // enum to int
            };

            // Extract fill properties.
            var fillInfo = new
            {
                Pattern = (int)shape.Fill.FillPattern.Value,         // enum to int
                ForegroundColor = shape.Fill.FillForegnd.Value,      // string
                BackgroundColor = shape.Fill.FillBkgnd.Value,        // string
                ForegroundTransparency = shape.Fill.FillForegndTrans.Value, // double (0‑1)
                BackgroundTransparency = shape.Fill.FillBkgndTrans.Value   // double (0‑1)
            };

            // Combine into a single export object.
            var exportObject = new
            {
                ShapeId = shapeId,
                Line = lineInfo,
                Fill = fillInfo
            };

            // Serialize to JSON with indentation for readability.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(exportObject, jsonOptions);

            // Write JSON to the specified output file.
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Shape line and fill definitions exported to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}