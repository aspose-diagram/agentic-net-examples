using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Apply italic style to the right footer only if the document has more than ten pages
            if (diagram.Pages.Count > 10)
            {
                // Set the right footer text (using Visio field code for page number as an example)
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Configure the footer font to be italic
                var footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.Italic = BOOL.True;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
