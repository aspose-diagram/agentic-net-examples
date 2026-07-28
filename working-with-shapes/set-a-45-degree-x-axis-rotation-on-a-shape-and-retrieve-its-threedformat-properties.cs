using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page at position (2,2)
            double pinX = 2.0;
            double pinY = 2.0;
            string masterName = "Rectangle";

            // The fourth parameter indicates whether to calculate geometry; set to false
            long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName, false);

            // Retrieve the shape instance using its ID
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Set a 45‑degree rotation around the X‑axis
            shape.ThreeDFormat.RotationXAngle.Value = 45;

            // Retrieve and display various ThreeDFormat properties
            Console.WriteLine($"RotationXAngle: {shape.ThreeDFormat.RotationXAngle.Value}");
            Console.WriteLine($"RotationYAngle: {shape.ThreeDFormat.RotationYAngle.Value}");
            Console.WriteLine($"RotationZAngle: {shape.ThreeDFormat.RotationZAngle.Value}");
            Console.WriteLine($"RotationType: {shape.ThreeDFormat.RotationType.Value}");
            Console.WriteLine($"Perspective: {shape.ThreeDFormat.Perspective.Value}");
            Console.WriteLine($"DistanceFromGround: {shape.ThreeDFormat.DistanceFromGround.Value}");
            Console.WriteLine($"KeepTextFlat: {shape.ThreeDFormat.KeepTextFlat.Value}");

            // Save the diagram (optional)
            diagram.Save("RotatedShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
