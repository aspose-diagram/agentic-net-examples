using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files. Change as needed or pass as first argument.
        string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";

        // Ensure the folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Process each Visio file in the folder.
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Only handle supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
            {
                Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
                continue;
            }

            try
            {
                // Load the diagram from a file stream.
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    Diagram diagram = new Diagram(stream);

                    // Assume we add the watermark to the first page.
                    Page page = diagram.Pages[0];

                    // Retrieve page dimensions.
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Watermark text and appearance.
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    string fontColor = "#A5A5A5"; // Light gray in hex.
                    double fontSizePoints = 72; // 72 points = 1 inch.
                    double fontSizeInches = fontSizePoints / 72.0;

                    // Add the watermark as a full‑page text shape.
                    // Width and height are set to the full page size so the text can be centered.
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 watermarkText, fontName, fontColor, fontSizeInches);
                    
                    // Prepare output path (original name with suffix).
                    string directory = Path.GetDirectoryName(filePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_watermarked.vsdx");

                    // Save the modified diagram as VSDX.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Successfully added watermark to: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add watermark to: {Path.GetFileName(filePath)}. Error: {ex.Message}");
            }
        }
    }
}
