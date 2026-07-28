using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing Visio files to process
        string inputFolder = @"C:\VisioFiles";
        // Folder to save processed files
        string outputFolder = @"C:\VisioFiles\Processed";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all Visio files (VSDX, VSD, VDX, etc.) in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Process only supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsx" && ext != ".vtx")
            {
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Add watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Watermark parameters
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    string fontColor = "#FF0000"; // Red color in hex
                    double fontSizeInPoints = 36; // 36 pt
                    double fontSizeInInches = fontSizeInPoints / 72.0; // Convert points to inches

                    // Add the watermark covering the full page
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 watermarkText, fontName, fontColor, fontSizeInInches);
                }

                // Save the modified diagram
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"SUCCESS: Watermark added to '{filePath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILURE: Could not add watermark to '{filePath}'. Error: {ex.Message}");
            }
        }
    }
}
