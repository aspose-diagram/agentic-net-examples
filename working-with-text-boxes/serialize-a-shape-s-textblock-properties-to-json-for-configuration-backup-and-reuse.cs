using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for shape-related operations

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape ID, output JSON path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string shapeIdArg = args[1];
        // Guard: parse shape ID to long
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        string outputPath = args[2];
        // Guard: ensure the directory for output exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns a Shape instance for the given ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                return;
            }

            // Extract TextBlock properties from the shape
            var textBlockInfo = new TextBlockDto
            {
                // Margins (points are converted to double values)
                LeftMargin = targetShape.TextBlock.LeftMargin.Value,
                RightMargin = targetShape.TextBlock.RightMargin.Value,
                TopMargin = targetShape.TextBlock.TopMargin.Value,
                BottomMargin = targetShape.TextBlock.BottomMargin.Value,

                // Text direction (enum to string)
                TextDirection = targetShape.TextBlock.TextDirection.Value.ToString(),

                // Vertical alignment (enum to string)
                VerticalAlign = targetShape.TextBlock.VerticalAlign.Value.ToString(),

                // Background color formula (e.g., "RGB(95,108,53)")
                TextBackground = targetShape.TextBlock.TextBkgnd.Ufe.F,

                // Background transparency (percentage)
                TextBackgroundTransparency = targetShape.TextBlock.TextBkgndTrans.Value,

                // Default tab stop (in inches)
                DefaultTabStop = targetShape.TextBlock.DefaultTabStop.Value
            };

            // Serialize the DTO to JSON with indentation
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(textBlockInfo, jsonOptions);

            // Write JSON to the specified output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"TextBlock properties saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// DTO representing the serializable TextBlock properties
class TextBlockDto
{
    public double LeftMargin { get; set; }
    public double RightMargin { get; set; }
    public double TopMargin { get; set; }
    public double BottomMargin { get; set; }
    public string TextDirection { get; set; } = string.Empty;
    public string VerticalAlign { get; set; } = string.Empty;
    public string TextBackground { get; set; } = string.Empty;
    public double TextBackgroundTransparency { get; set; }
    public double DefaultTabStop { get; set; }
}