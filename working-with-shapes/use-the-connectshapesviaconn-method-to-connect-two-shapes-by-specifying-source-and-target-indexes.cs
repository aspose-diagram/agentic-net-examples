using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path for the generated diagram
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram instance
            Diagram diagram = new Diagram();

            // Add a single blank page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Add the first rectangle shape (source) at position (2,2)
            // The fourth parameter 'isCalculate' must be a native bool, not BOOL
            long sourceShapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Add the second rectangle shape (target) at position (5,5)
            long targetShapeId = page.AddShape(5.0, 5.0, "Rectangle", false);

            // Add a dynamic connector shape that will be used to link the two rectangles
            long connectorShapeId = page.AddShape(0.0, 0.0, "Dynamic connector", false);

            // Connect the source shape to the target shape using connection indexes.
            // Here we use connection index 1 for both shapes (default first connection point).
            page.ConnectShapesViaConnectorIndex(
                sourceShapeId,   // ID of the source shape
                1,               // Connection index on the source shape
                targetShapeId,   // ID of the target shape
                1,               // Connection index on the target shape
                connectorShapeId // ID of the connector shape
            );

            // Save the diagram to a VSDX file using the correct SaveFileFormat enum member
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}