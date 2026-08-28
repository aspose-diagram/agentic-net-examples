using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportFirstPage
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = "diagram.vsdx";

            // Path for the exported PNG image
            string outputImage = "firstPage.png";

            // Load the Visio diagram from the file (uses Diagram(string) constructor)
            Diagram diagram = new Diagram(sourceFile);

            // Export the diagram (active page) as a high‑resolution PNG.
            // SaveFileFormat.Png selects PNG output format.
            diagram.Save(outputImage, SaveFileFormat.Png);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
