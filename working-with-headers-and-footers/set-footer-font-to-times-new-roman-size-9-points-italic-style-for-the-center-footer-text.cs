using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty Visio diagram
        using (Diagram diagram = new Diagram())
        {
            // Configure the global footer font (applies to all footer fields)
            var footerFont = diagram.HeaderFooter.HeaderFooterFont;
            footerFont.FaceName = "Times New Roman";   // Font family
            footerFont.Weight = 400;                  // Regular weight (400)
            footerFont.Height = -12;                  // 9 pt => -12 (9 * -1.333 ≈ -12)
            footerFont.Italic = BOOL.True;            // Italic style

            // Set the center footer text (example content)
            diagram.HeaderFooter.FooterCenter = "Center Footer";

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
