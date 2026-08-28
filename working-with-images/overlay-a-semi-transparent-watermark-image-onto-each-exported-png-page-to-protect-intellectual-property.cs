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

                // Input Visio file and watermark image paths
                string diagramPath = "input.vsdx";
                string watermarkPath = "watermark.png";

                // Output folder for PNG pages
                string outputFolder = "output";
                Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Insert the watermark image covering the whole page
                        using (FileStream imgStream = new FileStream(watermarkPath, FileMode.Open, FileAccess.Read))
                        {
                            // AddShape returns the shape ID (long)
                            long watermarkShapeId = page.AddShape(0, 0, pageWidth, pageHeight, imgStream);

                            // Retrieve the shape object to modify its properties
                            Shape watermarkShape = page.Shapes.GetShape(watermarkShapeId);

                            // Set semi‑transparent fill (0 = opaque, 100 = fully transparent)
                            watermarkShape.Fill.FillForegndTrans.Value = 50; // 50% transparency

                            // Send the watermark to the back so other content appears above it
                            watermarkShape.SendToBack();

                            // Make the watermark non‑selectable
                            watermarkShape.Protection.LockSelect.Value = BOOL.True;
                        }

                        // Prepare PNG export options for the current page only
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = (int)page.ID - 1, // zero‑based page index
                            PageCount = 1
                        };

                        // Export the page as a PNG file
                        string outputPath = Path.Combine(outputFolder, $"Page_{page.ID}.png");
                        diagram.Save(outputPath, pngOptions);
                    }
                }

                Console.WriteLine("Watermarked PNG pages have been generated.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }