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

            // Set the center footer text
            diagram.HeaderFooter.FooterCenter = "Center Footer Text";

            // Configure the footer font: Times New Roman, 9 pt, italic
            // Height uses a negative mapping: (points * -1.333) rounded → -12 for 9 pt
            diagram.HeaderFooter.HeaderFooterFont.FaceName = "Times New Roman";
            diagram.HeaderFooter.HeaderFooterFont.Height = -12;
            diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;
            // Normal weight (regular) – optional
            diagram.HeaderFooter.HeaderFooterFont.Weight = 400;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
