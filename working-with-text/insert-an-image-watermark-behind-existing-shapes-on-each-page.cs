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
                string inputVisioPath = "input.vsdx";
                string watermarkImagePath = "watermark.png";
                string outputVisioPath = "output_with_watermark.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputVisioPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Center position for the watermark shape
                        double centerX = pageWidth / 2.0;
                        double centerY = pageHeight / 2.0;

                        // Add the image as a shape covering the whole page
                        using (FileStream imageStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                        {
                            long shapeId = page.AddShape(
                                centerX,               // PinX (center X)
                                centerY,               // PinY (center Y)
                                pageWidth,             // Width (full page width)
                                pageHeight,            // Height (full page height)
                                imageStream);          // Image stream

                            // Retrieve the newly added shape
                            Shape watermarkShape = page.Shapes.GetShape(shapeId);

                            // Send the watermark to the back so it appears behind other shapes
                            watermarkShape.SendToBack();

                            // Make the watermark non‑selectable
                            watermarkShape.Protection.LockSelect.Value = BOOL.True;
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Watermark added and diagram saved to: " + outputVisioPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }