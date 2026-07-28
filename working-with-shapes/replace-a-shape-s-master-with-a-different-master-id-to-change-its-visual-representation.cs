using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the page (0‑based index) and the ID of the shape whose master will be changed
            int pageIndex = 0;          // first page
            long shapeId = 5;           // ID of the target shape

            // Retrieve the shape from the page
            Shape shape = diagram.Pages[pageIndex].Shapes.GetShape(shapeId);

            // Specify the ID of the new master that already exists in the diagram's Masters collection
            int newMasterId = 10;       // ID of the master to apply

            // Replace the shape's master with the new master
            shape.Master = diagram.Masters[newMasterId];

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
