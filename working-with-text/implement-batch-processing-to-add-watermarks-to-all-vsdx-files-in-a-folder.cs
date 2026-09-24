using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing .vsdx files – change as needed or pass as first argument
            string folderPath = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Get all Visio files with .vsdx extension
            string[] files = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in files)
            {
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

                        // Watermark text and appearance
                        string watermarkText = "CONFIDENTIAL";
                        string fontName = "Arial";
                        string fontColorHex = "#CCCCCC"; // Light gray
                        double fontSizeInInches = 0.5;   // Approx. 36 points

                        // Add a full‑page text shape as watermark
                        page.AddText(pinX, pinY, pageWidth, pageHeight,
                                     watermarkText, fontName, fontColorHex, fontSizeInInches);
                    }

                    // Save the modified diagram (overwrites original file)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Watermark added to: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }