using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output diagram path.
        string outputPath = "output.vsdx";

        // New watermark text.
        string newWatermarkText = "Confidential";

        try
        {
            // Load the diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to remove existing watermark shapes.
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of shapes that appear to be watermarks.
                    List<long> idsToRemove = new List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        bool isWatermark = false;

                        // Check shape name for the word "Watermark".
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Watermark", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            isWatermark = true;
                        }

                        // Check shape text for the word "Watermark".
                        if (!isWatermark && shape.Text != null && shape.Text.Value != null)
                        {
                            string plainText = shape.Text.Value.ToString();
                            if (!string.IsNullOrEmpty(plainText) &&
                                plainText.IndexOf("Watermark", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                isWatermark = true;
                            }
                        }

                        if (isWatermark)
                        {
                            idsToRemove.Add(shape.ID);
                        }
                    }

                    // Remove identified watermark shapes.
                    foreach (long id in idsToRemove)
                    {
                        Shape shapeToRemove = page.Shapes.GetShape(id);
                        if (shapeToRemove != null)
                        {
                            page.Shapes.Remove(shapeToRemove);
                        }
                    }

                    // Add a new watermark that covers the whole page.
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Font size in inches (e.g., 0.25 inches ≈ 18 points).
                    double fontSizeInches = 0.25;

                    // Add the watermark text shape.
                    page.AddText(pinX, pinY, pageWidth, pageHeight, newWatermarkText, "Calibri", "#a5a5a5", fontSizeInches);
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
