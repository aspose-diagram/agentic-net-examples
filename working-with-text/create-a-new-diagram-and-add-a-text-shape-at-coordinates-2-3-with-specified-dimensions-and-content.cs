using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page (the default page created with the diagram)
            Page page = diagram.ActivePage;

            // Add a text shape at coordinates (2, 3) with width 2, height 1 and the desired text
            // Using the AddText overload that accepts pinX, pinY, width, height and text.
            Shape textShape = page.AddText(
                pinX: 2.0,          // X coordinate of the text pin
                pinY: 3.0,          // Y coordinate of the text pin
                width: 2.0,         // Width of the text box (in inches)
                height: 1.0,        // Height of the text box (in inches)
                text: "Sample Text" // Content of the text shape
            );

            // (Optional) Refresh the shape data to ensure proper layout
            textShape.RefreshData();

            // Save the diagram to a VSDX file using the provided Save method
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
