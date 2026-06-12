using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the watermark text to look for (case‑insensitive)
                string oldWatermarkText = "Watermark";

                // Iterate through all pages and shapes to remove existing watermarks
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs to delete (cannot modify collection while iterating)
                    var shapesToDelete = new System.Collections.Generic.List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Check shape name or text for the watermark indicator
                        bool isWatermark = false;

                        // Check the universal name
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf(oldWatermarkText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            isWatermark = true;
                        }

                        // Check the plain text content
                        string shapeText = shape.Text.Value.ToString();
                        if (!isWatermark && !string.IsNullOrWhiteSpace(shapeText) &&
                            shapeText.IndexOf(oldWatermarkText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            isWatermark = true;
                        }

                        if (isWatermark)
                        {
                            // Mark the shape as deleted
                            shape.Del = BOOL.True;
                            // Optionally store the ID if further processing is needed
                            shapesToDelete.Add(shape.ID);
                        }
                    }

                    // (Optional) If you need to physically remove shapes from the collection,
                    // you could recreate the collection without the deleted IDs.
                    // Aspose.Diagram does not provide a direct Remove method, so marking as deleted is sufficient.
                }

                // Add a new watermark to each page
                string newWatermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColorHex = "#a5a5a5"; // Light gray
                double fontSizeInInches = 0.25; // Approx. 18 points (18/72)

                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Position the watermark at the center of the page
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the watermark text covering the full page area
                    page.AddText(pinX, pinY, pageWidth, pageHeight, newWatermarkText, fontName, fontColorHex, fontSizeInInches);
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }