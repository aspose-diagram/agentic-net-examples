using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // required for Aspose.Diagram types

namespace ShapeStyleExport
{
    // DTO for line properties
    public class LineDto
    {
        // Hex color string (e.g., "#FF0000")
        public string? Color { get; set; }

        // Line thickness in inches
        public double? Weight { get; set; }

        // Pattern name (enum converted to string)
        public string? Pattern { get; set; }

        // Arrow style indices
        public int? BeginArrow { get; set; }
        public int? EndArrow { get; set; }
    }

    // DTO for fill properties
    public class FillDto
    {
        // Foreground (fill) color hex
        public string? Foreground { get; set; }

        // Background (fill) color hex
        public string? Background { get; set; }

        // Fill pattern index
        public int? Pattern { get; set; }

        // Gradient enabled flag
        public bool? GradientEnabled { get; set; }

        // Gradient direction index
        public int? GradientDirection { get; set; }
    }

    // Root DTO containing both line and fill sections
    public class ShapeStyleDto
    {
        public LineDto Line { get; set; } = new();
        public FillDto Fill { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Validate argument count
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Usage: ShapeStyleExport <inputVisioPath> <shapeId> <outputJsonPath>");
                return;
            }

            // Input Visio file path
            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Shape identifier (numeric ID)
            if (!long.TryParse(args[1], out long shapeId))
            {
                Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
                return;
            }

            // Output JSON file path
            string outputPath = args[2];
            // Ensure the directory for the output exists
            string? outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the first page (index 0) – adjust if needed
                Page page = diagram.Pages[0];

                // Get the shape by its ID; cast to int because GetShape expects int
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Prepare DTO for serialization
                ShapeStyleDto styleDto = new ShapeStyleDto();

                // ----- Extract line properties -----
                // Color hex string
                styleDto.Line.Color = shape.Line.LineColor.Value;
                // Thickness in inches
                styleDto.Line.Weight = shape.Line.LineWeight.Value;
                // Convert enum to its name for readability
                styleDto.Line.Pattern = shape.Line.LinePattern.Value.ToString();
                // Arrow style indices (stored as IntValue)
                styleDto.Line.BeginArrow = shape.Line.BeginArrow.Value;
                styleDto.Line.EndArrow = shape.Line.EndArrow.Value;

                // ----- Extract fill properties -----
                // Foreground (fill) color
                styleDto.Fill.Foreground = shape.Fill.FillForegnd.Value;
                // Background (fill) color
                styleDto.Fill.Background = shape.Fill.FillBkgnd.Value;
                // Fill pattern index
                styleDto.Fill.Pattern = shape.Fill.FillPattern.Value;
                // Gradient enabled flag (BOOL enum)
                styleDto.Fill.GradientEnabled = shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True;
                // Gradient direction index
                styleDto.Fill.GradientDirection = shape.Fill.GradientFill.GradientDir.Value;

                // Serialize DTO to JSON with indentation
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                string json = JsonSerializer.Serialize(styleDto, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape style exported successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}