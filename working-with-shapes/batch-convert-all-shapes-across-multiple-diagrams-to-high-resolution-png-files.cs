using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeBatchConverter
{
    static void Main()
    {
        try
        {

            // Folder containing Visio files (VSD, VDX, VSDX, etc.)
            string inputFolder = @"C:\VisioFiles";
            // Folder where PNG images will be saved
            string outputFolder = @"C:\ShapeImages";

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Define high‑resolution image options (300 DPI)
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.Resolution = 300; // high‑resolution DPI

            // Process each Visio file in the input folder
            foreach (string diagramPath in Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly))
            {
                // Load the diagram using the provided constructor
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Build a unique file name: DiagramName_PageIndex_ShapeID.png
                            string diagramName = Path.GetFileNameWithoutExtension(diagramPath);
                            string fileName = $"{diagramName}_Page{page.ID}_Shape{shape.ID}.png";
                            string outputPath = Path.Combine(outputFolder, fileName);

                            // Export the shape to PNG using the provided ToImage method
                            shape.ToImage(outputPath, imgOptions);
                        }
                    }
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
