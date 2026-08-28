using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Light gray is typically the third standard color (index 2) in the Visio color table
            const int LightGrayFillStyleId = 2;

            // Apply the light‑gray fill style to every page in the document
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Keep existing text and line styles (-1) and set only the fill style
                page.ApplyStyle(textStyle: -1, lineStyle: -1, fillStyle: LightGrayFillStyleId);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
