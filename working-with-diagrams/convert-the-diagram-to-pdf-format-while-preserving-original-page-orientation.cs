using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToPdf
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram (any supported format)
            string sourcePath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the existing diagram (uses the provided load rule)
            Diagram diagram = new Diagram(sourcePath);

            // Save the diagram as PDF while preserving the original page orientation
            // (uses the provided save rule with PDF format)
            diagram.Save(outputPath, SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
