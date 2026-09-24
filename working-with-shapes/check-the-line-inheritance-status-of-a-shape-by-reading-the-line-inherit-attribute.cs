using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for shape operations as per guidelines

class Program
{
    static void Main(string[] args)
    {
        // Input diagram file path
        string diagramPath = "input.vsdx";
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Input shape ID to inspect (replace with actual ID as needed)
        long shapeId = 1;
        // Guard: shape ID must be positive
        if (shapeId <= 0)
        {
            Console.Error.WriteLine("Invalid shape ID specified.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (assuming the shape is on page 0)
            Page page = diagram.Pages[0];

            // Attempt to get the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Compare a line property (e.g., LineColor) with its inherited counterpart
            // Matching values indicate the line formatting is inherited
            bool isLineColorInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
            bool isLineWeightInherited = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;
            bool isLinePatternInherited = shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;

            // Determine overall inheritance status (all three must match)
            bool isLineInherited = isLineColorInherited && isLineWeightInherited && isLinePatternInherited;

            // Output the inheritance status
            Console.WriteLine($"Shape ID: {shapeId}");
            Console.WriteLine($"Line Color Inherited: {isLineColorInherited}");
            Console.WriteLine($"Line Weight Inherited: {isLineWeightInherited}");
            Console.WriteLine($"Line Pattern Inherited: {isLinePatternInherited}");
            Console.WriteLine($"Overall Line Inheritance: {isLineInherited}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}