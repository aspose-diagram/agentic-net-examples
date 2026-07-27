using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the pentagon and square shapes by their names (replace with actual names/IDs)
            Shape pentagon = page.Shapes.GetShape("Pentagon");
            Shape square   = page.Shapes.GetShape("Square");

            // Group the two shapes into a composite shape
            Shape groupShape = page.Shapes.Group(new Shape[] { pentagon, square });

            // Optional: give the group a meaningful name
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
