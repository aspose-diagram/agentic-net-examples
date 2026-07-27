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

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Define position and size for the oval (ellipse)
                double pinX = 5.0;   // center X in inches
                double pinY = 5.0;   // center Y in inches
                double width = 3.0;  // width in inches
                double height = 2.0; // height in inches

                // Add an oval shape using the built‑in "Ellipse" master
                long shapeId = page.AddShape(pinX, pinY, width, height, "Ellipse");

                // Retrieve the shape object from the page
                Shape oval = page.Shapes.GetShape(shapeId);

                // Set the line cap style to round (smooth line ends)
                // BOOL.True corresponds to a rounded line cap
                oval.Line.LineCap.Value = BOOL.True;

                // Save the diagram to a VSDX file to verify the change
                diagram.Save("OvalWithRoundCap.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Oval shape created with rounded line caps and saved as OvalWithRoundCap.vsdx");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }