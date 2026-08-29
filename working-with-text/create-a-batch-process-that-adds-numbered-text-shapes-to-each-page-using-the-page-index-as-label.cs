using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Loop through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Position (in inches) where the text will be placed on each page
                double pinX = 1.0;   // X coordinate
                double pinY = 1.0;   // Y coordinate
                double width = 2.0; // Width of the text box
                double height = 0.5; // Height of the text box

                // Text to display – using the page index (1‑based) as the label
                string text = $"Page {i + 1}";

                // Add the text shape to the current page
                page.AddText(pinX, pinY, width, height, text);
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
