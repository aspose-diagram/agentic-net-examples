using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (uses the provided creation rule)
        Diagram diagram = new Diagram();

        // Ensure there is at least one page; the default diagram contains a page at index 0
        Page page = diagram.Pages[0];

        // Define rectangle position (PinX, PinY) and size (Width, Height)
        double pinX = 2.0;      // X‑coordinate of the rectangle's pin
        double pinY = 3.0;      // Y‑coordinate of the rectangle's pin
        double width = 1.0;     // Width of the rectangle (in inches)
        double height = 1.0;    // Height of the rectangle (in inches)

        // Draw the rectangle on page zero; the method returns a unique shape ID
        long rectangleId = page.DrawRectangle(pinX, pinY, width, height);

        // (Optional) Use the returned ID for further processing
        // Console.WriteLine($"Rectangle shape ID: {rectangleId}");

        // Save the diagram (uses the provided saving rule)
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
