using System.IO;
using System;
using Aspose.Diagram;

class VerifyShapeGlueAndConnection
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // Replace "input.vsdx" with the path to your diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // IDs of the two shapes to verify.
            // Set these to the actual shape IDs you want to check.
            long shapeId1 = 1;   // example ID for the first shape
            long shapeId2 = 2;   // example ID for the second shape

            // Retrieve the shapes from the diagram by their IDs
            Shape shape1 = diagram.Pages[0].Shapes.GetShape(shapeId1);
            Shape shape2 = diagram.Pages[0].Shapes.GetShape(shapeId2);

            // Verify whether the two shapes are glued
            bool areGlued = shape1.IsGlued(shape2);
            // Verify whether the two shapes are connected (e.g., a connector)
            bool areConnected = shape1.IsConnected(shape2);

            // Output the verification results
            Console.WriteLine($"Shape {shapeId1} and Shape {shapeId2} glued: {areGlued}");
            Console.WriteLine($"Shape {shapeId1} and Shape {shapeId2} connected: {areConnected}");

            // Optionally, save the diagram if any changes were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
