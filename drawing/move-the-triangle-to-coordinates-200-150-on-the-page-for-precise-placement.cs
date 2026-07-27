using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            var page = diagram.Pages[0];

            // Find the triangle shape by its name (replace "Triangle" with the actual shape name if different)
            Aspose.Diagram.Shape triangle = null;
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                if (shape.NameU == "Triangle")
                {
                    triangle = shape;
                    break;
                }
            }

            // If the triangle shape is found, move it to the absolute coordinates (200, 150)
            if (triangle != null)
            {
                triangle.MoveTo(200.0, 150.0);
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
