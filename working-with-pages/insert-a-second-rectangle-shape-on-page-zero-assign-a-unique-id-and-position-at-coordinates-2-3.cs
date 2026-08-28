using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one if needed)
        Diagram diagram = new Diagram();

        // Ensure there is at least one page; Aspose.Diagram creates a default page on construction
        Page pageZero = diagram.Pages[0];

        // Insert a rectangle shape at coordinates (PinX = 2, PinY = 3)
        // Width and Height are set to 1 inch each (adjust as required)
        long rectangleId = pageZero.DrawRectangle(2.0, 3.0, 1.0, 1.0);

        // rectangleId now holds the unique ID of the newly added rectangle shape
        // Further processing can be done using this ID if needed
    }
}
