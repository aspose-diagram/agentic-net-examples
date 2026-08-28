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

            // Access the global header/footer font settings
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Retrieve the current Italic setting
            BOOL originalItalic = headerFont.Italic;
            Console.WriteLine($"Original Italic setting: {(originalItalic == BOOL.True ? "True" : "False")}");

            // Change the header font style to Italic
            headerFont.Italic = BOOL.True;

            // Verify that the change was applied
            if (headerFont.Italic != BOOL.True)
            {
                throw new Exception("Failed to set header font to Italic.");
            }
            Console.WriteLine("Header font successfully set to Italic.");

            // Optionally, save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
