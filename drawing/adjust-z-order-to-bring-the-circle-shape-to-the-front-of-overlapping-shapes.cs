using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the circle (ellipse) shape.
            // Visio uses the master name "Ellipse" for circles; adjust the condition if your shape has a different name.
            Shape circleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (!string.IsNullOrEmpty(shape.NameU) &&
                    shape.NameU.IndexOf("Ellipse", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    circleShape = shape;
                    break;
                }
            }

            // If the shape was found, bring it to the front of the Z‑order.
            if (circleShape != null)
            {
                circleShape.BringToFront();   // Brings the shape to the front of overlapping shapes
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
