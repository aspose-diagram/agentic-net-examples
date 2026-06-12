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

            // Access page five (zero‑based index 4)
            Page pageFive = diagram.Pages[4];

            // IDs for the Caption style – replace with actual IDs from your document if different
            int captionTextStyleId = 2;   // example text style ID for "Caption"
            int captionLineStyleId = -1;  // keep existing line style
            int captionFillStyleId = -1;  // keep existing fill style

            // Apply the Caption style to all shapes on page five
            pageFive.ApplyStyle(captionTextStyleId, captionLineStyleId, captionFillStyleId);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
