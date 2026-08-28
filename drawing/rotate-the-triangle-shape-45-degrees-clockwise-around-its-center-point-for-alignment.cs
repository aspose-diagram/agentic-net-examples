using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Find the triangle shape (assuming its master name is "Triangle")
            Aspose.Diagram.Shape triangleShape = null;
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Master name "Triangle" identifies the triangle shape
                    if (shape.Master != null && shape.Master.NameU == "Triangle")
                    {
                        triangleShape = shape;
                        break;
                    }
                }
                if (triangleShape != null) break;
            }

            // If the triangle shape was found, rotate it 45 degrees clockwise around its center
            if (triangleShape != null)
            {
                // Clockwise rotation is negative in radians (Visio uses counter‑clockwise as positive)
                double angleRadians = -Math.PI / 4; // -45 degrees
                triangleShape.SetAngle(angleRadians);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
