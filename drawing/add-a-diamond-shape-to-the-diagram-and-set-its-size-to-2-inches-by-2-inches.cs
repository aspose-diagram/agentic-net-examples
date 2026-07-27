using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (lifecycle rule: create)
            Diagram diagram = new Diagram();

            // Get the first page (or create one if none exists)
            Page page = diagram.Pages[0];

            // Define the position (PinX, PinY) for the diamond shape.
            // Here we place it at (5, 5) inches from the page origin.
            double pinX = 5.0;
            double pinY = 5.0;

            // Define the size of the diamond: 2 inches width and 2 inches height.
            double width = 2.0;
            double height = 2.0;

            // Add a diamond shape using the built‑in master named "Diamond".
            // This uses the AddShape overload that accepts PinX, PinY, Width, Height and master name.
            page.AddShape(pinX, pinY, width, height, "Diamond");

            // Save the diagram to a file (lifecycle rule: save)
            diagram.Save("DiamondShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
