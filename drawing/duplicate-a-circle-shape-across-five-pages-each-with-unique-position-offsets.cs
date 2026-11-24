using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "DuplicatedCircles.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Define base circle parameters
            double basePinX = 5.0; // base X position
            double basePinY = 5.0; // base Y position
            double circleSize = 2.0; // width and height to make it a circle
            string masterName = "Ellipse"; // Visio master for an ellipse (circle when width == height)

            // Create five pages and place a circle on each with a unique offset
            for (int i = 0; i < 5; i++)
            {
                // Add a new page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Calculate offset for this page
                double offsetX = basePinX + i * 2.0; // shift 2 units right per page
                double offsetY = basePinY + i * 1.0; // shift 1 unit up per page

                // Add the ellipse shape (returns the shape ID)
                long shapeId = page.AddShape(offsetX, offsetY, masterName, false);

                // Retrieve the shape object using the ID
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Ensure the shape is a circle by setting equal width and height
                shape.XForm.Width.Value = circleSize;
                shape.XForm.Height.Value = circleSize;

                // Optional: set a fill color for visual distinction
                shape.Fill.FillPattern.Value = 1; // solid fill
                shape.Fill.FillForegnd.Value = "#FFCC00"; // orange fill

                // Optional: set a line color
                shape.Line.LineColor.Value = "#000000"; // black border
            }

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