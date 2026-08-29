using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

                        // Add a text shape that covers the whole page.
                        // Parameters: pinX, pinY, width, height, text, font name, font color (hex), font size (in inches)
                        Shape watermark = page.AddText(
                            centerX,               // PinX (center X)
                            centerY,               // PinY (center Y)
                            pageWidth,             // Width of the text box
                            pageHeight,            // Height of the text box
                            "CONFIDENTIAL",        // Watermark text
                            "Arial",               // Font name
                            "#C0C0C0",             // Light gray color
                            0.5                    // Font size (0.5 inches ≈ 36 points)
                        );

                        // Rotate the text 45 degrees. TextXForm.TxtAngle expects radians.
                        watermark.TextXForm.TxtAngle.Value = (Math.PI / 180.0) * 45.0;
                    }

                    // Save the modified diagram to a new file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }