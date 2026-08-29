using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = @"C:\VisioFiles";
            // Output folder for processed files
            string outputFolder = Path.Combine(inputFolder, "output");
            Directory.CreateDirectory(outputFolder);

            // Get all Visio files (VSDX, VSD, VDX, VSSX, etc.)
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                // Process only supported Visio extensions
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" &&
                    extension != ".vssx" && extension != ".vstx" && extension != ".vsdm")
                {
                    Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Add watermark to each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Center position for the watermark
                        double pinX = pageWidth / 2.0;
                        double pinY = pageHeight / 2.0;

                        // Add the watermark text (full‑page size)
                        page.AddText(
                            pinX,                     // pinX
                            pinY,                     // pinY
                            pageWidth,                // width
                            pageHeight,               // height
                            "CONFIDENTIAL",           // text
                            "Arial",                  // font name
                            "#CCCCCC",                // font color (hex)
                            0.5);                     // font size in inches (≈36 pt)
                    }

                    // Save the modified diagram next to the original with a suffix
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_watermarked{extension}");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"SUCCESS: Watermark added to '{Path.GetFileName(filePath)}' and saved as '{Path.GetFileName(outputPath)}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"FAILURE: Could not process '{Path.GetFileName(filePath)}'. Error: {ex.Message}");
                }
            }

            Console.WriteLine("Processing completed.");
        }
    }