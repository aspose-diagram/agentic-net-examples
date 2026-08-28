using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Set the left header text (optional, can be any string)
            diagram.HeaderFooter.HeaderLeft = "My Header";

            // Access the header/footer font object
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Define the font name as Arial
            headerFont.FaceName = "Arial";

            // Define the font size as 10 points
            headerFont.Height = 10;

            // Apply bold style (weight 700 corresponds to bold in GDI)
            headerFont.Weight = 700;

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
