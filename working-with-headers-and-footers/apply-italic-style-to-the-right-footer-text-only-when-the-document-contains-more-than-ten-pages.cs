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
            string inputPath = "input.vsdx";   // TODO: replace with actual file path
            Diagram diagram = new Diagram(inputPath);

            // Check if the document has more than ten pages
            if (diagram.Pages.Count > 10)
            {
                // Set the right footer text
                diagram.HeaderFooter.FooterRight = "Sample Right Footer";

                // Apply italic style to the footer font
                // HeaderFooterFont uses BOOL enumeration for style flags
                diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx"; // TODO: replace with desired output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
