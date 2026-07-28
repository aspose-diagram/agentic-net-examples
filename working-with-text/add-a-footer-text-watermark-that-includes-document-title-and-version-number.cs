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

            // Retrieve document title and version
            string title = diagram.DocumentProps.Title ?? string.Empty;
            string version = diagram.Version ?? string.Empty;

            // Compose footer watermark text
            string footerText = $"{title} - Version {version}";

            // Assign the watermark to the right side of the footer
            diagram.HeaderFooter.FooterRight = footerText;

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
