using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file (VSDX, VDX, etc.)
            string inputPath = "input.vsdx";

            // Desired path for the exported HTML file
            string outputPath = "output.html";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Set up HTML export options.
            // By default Aspose.Diagram embeds shape graphics as inline SVG.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // If the library version supports it, you can ensure images are embedded:
            // htmlOptions.IsExportEmbeddedImages = true; // Uncomment if the property exists

            // Export the diagram to HTML with inline SVG markup
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
