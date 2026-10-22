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

        // Define output path for the diagram with watermark
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_with_watermark.vsdx");

        // Load the source diagram, add watermark to each page, and save
        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Add watermark text to every page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add full‑page watermark text
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                    page.AddText(pinX, pinY, pageWidth, pageHeight, "Watermark", "Calibri", "#a5a5a5", 0.25);
                }

                // Save the diagram with watermark
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during watermark addition or saving: {ex.Message}");
            return;
        }

        // Validate that the watermark exists on every page after reloading
        try
        {
            // Reload the saved diagram inside a using block
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                // Iterate through each page to verify watermark presence
                foreach (Page page in savedDiagram.Pages)
                {
                    bool watermarkFound = false;

                    // Scan all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has text and compare its plain value
                        // shape.Text.Value.Text returns the concatenated plain text
                        if (shape.Text != null && shape.Text.Value.Text == "Watermark")
                        {
                            watermarkFound = true;
                            break;
                        }
                    }

                    if (!watermarkFound)
                    {
                        throw new Exception($"Watermark not found on page ID {page.ID}.");
                    }
                }

                Console.WriteLine("Watermark validation succeeded: present on all pages.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
    }
}