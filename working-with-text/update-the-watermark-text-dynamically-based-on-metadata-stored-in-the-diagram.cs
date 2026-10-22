using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve watermark text from diagram metadata.
            // Here we use the document title as an example metadata field.
            // If the title is empty, fall back to a default watermark.
            string watermarkText = diagram.DocumentProps.Title;
            if (string.IsNullOrWhiteSpace(watermarkText))
            {
                watermarkText = "Default Watermark";
            }

            // Iterate through all pages and add a full‑page watermark text shape.
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page has a valid PageSheet (required for dimensions)
                if (page.PageSheet?.PageProps == null)
                {
                    Console.Error.WriteLine($"Page {page.Name} does not contain valid PageSheet data.");
                    continue;
                }

                // Get page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add the watermark text shape.
                // Font: Calibri, Color: light gray (#a5a5a5), Size: 0.25 (relative)
                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, "Calibri", "#a5a5a5", 0.25);
            }

            // Save the updated diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}