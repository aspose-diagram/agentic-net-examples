using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID.
                // Replace 1 with the actual shape ID you want to test.
                Shape shape = page.Shapes.GetShape(1);

                // Desired rotation angle in degrees
                double targetAngle = 30.0;

                // Apply rotation to the shape
                shape.SetAngle(targetAngle);

                // Read back the angle from the shape's Angle cell
                double actualAngle = shape.XForm.Angle.Value;

                // Verify that the angle was set correctly
                const double tolerance = 0.001;
                if (Math.Abs(actualAngle - targetAngle) > tolerance)
                {
                    throw new Exception($"Rotation verification failed. Expected: {targetAngle}, Actual: {actualAngle}");
                }
                else
                {
                    Console.WriteLine($"Rotation verification succeeded. Angle = {actualAngle} degrees.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }