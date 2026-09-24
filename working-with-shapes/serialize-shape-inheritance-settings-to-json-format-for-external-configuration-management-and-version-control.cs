using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for any Aspose.Diagram operations

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output JSON file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "inheritance.json";

        // List to hold inheritance information for all shapes
        List<ShapeInheritanceDto> inheritanceList = new();

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Capture fill inheritance values
                    string fillForegnd = shape.InheritFill?.FillForegnd?.Value ?? string.Empty;
                    string fillBkgnd = shape.InheritFill?.FillBkgnd?.Value ?? string.Empty;
                    int fillPattern = shape.InheritFill?.FillPattern?.Value ?? 0;

                    // Capture line inheritance values
                    string lineColor = shape.InheritLine?.LineColor?.Value ?? string.Empty;
                    double lineWeight = shape.InheritLine?.LineWeight?.Value ?? 0.0;
                    // Convert enum LinePatternValue to its underlying int representation
                    int linePattern = shape.InheritLine?.LinePattern?.Value != null
                        ? (int)shape.InheritLine.LinePattern.Value
                        : 0;

                    // Populate DTO with captured data
                    ShapeInheritanceDto dto = new ShapeInheritanceDto
                    {
                        ShapeId = shape.ID,
                        ShapeName = shape.NameU,
                        FillForegnd = fillForegnd,
                        FillBkgnd = fillBkgnd,
                        FillPattern = fillPattern,
                        LineColor = lineColor,
                        LineWeight = lineWeight,
                        LinePattern = linePattern
                    };

                    inheritanceList.Add(dto);
                }
            }

            // Serialize the list to JSON with indentation for readability
            string json = JsonSerializer.Serialize(inheritanceList, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Inheritance data exported to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// DTO representing the inheritance settings of a shape
class ShapeInheritanceDto
{
    public long ShapeId { get; set; }               // Unique identifier of the shape
    public string ShapeName { get; set; } = "";     // Universal name of the shape
    public string FillForegnd { get; set; } = "";   // Inherited foreground fill color (hex)
    public string FillBkgnd { get; set; } = "";     // Inherited background fill color (hex)
    public int FillPattern { get; set; }            // Inherited fill pattern index
    public string LineColor { get; set; } = "";     // Inherited line color (hex)
    public double LineWeight { get; set; }          // Inherited line weight (inches)
    public int LinePattern { get; set; }            // Inherited line pattern index
}