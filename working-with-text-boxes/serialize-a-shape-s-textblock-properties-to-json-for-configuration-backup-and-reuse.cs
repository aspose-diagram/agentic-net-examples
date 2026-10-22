using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path, shape index, output JSON path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeIndex> <outputJsonPath>");
            return;
        }

        string diagramPath = args[0];
        string shapeIndexArg = args[1];
        string outputJsonPath = args[2];

        // Guard: check input diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        // Guard: parse shape index
        if (!int.TryParse(shapeIndexArg, out int shapeIndex) || shapeIndex < 0)
        {
            Console.Error.WriteLine($"Invalid shape index: {shapeIndexArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page (index 0)
                Page page = diagram.Pages[0];

                // Guard: check shape index within range
                if (shapeIndex >= page.Shapes.Count)
                {
                    Console.Error.WriteLine($"Shape index {shapeIndex} is out of range. Page contains {page.Shapes.Count} shapes.");
                    return;
                }

                // Retrieve the target shape
                Shape shape = page.Shapes[shapeIndex];

                // Extract TextBlock properties safely
                var tb = shape.TextBlock;
                var config = new TextBlockConfig
                {
                    // Margins (in inches)
                    LeftMargin = tb.LeftMargin?.Value ?? 0.0,
                    RightMargin = tb.RightMargin?.Value ?? 0.0,
                    TopMargin = tb.TopMargin?.Value ?? 0.0,
                    BottomMargin = tb.BottomMargin?.Value ?? 0.0,

                    // Background transparency (percentage)
                    TextBkgndTrans = tb.TextBkgndTrans?.Value ?? 0.0,

                    // Background color formula (e.g., "RGB(95,108,53)")
                    TextBkgnd = tb.TextBkgnd?.Ufe?.F,

                    // Default tab stop (in inches)
                    DefaultTabStop = tb.DefaultTabStop?.Value ?? 0.0,

                    // Vertical alignment enum name
                    // Fixed: VerticalAlign is an enum, not nullable; use ToString() directly
                    VerticalAlign = tb.VerticalAlign.ToString(),

                    // Text direction enum name
                    // Fixed: TextDirection is an enum, not nullable; use ToString() directly
                    TextDirection = tb.TextDirection.ToString()
                };

                // Serialize configuration to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(config, jsonOptions);

                // Ensure output directory exists
                string outputDir = Path.GetDirectoryName(outputJsonPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write JSON to file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"TextBlock configuration saved to: {outputJsonPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // DTO representing the serializable TextBlock configuration
    private class TextBlockConfig
    {
        public double LeftMargin { get; set; }
        public double RightMargin { get; set; }
        public double TopMargin { get; set; }
        public double BottomMargin { get; set; }
        public double TextBkgndTrans { get; set; }
        public string? TextBkgnd { get; set; }
        public double DefaultTabStop { get; set; }
        public string? VerticalAlign { get; set; }
        public string? TextDirection { get; set; }
    }
}
