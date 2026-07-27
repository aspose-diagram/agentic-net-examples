using System.IO;
using Aspose.Diagram;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the circle shape by its name (replace with actual name or ID if different)
            Shape circleShape = page.Shapes.FirstOrDefault(s => s.Name == "Circle");

            if (circleShape != null)
            {
                // Bring the circle shape to the front of the Z‑order
                circleShape.BringToFront();
            }
            else
            {
                Console.WriteLine("Circle shape not found.");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
