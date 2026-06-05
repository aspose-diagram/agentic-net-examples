using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Rotation angle: 30 degrees expressed in radians
            double angleRad = Math.PI / 6.0;

            // Locate the pentagon shape and apply the rotation
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify the pentagon by its name (adjust if needed)
                    if (shape.NameU != null && shape.NameU.Equals("Pentagon", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set the new angle (counter‑clockwise) around the shape's pin (center)
                        shape.SetAngle(angleRad);
                    }
                }
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
