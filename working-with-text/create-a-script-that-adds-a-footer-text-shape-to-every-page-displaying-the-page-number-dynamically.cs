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

            // Iterate through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Position for the footer text (centered near the bottom of the page)
                // PinX is the X coordinate of the text's center, PinY is the Y coordinate.
                double pinX = 5.0;   // adjust as needed for your page width
                double pinY = 0.5;   // distance from the bottom edge
                double width = 2.0;  // width of the text box
                double height = 0.5; // height of the text box

                // Create the footer text displaying the page number
                string footerText = $"Page {i + 1}";

                // Add the text shape to the current page
                page.AddText(pinX, pinY, width, height, footerText);
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
