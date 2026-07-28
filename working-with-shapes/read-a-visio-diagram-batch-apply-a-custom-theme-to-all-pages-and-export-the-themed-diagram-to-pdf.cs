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

            // Paths to the source theme diagram, the diagram to be themed, and the output PDF.
            string themeDiagramPath = "customTheme.vsdx";
            string inputDiagramPath = "inputDiagram.vsdx";
            string outputPdfPath = "themedDiagram.pdf";

            // Load the diagram that contains the custom theme.
            using (Diagram themeDiagram = new Diagram(themeDiagramPath))
            // Load the diagram that will receive the theme.
            using (Diagram targetDiagram = new Diagram(inputDiagramPath))
            {
                // Apply the custom theme from the source diagram to the target diagram.
                targetDiagram.CopyTheme(themeDiagram);

                // Save the themed diagram as a PDF file.
                targetDiagram.Save(outputPdfPath, SaveFileFormat.Pdf);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
