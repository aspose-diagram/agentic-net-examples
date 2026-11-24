using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file, output file, watermark text
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <watermark text>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string newWatermarkText = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------------------------------------------------------------
                // Remove existing watermarks (shapes containing the word "Watermark")
                // ---------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes to remove to avoid modifying the collection during iteration
                    List<Shape> shapesToRemove = new List<Shape>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the concatenated plain text of the shape (if any)
                        string shapeText = shape.Text?.Value?.ToString();

                        if (!string.IsNullOrEmpty(shapeText) &&
                            shapeText.IndexOf("Watermark", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            shapesToRemove.Add(shape);
                        }
                    }

                    // Remove the identified shapes from the page
                    foreach (Shape s in shapesToRemove)
                    {
                        page.Shapes.Remove(s);
                    }
                }

                // ---------------------------------------------------------------
                // Add a new watermark shape to the first page (if any pages exist)
                // ---------------------------------------------------------------
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];

                    // Retrieve page dimensions (in inches)
                    double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

                    // Add a full‑page text shape as the watermark
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                    Shape watermarkShape = firstPage.AddText(
                        0,                     // pinX (left)
                        0,                     // pinY (top)
                        pageWidth,             // width
                        pageHeight,            // height
                        newWatermarkText,      // watermark text
                        "Calibri",             // font name
                        "#a5a5a5",             // font color (light gray)
                        0.25);                 // font size (in inches)

                    // Optionally make the watermark outline thin
                    watermarkShape.Line.LineWeight.Value = 0.1;
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}