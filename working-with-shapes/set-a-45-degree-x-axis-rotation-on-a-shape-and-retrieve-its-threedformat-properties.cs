using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: PinX, PinY, Width, Height, MasterName
                long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle");

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set a 45‑degree rotation around the X‑axis
                shape.ThreeDFormat.RotationXAngle.Value = 45.0;

                // Optionally set other 3D format properties for completeness
                shape.ThreeDFormat.RotationYAngle.Value = 0.0;
                shape.ThreeDFormat.RotationZAngle.Value = 0.0;
                shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Parallel;
                shape.ThreeDFormat.Perspective.Value = 0.0;
                shape.ThreeDFormat.DistanceFromGround.Value = 0.0;
                shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

                // Retrieve and display the 3D format properties
                Console.WriteLine("ThreeDFormat properties of the shape:");
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
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }