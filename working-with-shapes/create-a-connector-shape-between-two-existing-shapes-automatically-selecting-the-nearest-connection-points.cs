using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the diagram has at least one page
            Page page = diagram.Pages[0];

            // Find the two shapes by their universal names (adjust names as needed)
            Shape shapeA = null;
            Shape shapeB = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == "ShapeA")
                    shapeA = s;
                else if (s.NameU == "ShapeB")
                    shapeB = s;
            }

            if (shapeA == null || shapeB == null)
            {
                Console.WriteLine("One or both shapes not found. Ensure shapes named 'ShapeA' and 'ShapeB' exist.");
                return;
            }

            // Determine the nearest connection points based on horizontal positions
            // If shapeA is left of shapeB, connect from Right of A to Left of B, otherwise reverse.
            ConnectionPointPlace placeFrom = shapeA.XForm.PinX.Value < shapeB.XForm.PinX.Value
                ? ConnectionPointPlace.Right
                : ConnectionPointPlace.Left;

            ConnectionPointPlace placeTo = placeFrom == ConnectionPointPlace.Right
                ? ConnectionPointPlace.Left
                : ConnectionPointPlace.Right;

            // Add a dynamic connector shape (position will be adjusted after connecting)
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

            // Connect the two shapes using the chosen connection points
            page.ConnectShapesViaConnector(shapeA.ID, placeFrom, shapeB.ID, placeTo, connectorId);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Connector added and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
