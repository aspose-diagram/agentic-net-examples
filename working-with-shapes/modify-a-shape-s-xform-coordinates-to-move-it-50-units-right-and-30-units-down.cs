using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape to move (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Move the shape 50 units right and 30 units down (units are inches)
            shape.Move(50.0, 30.0);

            // Refresh shape data after moving
            shape.RefreshData();

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
