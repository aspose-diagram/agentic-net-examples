using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "AutoSpacedDiagram.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Retrieve the first (default) page
            Page page = diagram.Pages[0];

            // Add three rectangle shapes to the page
            // Parameters: pinX, pinY, master name, page index
            long shapeId1 = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
            long shapeId2 = diagram.AddShape(3.0, 1.0, "Rectangle", 0);
            long shapeId3 = diagram.AddShape(5.0, 1.0, "Rectangle", 0);

            // Configure AutoSpaceOptions with a custom vertical distance
            AutoSpaceOptions options = new AutoSpaceOptions();
            // Set the vertical gap between shapes (units are inches)
            options.DistanceInVertical = 2.0; // Custom vertical spacing
            // Optionally set horizontal distance if needed
            // options.DistanceInHorizontal = 1.0;

            // Apply auto‑spacing to all shapes on the page using the configured options
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}' with custom vertical spacing.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}