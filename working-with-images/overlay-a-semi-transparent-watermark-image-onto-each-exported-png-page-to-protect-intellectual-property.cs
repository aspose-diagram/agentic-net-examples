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

            // Paths – adjust as needed
            string diagramPath = "input.vsdx";
            string watermarkPath = "watermark.png";
            string outputFolder = "output";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the watermark image into memory
            byte[] watermarkBytes = File.ReadAllBytes(watermarkPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Get page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Insert the watermark image covering the whole page
                using (MemoryStream imgStream = new MemoryStream(watermarkBytes))
                {
                    // AddShape returns the shape ID (long)
                    long shapeId = page.AddShape(0, 0, pageWidth, pageHeight, imgStream);
                    Shape watermarkShape = page.Shapes.GetShape(shapeId);

                    // Set semi‑transparent fill (50% opacity)
                    watermarkShape.Fill.FillForegndTrans.Value = 50; // percent

                    // Send the watermark to the back so other content appears on top
                    watermarkShape.SendToBack();

                    // Optional: make the watermark non‑selectable
                    watermarkShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Export the current page as a PNG file
                string outputPath = Path.Combine(outputFolder, $"Page_{page.ID}.png");
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = page.ID; // Export only this page
                diagram.Save(outputPath, options);
            }

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
