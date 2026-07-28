using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramPages
{
    static void Main()
    {
        try
        {

            // Load the diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Loop through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Determine page complexity by counting shapes
                int shapeCount = diagram.Pages[i].Shapes.Count;

                // Choose JPEG quality based on complexity
                int jpegQuality;
                if (shapeCount > 100)
                    jpegQuality = 100;          // very complex – best quality
                else if (shapeCount > 50)
                    jpegQuality = 85;           // moderately complex
                else
                    jpegQuality = 70;           // simple page – lower quality for smaller size

                // Set up image save options for JPEG
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                saveOptions.JpegQuality = jpegQuality; // custom quality per page
                saveOptions.PageIndex = i;              // zero‑based page index
                saveOptions.PageCount = 1;              // render only this page

                // Save the current page as a JPEG file
                string outputFile = $"Page_{i + 1}.jpg";
                diagram.Save(outputFile, saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
