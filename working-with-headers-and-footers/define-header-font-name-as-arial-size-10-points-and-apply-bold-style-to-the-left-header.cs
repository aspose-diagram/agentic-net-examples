using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing diagram (replace with actual load logic as per your lifecycle rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Set the left header text
            diagram.HeaderFooter.HeaderLeft = "Your Header Text";

            // Configure the header/footer font
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial";   // Font name
            headerFont.Height = 10;          // Font size in points
            headerFont.Weight = 700;         // Bold weight (typically 700)

            // Save the modified diagram (replace with actual save logic as per your lifecycle rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
