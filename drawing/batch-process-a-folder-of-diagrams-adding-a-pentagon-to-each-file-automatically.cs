using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchAddPentagon
{
    static void Main(string[] args)
    {
        // Folder containing Visio diagram files
        string folderPath = @"C:\Diagrams";

        // Process each .vsdx file in the folder
        foreach (string filePath in Directory.GetFiles(folderPath, "*.vsdx"))
        {
            // Load the diagram using the constructor that accepts a file name
            using (Diagram diagram = new Diagram(filePath))
            {
                // Get the first page (or ActivePage) to add the shape
                Page page = diagram.ActivePage ?? diagram.Pages[0];

                // Add a pentagon shape at a fixed position (2 inches, 2 inches)
                // Master name "Pentagon" is a built‑in Visio shape
                page.AddShape(2.0, 2.0, "Pentagon");

                // Save the diagram back, overwriting the original file
                // Use the same format as the source file (VSDX)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
        }
    }
}
