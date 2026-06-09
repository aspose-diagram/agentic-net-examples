using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the header/footer font object
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Set font name to Arial
            headerFont.FaceName = "Arial";

            // Set font size to 10 points
            headerFont.Height = 10;

            // Apply bold style (weight 700 corresponds to bold)
            headerFont.Weight = 700;

            // (Optional) Set the left header text if needed
            // diagram.HeaderFooter.HeaderLeft = "Your Header Text";

            // Save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
