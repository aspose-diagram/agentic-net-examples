using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process: use first argument if provided, otherwise current directory
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Get all .vdx files in the folder (non‑recursive)
            string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx", SearchOption.TopDirectoryOnly);

            if (vdxFiles.Length == 0)
            {
                Console.WriteLine("No VDX files found in the specified folder.");
                return;
            }

            foreach (string vdxFile in vdxFiles)
            {
                try
                {
                    // Load the Visio diagram using the VDX format
                    using (Diagram diagram = new Diagram(vdxFile, LoadFileFormat.Vdx))
                    {
                        // Set each page's print orientation to Portrait
                        foreach (Page page in diagram.Pages)
                        {
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }

                        // Prepare PDF save options
                        PdfSaveOptions pdfOptions = new PdfSaveOptions
                        {
                            // Ensure the format is explicitly set (required when Aspose.Pdf is also referenced)
                            SaveFormat = SaveFileFormat.Pdf,
                            // Optional: specify a default font to avoid missing‑font issues
                            DefaultFont = "Arial"
                        };

                        // Determine output PDF file path (same name, .pdf extension)
                        string pdfFile = Path.ChangeExtension(vdxFile, ".pdf");

                        // Save the diagram as PDF using the options
                        diagram.Save(pdfFile, pdfOptions);

                        Console.WriteLine($"Converted '{Path.GetFileName(vdxFile)}' to PDF successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vdxFile}': {ex.Message}");
                }
            }
        }
    }