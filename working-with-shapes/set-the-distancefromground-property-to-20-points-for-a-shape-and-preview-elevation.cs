using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example: shape with ID 1)
                // Adjust the ID as needed for your specific diagram
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the distance from ground to 20 points (1 point = 1/72 inch)
                // The property expects a double value representing points
                shape.ThreeDFormat.DistanceFromGround.Value = 20.0;

                // Optionally, adjust elevation angle for preview (e.g., rotate around X axis)
                // Here we set a 30-degree elevation (converted to radians as required by SetAngle)
                double elevationDegrees = 30.0;
                double elevationRadians = (Math.PI / 180.0) * elevationDegrees;
                shape.ThreeDFormat.RotationXAngle.Value = elevationRadians;

                // Refresh the shape to apply 3D changes
                shape.RefreshData();

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("DistanceFromGround set to 20 points and elevation preview applied.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }