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

            // Identify the shape to move (e.g., first shape on the first page)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[1]; // index 0 is the page background

            // Define the offset in inches (Visio uses inches for positioning)
            double offsetX = 1.5; // move 1.5 inches to the right
            double offsetY = 0.75; // move 0.75 inches upward

            // Reposition the shape by applying the offset.
            // This moves the shape without altering its geometry or size.
            shape.Move(offsetX, offsetY);

            // Refresh shape data to ensure internal geometry is updated
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
