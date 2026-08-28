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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Preserve existing font name and size for the header/footer
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            string existingFaceName = headerFont.FaceName;
            int existingHeight = headerFont.Height; // height is stored as an integer (points)

            // Apply bold (Weight = 700) and underline (Underline = BOOL.True) to the center header font
            headerFont.FaceName = existingFaceName; // keep original font name
            headerFont.Height = existingHeight;     // keep original font size
            headerFont.Weight = 700;                // 700 corresponds to bold
            headerFont.Underline = BOOL.True;       // enable underline

            // Optionally set the center header text (preserve existing text if needed)
            // diagram.HeaderFooter.HeaderCenter = "Your Header Text";

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
