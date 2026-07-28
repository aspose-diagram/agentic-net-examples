using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string diagramPath = "input.vsdx";

            // IDs of the two shapes to verify
            long shapeIdA = 1;
            long shapeIdB = 2;

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram(diagramPath);

            // Assume both shapes are on the first page
            Page page = diagram.Pages[0];

            // Retrieve the shapes by their IDs
            Shape shapeA = page.Shapes.GetShape(shapeIdA);
            Shape shapeB = page.Shapes.GetShape(shapeIdB);

            // Verify if the shapes are glued
            bool isGlued = shapeA.IsGlued(shapeB);

            // Verify if the shapes are connected
            bool isConnected = shapeA.IsConnected(shapeB);

            // Output the results
            Console.WriteLine($"Shapes {shapeIdA} and {shapeIdB} glued: {isGlued}");
            Console.WriteLine($"Shapes {shapeIdA} and {shapeIdB} connected: {isConnected}");

            // (Optional) Save the diagram if any changes were made, using the provided save rule
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
