using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Assume the pentagon is on the first page; adjust the page index if needed
            Aspose.Diagram.Page page = diagram.Pages[0];

            // Find the pentagon shape by its name (or by ID if you know it)
            // Here we look for a shape whose NameU equals "Pentagon"
            Aspose.Diagram.Shape pentagon = null;
            foreach (Aspose.Diagram.Shape shp in page.Shapes)
            {
                if (shp.NameU != null && shp.NameU.Equals("Pentagon", System.StringComparison.OrdinalIgnoreCase))
                {
                    pentagon = shp;
                    break;
                }
            }

            // If the shape was not found, you may need to locate it by its ID instead
            if (pentagon == null)
            {
                // Example: use a known shape ID (replace 5 with the actual ID)
                pentagon = page.Shapes.GetShape(5);
            }

            // Rotate the pentagon 30 degrees (π/6 radians) around its geometric center
            // The shape's pin point is its center of rotation, so no additional move is required
            double angleInRadians = System.Math.PI / 6.0; // 30 degrees
            pentagon.SetAngle(angleInRadians);

            // Save the modified diagram (replace with your desired output path and format)
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
