using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Define the position (PinX, PinY) where the diamond will be placed
            double pinX = 4.0; // inches from the left edge of the page
            double pinY = 5.0; // inches from the top edge of the page

            // Add a diamond shape with a width and height of 2 inches each
            // Master name "Diamond" corresponds to the built‑in Visio diamond shape
            page.AddShape(pinX, pinY, 2.0, 2.0, "Diamond");

            // Save the diagram to a file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
