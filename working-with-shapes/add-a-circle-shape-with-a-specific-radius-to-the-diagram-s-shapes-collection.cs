using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the active page (first page) to add the circle
            Page page = diagram.ActivePage;

            // Define the desired radius for the circle
            double radius = 2.0; // example radius in diagram units

            // Width and height of the ellipse must be twice the radius to form a circle
            double width = radius * 2;
            double height = radius * 2;

            // Position of the circle's center (PinX, PinY). Adjust as needed.
            double pinX = 5.0;
            double pinY = 5.0;

            // Add an ellipse master shape; setting isCalculate to false (no immediate layout)
            long shapeId = page.AddShape(pinX, pinY, width, height, "Ellipse", false);

            // Retrieve the newly added shape for any further customization (optional)
            Shape circleShape = page.Shapes.GetShape(shapeId);

            // Example: set the shape's line color to black (optional customization)
            // Note: Fill and line properties use specific members; here we set line weight as an example.
            circleShape.Line.LineWeight.Value = 0.03; // line weight in inches

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}