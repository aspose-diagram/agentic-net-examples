using System.IO;
using Aspose.Diagram;
using System;

class WatermarkMacro
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the master shape that represents the watermark
            string watermarkMaster = "Watermark";

            // Iterate through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Position where the watermark will be placed (example: center of page)
                double pinX = 5.0; // X coordinate in inches
                double pinY = 5.0; // Y coordinate in inches

                // Append the watermark shape to the current page
                page.AddShape(pinX, pinY, watermarkMaster);
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
