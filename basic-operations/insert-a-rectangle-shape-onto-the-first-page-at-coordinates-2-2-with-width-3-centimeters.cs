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

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Coordinates (PinX, PinY) in inches
                double pinX = 2.0;
                double pinY = 2.0;

                // Width of 3 centimeters converted to inches (1 cm = 0.393701 inches)
                double widthInches = 3.0 * 0.393701;

                // Add a rectangle shape using the built‑in "Rectangle" master.
                // The fourth parameter (isCalculate) must be a boolean.
                long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

                // Retrieve the shape object to modify its dimensions
                Shape rectangle = page.Shapes.GetShape(shapeId);

                // Set the width to 3 cm (in inches). Height can remain default.
                rectangle.XForm.Width.Value = widthInches;

                // Optional: Save the diagram to a VSDX file to verify the result
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }