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

            // Input Visio file, custom theme file and output PDF paths
            string inputVisioPath = "input.vsdx";
            string themeVisioPath = "customTheme.vsdx";
            string outputPdfPath = "themed_output.pdf";

            // Load the original diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Load the diagram that contains the custom theme
            Diagram themeDiagram = new Diagram(themeVisioPath);

            // Apply the custom theme to the entire diagram (all pages)
            diagram.CopyTheme(themeDiagram);

            // Export the themed diagram to PDF
            diagram.Save(outputPdfPath, SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
