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

            // Work with the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve the pentagon and square shapes (by name or ID)
            Shape pentagon = page.Shapes.GetShape("Pentagon");
            Shape square   = page.Shapes.GetShape("Square");

            // Group the two shapes into a composite shape collection
            Shape[] shapesToGroup = new Shape[] { pentagon, square };
            Shape groupShape = page.Shapes.Group(shapesToGroup);

            // Optionally assign a name to the new group shape
            groupShape.Name = "PentagonSquareGroup";

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
