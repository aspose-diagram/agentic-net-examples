using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the existing Visio diagram
                string inputPath = "input.vsdx";
                // Path for the updated diagram
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs that are identified as watermarks
                    var watermarkShapeIds = new System.Collections.Generic.List<long>();

                    // Identify potential watermark shapes
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape's text contains the word "Watermark"
                        // or if the shape's universal name indicates a watermark.
                        string shapeText = shape.Text.Value.ToString();
                        string shapeNameU = shape.NameU ?? string.Empty;

                        if (shapeText.IndexOf("Watermark", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            shapeNameU.IndexOf("Watermark", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Mark this shape for deletion
                            watermarkShapeIds.Add(shape.ID);
                        }
                    }

                    // Delete identified watermark shapes by setting the Del flag
                    foreach (long shapeId in watermarkShapeIds)
                    {
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);
                        if (watermarkShape != null)
                        {
                            watermarkShape.Del = BOOL.True;
                        }
                    }

                    // Add a new watermark text covering the full page
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add the watermark text (font size is in inches; 0.25 inches ≈ 18 points)
                    page.AddText(
                        centerX,               // PinX (center X)
                        centerY,               // PinY (center Y)
                        pageWidth,             // Width of the text box (full page width)
                        pageHeight,            // Height of the text box (full page height)
                        "New Watermark",       // Watermark text
                        "Calibri",             // Font name
                        "#a5a5a5",             // Font color (hex)
                        0.25);                 // Font size in inches
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }