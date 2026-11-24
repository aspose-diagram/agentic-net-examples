using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output file path for the diagram with watermarks
        string outputPath = "output_with_watermark.vsdx";

        // Load the source diagram inside a using block (required pattern)
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Add watermark text to every page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Position at (0,0) covering the full page
                    double pinX = 0;
                    double pinY = 0;

                    // Watermark properties
                    string watermarkText = "Watermark";
                    string fontName = "Calibri";
                    string fontColor = "#a5a5a5"; // light gray
                    double fontSize = 0.25; // size in inches

                    // Add the watermark as a full‑page text shape
                    page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSize);
                }

                // Save the diagram with watermarks (must specify format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during watermark addition or saving: {ex.Message}");
            return;
        }

        // Validate that the watermark exists on every page after saving
        try
        {
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                bool allPagesValid = true;

                foreach (Page page in savedDiagram.Pages)
                {
                    bool watermarkFound = false;

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has text and compare plain text
                        if (shape.Text != null && shape.Text.Value != null && shape.Text.Value.Text == "Watermark")
                        {
                            watermarkFound = true;
                            break;
                        }
                    }

                    if (!watermarkFound)
                    {
                        Console.Error.WriteLine($"Watermark not found on page ID {page.ID}.");
                        allPagesValid = false;
                    }
                }

                if (allPagesValid)
                {
                    Console.WriteLine("Validation succeeded: Watermark appears on every page.");
                }
                else
                {
                    throw new Exception("Validation failed: One or more pages are missing the watermark.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}