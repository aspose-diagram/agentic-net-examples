using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing the .vsdx files
            string inputFolder = @"C:\VisioFiles";
            // Folder to store watermarked files (can be the same as inputFolder)
            string outputFolder = @"C:\VisioFiles\Watermarked";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all .vsdx files in the input folder
            string[] files = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in files)
            {
                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Add watermark to each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Add a full‑page text watermark.
                        // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                        page.AddText(
                            0,                     // pinX (left edge)
                            0,                     // pinY (bottom edge)
                            pageWidth,             // width (full page width)
                            pageHeight,            // height (full page height)
                            "CONFIDENTIAL",        // watermark text
                            "Calibri",             // font name
                            "#a5a5a5",             // light gray color in hex
                            0.25);                 // font size (0.25 inches ≈ 18 pt)
                    }

                    // Build output file name
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_watermarked.vsdx");

                    // Save the modified diagram back to VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Watermarked file saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch watermarking completed.");
        }
    }