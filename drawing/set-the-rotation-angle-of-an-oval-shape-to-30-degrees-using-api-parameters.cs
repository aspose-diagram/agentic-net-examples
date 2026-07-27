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

            // Access the page that contains the oval shape (e.g., first page)
            Page page = diagram.Pages[0];

            // Retrieve the oval shape by its ID (replace 1 with the actual shape ID)
            Shape oval = page.Shapes.GetShape(1);

            // Convert 30 degrees to radians (Aspose.Diagram expects radians)
            double angleInRadians = 30.0 * Math.PI / 180.0;

            // Set the rotation angle of the oval shape
            oval.SetAngle(angleInRadians);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
