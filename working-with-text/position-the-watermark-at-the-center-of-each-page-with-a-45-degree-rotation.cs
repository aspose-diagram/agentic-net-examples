using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate the center point of the page
                        double centerX = pageWidth / 2.0;
                        double centerY = pageHeight / 2.0;

                        // Add a full‑page text shape that will serve as the watermark
                        // Font size is specified in inches (0.25 inch ≈ 18 pt)
                        Shape watermark = page.AddText(
                            centerX,               // PinX (center X)
                            centerY,               // PinY (center Y)
                            pageWidth,             // Width (full page)
                            pageHeight,            // Height (full page)
                            "Watermark",           // Text content
                            "Arial",               // Font name
                            "#A5A5A5",             // Font color (hex)
                            0.25);                 // Font size (in inches)

                        // Rotate the watermark 45 degrees (SetAngle expects radians)
                        watermark.SetAngle(Math.PI / 4);
                    }

                    // Save the modified diagram
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }