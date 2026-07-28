using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the active page
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape object
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Apply a preset theme to the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Apply 3‑D rotation to create a perspective view
                shape.ThreeDFormat.RotationXAngle.Value = 30; // degrees
                shape.ThreeDFormat.RotationYAngle.Value = 20;
                shape.ThreeDFormat.RotationZAngle.Value = 10;
                shape.ThreeDFormat.RotationType.Value = RotationTypeValue.ObliqueFromBottomLeft;
                shape.ThreeDFormat.Perspective.Value = 30; // perspective depth
                shape.ThreeDFormat.DistanceFromGround.Value = 0;
                shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

                // Save the diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }