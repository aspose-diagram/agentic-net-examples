using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram(@"input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Draw a new ellipse on the page
            // (pinX, pinY) = center of the ellipse, width and height define its size
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 2.0;
            double height = 1.0;
            long ellipseId = page.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the newly created ellipse shape
            Shape ellipseShape = page.Shapes.GetShape(ellipseId);

            // Retrieve an existing rectangle shape.
            // Replace 1 with the actual ID of the rectangle you want to group.
            long rectangleId = 1;
            Shape rectangleShape = page.Shapes.GetShape(rectangleId);

            // Group the rectangle and the ellipse together
            Shape[] groupItems = new Shape[] { rectangleShape, ellipseShape };
            Shape groupShape = page.Shapes.Group(groupItems);

            // Assign a common name to the group shape
            // The Name property is used to identify the shape within the diagram.
            groupShape.Name = "RectangleEllipseGroup";

            // Save the modified diagram (replace with your desired output path)
            diagram.Save(@"output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
