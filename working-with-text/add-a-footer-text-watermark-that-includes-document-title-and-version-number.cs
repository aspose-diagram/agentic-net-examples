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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve built‑in document properties
            string title = diagram.DocumentProps.Title ?? "Untitled";
            string version = diagram.Version ?? "1.0";

            // Compose the footer watermark text
            string footerText = $"{title} v{version}";

            // Apply the watermark to the center of the footer
            diagram.HeaderFooter.FooterCenter = footerText;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
