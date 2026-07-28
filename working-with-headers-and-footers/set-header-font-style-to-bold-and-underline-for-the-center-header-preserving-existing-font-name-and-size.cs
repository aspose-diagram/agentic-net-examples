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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the global header/footer font settings
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Preserve existing font name (FaceName) and size (Height)
            // Apply bold style by setting the weight to 700 and enable underline
            headerFont.Weight = 700;          // Bold
            headerFont.Underline = BOOL.True; // Underline

            // Save the diagram with the updated header formatting
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
