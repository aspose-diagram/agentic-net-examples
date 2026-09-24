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

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Define shape placement parameters
                double pinX = 2.0;          // X coordinate (inches)
                double pinY = 2.0;          // Y coordinate (inches)
                string masterName = "Rectangle"; // Built‑in master name
                bool isCalculate = false;  // Do not recalculate geometry automatically

                // Add the shape to the page; AddShape returns the shape ID (long)
                long shapeId = page.AddShape(pinX, pinY, masterName, isCalculate);

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Extract geometry data from the shape
                double actualPinX = shape.XForm.PinX.Value;
                double actualPinY = shape.XForm.PinY.Value;
                double actualWidth = shape.XForm.Width.Value;
                double actualHeight = shape.XForm.Height.Value;
                double actualAngle = shape.XForm.Angle.Value; // rotation angle in degrees

                // Verify that the geometry matches the expected placement
                if (Math.Abs(actualPinX - pinX) > 0.001 ||
                    Math.Abs(actualPinY - pinY) > 0.001)
                {
                    throw new Exception($"Shape position mismatch. Expected ({pinX}, {pinY}) but got ({actualPinX}, {actualPinY}).");
                }

                // Output the retrieved geometry for confirmation
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"Position: PinX = {actualPinX}, PinY = {actualPinY}");
                Console.WriteLine($"Size: Width = {actualWidth}, Height = {actualHeight}");
                Console.WriteLine($"Rotation Angle: {actualAngle} degrees");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }