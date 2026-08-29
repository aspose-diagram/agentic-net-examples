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

                // Ensure there is at least one page (the default diagram contains one)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // PinX and PinY are the center of the shape; initial width and height are arbitrary
                double initialPinX = 5.0;
                double initialPinY = 5.0;
                double initialWidth = 2.0;
                double initialHeight = 1.0;
                long shapeId = page.AddShape(initialPinX, initialPinY, initialWidth, initialHeight, "Rectangle");

                // Retrieve the shape instance
                Shape shape = page.Shapes.GetShape(shapeId);

                // Define new dimensions to set
                double newWidth = 3.75;
                double newHeight = 2.5;

                // Apply the new dimensions using SetWidth and SetHeight
                shape.SetWidth(newWidth);
                shape.SetHeight(newHeight);

                // Tolerance for floating‑point comparison
                double tolerance = 0.001;

                // Verify width
                double actualWidth = shape.XForm.Width.Value;
                if (Math.Abs(actualWidth - newWidth) > tolerance)
                {
                    throw new Exception($"Width verification failed. Expected: {newWidth}, Actual: {actualWidth}");
                }
                else
                {
                    Console.WriteLine($"Width set correctly: {actualWidth}");
                }

                // Verify height
                double actualHeight = shape.XForm.Height.Value;
                if (Math.Abs(actualHeight - newHeight) > tolerance)
                {
                    throw new Exception($"Height verification failed. Expected: {newHeight}, Actual: {actualHeight}");
                }
                else
                {
                    Console.WriteLine($"Height set correctly: {actualHeight}");
                }

                // Optional: Save the diagram to verify visually (not required for the test)
                // diagram.Save("TestDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }