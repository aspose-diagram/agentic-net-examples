using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one if needed)
        Diagram diagram = new Diagram();

        // Ensure there is at least one page; add one if the diagram is empty
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Access the first page (page index 0)
        Page page = diagram.Pages[0];

        // Define rectangle position and size
        double pinX = 2.0;      // X‑coordinate of the rectangle's pin
        double pinY = 3.0;      // Y‑coordinate of the rectangle's pin
        double width = 1.0;     // Width of the rectangle (in inches)
        double height = 1.0;    // Height of the rectangle (in inches)

        // Draw the rectangle on the page; the method returns a unique shape ID
        long rectangleId = page.DrawRectangle(pinX, pinY, width, height);

        // rectangleId now holds the unique identifier for the newly added shape
        // (additional shape customization can be performed here if required)

        // Save the diagram to a file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
