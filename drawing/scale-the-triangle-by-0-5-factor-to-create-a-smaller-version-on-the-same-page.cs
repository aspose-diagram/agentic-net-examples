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

            // Assume the triangle is on the first page and is the first shape (adjust as needed)
            Aspose.Diagram.Page page = diagram.Pages[0];
            Aspose.Diagram.Shape triangle = page.Shapes[0];

            // Scale the triangle by a factor of 0.5 (reduce width and height)
            // Keep the pin (center) position unchanged
            if (triangle.XForm != null && triangle.XForm.Width != null && triangle.XForm.Height != null)
            {
                triangle.XForm.Width.Value *= 0.5;
                triangle.XForm.Height.Value *= 0.5;
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
