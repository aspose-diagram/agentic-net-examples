using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing .vsdx files
            string folderPath;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path to the folder containing .vsdx files: ");
                folderPath = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("The specified folder does not exist.");
                    return;
                }
            }

            // Get all .vsdx files in the folder
            string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
            if (vsdxFiles.Length == 0)
            {
                Console.WriteLine("No .vsdx files found in the specified folder.");
                return;
            }

            foreach (string filePath in vsdxFiles)
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

                        // Add a full‑page text shape as watermark
                        // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                        page.AddText(0, 0, pageWidth, pageHeight,
                                     "CONFIDENTIAL", "Arial", "#808080", 0.5);
                    }

                    // Save the modified diagram with a new name to avoid overwriting the original
                    string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_watermarked.vsdx");

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    diagram.Dispose();

                    Console.WriteLine($"Watermark added and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }