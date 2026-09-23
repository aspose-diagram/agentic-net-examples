using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the global header/footer font settings
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Preserve existing font name (FaceName) and size (Height)
            // and set the style to bold and underline
            headerFont.Weight = 700;          // 700 = Bold
            headerFont.Underline = BOOL.True; // Enable underline

            // Optionally, you can set or keep the center header text
            // diagram.HeaderFooter.HeaderCenter = "Your Center Header Text";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
