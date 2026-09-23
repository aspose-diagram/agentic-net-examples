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

            // Paths of the diagrams to be merged.
            string[] diagramPaths = { "Diagram1.vsdx", "Diagram2.vsdx", "Diagram3.vsdx" };

            // Load the first diagram – it will become the target diagram.
            Diagram mergedDiagram = new Diagram(diagramPaths[0]);

            // Load each subsequent diagram and combine it into the target.
            for (int i = 1; i < diagramPaths.Length; i++)
            {
                Diagram sourceDiagram = new Diagram(diagramPaths[i]);
                mergedDiagram.Combine(sourceDiagram);
            }

            // Optional: save the merged diagram for later use.
            mergedDiagram.Save("MergedDiagram.vsdx", SaveFileFormat.Vsdx);

            // Create image save options for PNG thumbnail.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.PageIndex = 0;      // Export only the first page.
            pngOptions.PageCount = 1;      // Ensure a single page is rendered.
            pngOptions.Scale = 0.5f;       // Scale down to create a thumbnail (adjust as needed).

            // Save the thumbnail of the first page as PNG.
            mergedDiagram.Save("Thumbnail.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
