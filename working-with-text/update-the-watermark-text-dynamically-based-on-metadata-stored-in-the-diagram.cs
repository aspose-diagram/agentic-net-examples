using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve watermark text from diagram metadata.
            // Here we use the document title as an example; adjust as needed.
            string watermarkText = diagram.DocumentProps.Title?.ToString() ?? "Default Watermark";

            // Iterate through all pages and add a full‑page watermark.
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page has a valid PageSheet.
                if (page.PageSheet == null || page.PageSheet.PageProps == null)
                {
                    Console.Error.WriteLine($"Page {page.Name} is missing PageSheet or PageProps. Skipping.");
                    continue;
                }

                // Get page dimensions.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the watermark at the lower‑left corner (0,0) and span the whole page.
                double pinX = 0;
                double pinY = 0;
                double width = pageWidth;
                double height = pageHeight;

                // Add the watermark text shape.
                // Font name, color (hex), and size are chosen per documentation example.
                page.AddText(pinX, pinY, width, height, watermarkText, "Calibri", "#a5a5a5", 0.25);
            }

            // Save the updated diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}