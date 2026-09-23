using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing VDX files. Change as needed or pass as first argument.
            string folderPath = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Get all VDX files in the folder.
            string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx", SearchOption.TopDirectoryOnly);
            if (vdxFiles.Length == 0)
            {
                Console.WriteLine("No VDX files found.");
                return;
            }

            foreach (string vdxPath in vdxFiles)
            {
                try
                {
                    // Load the Visio diagram.
                    using (Diagram diagram = new Diagram(vdxPath))
                    {
                        // Set each page's orientation to Portrait.
                        foreach (Page page in diagram.Pages)
                        {
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }

                        // Prepare PDF save options.
                        PdfSaveOptions pdfOptions = new PdfSaveOptions
                        {
                            DefaultFont = "Arial",
                            SaveFormat = SaveFileFormat.Pdf
                        };

                        // Determine output PDF path.
                        string pdfPath = Path.ChangeExtension(vdxPath, ".pdf");

                        // Save the diagram as PDF.
                        diagram.Save(pdfPath, pdfOptions);
                        Console.WriteLine($"Converted: {Path.GetFileName(vdxPath)} -> {Path.GetFileName(pdfPath)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }