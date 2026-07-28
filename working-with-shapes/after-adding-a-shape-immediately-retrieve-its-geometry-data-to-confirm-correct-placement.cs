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

                // Define shape parameters
                double pinX = 2.0; // X coordinate of the shape's pin (center) in inches
                double pinY = 3.0; // Y coordinate of the shape's pin (center) in inches
                string masterName = "Rectangle"; // Use a built‑in master name

                // Add the shape to the active page; AddShape returns the shape's unique ID (long)
                long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName);

                // Retrieve the concrete Shape object using the returned ID
                // GetShape expects an int, so cast the long ID accordingly
                Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

                // Access geometry data from the shape's XForm section
                double actualPinX = shape.XForm.PinX.Value;
                double actualPinY = shape.XForm.PinY.Value;
                double actualWidth = shape.XForm.Width.Value;
                double actualHeight = shape.XForm.Height.Value;

                // Output the geometry data to verify correct placement
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"PinX: {actualPinX} inches");
                Console.WriteLine($"PinY: {actualPinY} inches");
                Console.WriteLine($"Width: {actualWidth} inches");
                Console.WriteLine($"Height: {actualHeight} inches");

                // Simple validation: ensure the retrieved PinX/Y match the values we set
                if (Math.Abs(actualPinX - pinX) > 0.001 || Math.Abs(actualPinY - pinY) > 0.001)
                {
                    throw new Exception("Shape position does not match the expected coordinates.");
                }

                // Optional: save the diagram to verify visually (not required by the task)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }