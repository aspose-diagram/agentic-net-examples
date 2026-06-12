using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2,2) inches
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the distance from ground to 20 points (elevation)
                shape.ThreeDFormat.DistanceFromGround.Value = 20;

                // Optional: keep the text flat while the shape is elevated
                shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

                // Save the diagram in Visio format
                diagram.Save("ElevatedShape.vsdx", SaveFileFormat.Vsdx);

                // Export a preview image (PNG) to visualize the elevation
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save("ElevatedShapePreview.png", imgOptions);

                Console.WriteLine("Shape elevation set and files saved successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }