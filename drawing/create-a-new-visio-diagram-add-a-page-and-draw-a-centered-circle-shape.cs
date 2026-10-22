using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "CircleDiagram.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // The diagram already contains a default page; retrieve it
            Page page = diagram.Pages[0];

            // Draw a centered circle (ellipse with equal width and height)
            // PinX and PinY define the center of the shape.
            // Width and Height define the size of the circle.
            double centerX = 5.0; // inches from the page origin
            double centerY = 5.0;
            double diameter = 2.0; // inches
            long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

            // Optional: add a label to the circle
            Shape circleShape = page.Shapes.GetShape(shapeId);
            if (circleShape != null)
            {
                // Clear any existing text and add new text
                circleShape.Text.Value.Clear();
                circleShape.Text.Value.Add(new Txt("Circle"));
            }

            // Save the diagram in VSDX format
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