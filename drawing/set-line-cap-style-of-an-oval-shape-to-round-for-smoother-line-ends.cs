using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first page (active page)
                Page page = diagram.ActivePage;

                // Define position and size for the oval (ellipse)
                double pinX = 5.0;   // X coordinate of the shape's center
                double pinY = 5.0;   // Y coordinate of the shape's center
                double width = 3.0;  // Width in inches
                double height = 2.0; // Height in inches

                // Draw an oval (ellipse) on the page
                long shapeId = page.DrawEllipse(pinX, pinY, width, height);

                // Retrieve the Shape object using the returned ID
                Shape oval = page.Shapes.GetShape(shapeId);

                // Set the line cap style to round (BOOL.True) for smoother line ends
                oval.Line.LineCap.Value = BOOL.True;

                // Optional: Save the diagram to a VSDX file
                diagram.Save("OvalWithRoundCap.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }