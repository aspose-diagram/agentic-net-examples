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

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Path to the watermark image (PNG, JPEG, etc.)
            string watermarkImagePath = "watermark.png";

            // Load the diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate over all pages in the diagram
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the watermark shape
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add the watermark image as a shape covering the whole page
                    using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        long shapeId = page.AddShape(centerX, centerY, pageWidth, pageHeight, imgStream);
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Set semi‑transparent fill (0 = opaque, 100 = fully transparent)
                        watermarkShape.Fill.FillForegndTrans.Value = 50; // 50 % transparency

                        // Send the watermark to the back so it does not obscure other content
                        watermarkShape.SendToBack();
                    }

                    // Prepare PNG export options for the current page
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = pageIndex, // zero‑based page index
                        // Optional: adjust resolution if needed
                        // HorizontalResolution = 300,
                        // VerticalResolution = 300
                    };

                    // Export the page with the watermark applied
                    string outputFile = $"output_page_{pageIndex + 1}.png";
                    diagram.Save(outputFile, pngOptions);

                    pageIndex++;
                }
            }

            Console.WriteLine("Export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
