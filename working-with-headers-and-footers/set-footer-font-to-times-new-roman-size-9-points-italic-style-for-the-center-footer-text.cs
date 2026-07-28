using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Set the center footer text
            diagram.HeaderFooter.FooterCenter = "Center Footer Text";

            // Configure the footer font: Times New Roman, 9 pt, italic
            var footerFont = diagram.HeaderFooter.HeaderFooterFont;
            footerFont.FaceName = "Times New Roman";
            footerFont.Height = 9;               // point size
            footerFont.Italic = BOOL.True;       // italic style
            footerFont.Weight = 400;             // normal weight (optional)

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
