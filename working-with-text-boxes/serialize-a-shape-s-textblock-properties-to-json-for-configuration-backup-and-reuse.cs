using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // DTO representing the serializable TextBlock configuration
    public class TextBlockConfig
    {
        public string? VerticalAlign { get; set; }
        public double? LeftMargin { get; set; }
        public double? RightMargin { get; set; }
        public double? TopMargin { get; set; }
        public double? BottomMargin { get; set; }
        public string? BackgroundColor { get; set; }
        public double? BackgroundTransparency { get; set; }
        public double? DefaultTabStop { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate arguments: diagram path, shape ID, optional output JSON path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeId> [outputJsonPath]");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "textblock_config.json";

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by API)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Locate the shape with the specified ID across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    catch
                    {
                        // Ignore pages where the shape ID is not present
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                    return;
                }

                // Extract TextBlock properties
                TextBlock textBlock = targetShape.TextBlock;

                // Prepare configuration DTO
                var config = new TextBlockConfig
                {
                    // Vertical alignment (enum to string)
                    VerticalAlign = textBlock.VerticalAlign != null
                        ? textBlock.VerticalAlign.Value.ToString()
                        : null,

                    // Margin values (DoubleValue -> double)
                    LeftMargin = textBlock.LeftMargin?.Value,
                    RightMargin = textBlock.RightMargin?.Value,
                    TopMargin = textBlock.TopMargin?.Value,
                    BottomMargin = textBlock.BottomMargin?.Value,

                    // Background color (formula string, if set)
                    BackgroundColor = (textBlock.TextBkgnd != null && textBlock.TextBkgnd.Ufe != null)
                        ? textBlock.TextBkgnd.Ufe.F
                        : null,

                    // Background transparency (percentage as double)
                    BackgroundTransparency = textBlock.TextBkgndTrans?.Value,

                    // Default tab stop (in inches)
                    DefaultTabStop = textBlock.DefaultTabStop?.Value
                };

                // Serialize configuration to JSON with indentation
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"TextBlock configuration saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}