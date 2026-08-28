using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Select the shape you want to reposition (e.g., the first shape on the page)
            Shape shape = page.Shapes[0];

            // Define the offset in inches (positive X moves right, positive Y moves down)
            double offsetX = 1.0; // move 1 inch to the right
            double offsetY = 0.5; // move 0.5 inch down

            // Reposition the shape without altering its size
            shape.Move(offsetX, offsetY);

            // Refresh internal geometry data after moving
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
