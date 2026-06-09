using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Folder that contains the Visio files
            string diagramsFolder = @"C:\Visio\Diagrams";

            // Get all Visio files (adjust the extension if needed)
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");

            foreach (string filePath in diagramFiles)
            {
                // Load the diagram (lifecycle rule)
                Diagram diagram = new Diagram(filePath);

                // Set the right‑hand footer placeholder for the whole document.
                // Visio uses field codes like &[Page] for the current page number.
                // Aspose.Diagram exposes the HeaderFooter object on the Diagram.
                diagram.HeaderFooter.FooterRight = "Page &[Page]";

                // Save the diagram back to the same file (lifecycle rule)
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                // Release resources
                diagram.Dispose();
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
