using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page (ActivePage is created by default)
            Page page = diagram.ActivePage;

            // Add a rectangle shape at (PinX=2, PinY=2)
            // The fourth argument 'isCalculate' must be a bool (false means no automatic layout calculation)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the newly added shape using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Convert 3 centimeters to inches (1 cm = 0.393701 inches)
            double widthInInches = 3.0 * 0.393701;

            // Set the shape's width to 3 cm (height remains default)
            shape.XForm.Width.Value = widthInInches;

            // Optionally, you could set the height as well if desired
            // shape.XForm.Height.Value = widthInInches;

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}