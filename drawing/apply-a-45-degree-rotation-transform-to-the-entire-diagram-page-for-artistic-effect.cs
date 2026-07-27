using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the active page of the diagram
            Page page = diagram.ActivePage;

            // 45 degrees expressed in radians (SetAngle expects radians)
            double angleRad = 45.0 * Math.PI / 180.0;

            // Rotate every shape on the page by 45 degrees
            foreach (Shape shape in page.Shapes)
            {
                shape.SetAngle(angleRad);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
