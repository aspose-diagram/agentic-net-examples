using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve an existing rectangle shape (assumed to have ID 1)
            Shape rectangle = page.Shapes.GetShape(1);

            // Draw a new ellipse on the page
            // Parameters: pinX, pinY, width, height
            long ellipseId = page.DrawEllipse(2.0, 2.0, 1.5, 1.0);
            Shape ellipse = page.Shapes.GetShape(ellipseId);

            // Group the rectangle and the newly drawn ellipse
            Shape groupShape = page.Shapes.Group(new Shape[] { rectangle, ellipse });

            // Assign a common name to the group shape
            groupShape.Name = "RectEllipseGroup";

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
