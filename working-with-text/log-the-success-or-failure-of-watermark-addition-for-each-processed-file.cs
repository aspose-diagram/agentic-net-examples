using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least one file path is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("No input files specified.");
            return;
        }

        // Process each file path supplied via command line arguments
        foreach (string inputPath in args)
        {
            // Guard: verify the file exists before attempting to load
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram to add a watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (width and height)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Watermark configuration
                    double pinX = 0; // Top‑left X coordinate
                    double pinY = 0; // Top‑left Y coordinate
                    string watermarkText = "Watermark";
                    string fontName = "Calibri";
                    string fontColor = "#a5a5a5";
                    double fontSize = 0.25;

                    // Add the watermark text covering the full page area
                    page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSize);
                }

                // Construct output file path (original name with suffix)
                string directory = Path.GetDirectoryName(inputPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_watermarked.vsdx");

                // Save the modified diagram using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Log successful watermark addition
                Console.WriteLine($"Successfully added watermark to '{inputPath}'. Saved as '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Log any errors that occurred during processing
                Console.Error.WriteLine($"Failed to add watermark to '{inputPath}'. Error: {ex.Message}");
            }
        }
    }
}