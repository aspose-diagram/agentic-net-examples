using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define input and output directories
                string inputFolder = @"C:\Visio\Input";
                string outputFolder = @"C:\Visio\Output";

                // Ensure the output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all VDX files in the input folder
                string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

                foreach (string vdxPath in vdxFiles)
                {
                    // Load the Visio diagram
                    using (Diagram diagram = new Diagram(vdxPath, LoadFileFormat.Vdx))
                    {
                        // Configure PDF save options
                        PdfSaveOptions pdfOptions = new PdfSaveOptions
                        {
                            // Set a default font to avoid missing font issues
                            DefaultFont = "Arial",
                            // Example custom options
                            EnlargePage = true,
                            ExportHiddenPage = false,
                            ExportGuideShapes = false
                        };

                        // Build the output PDF file path
                        string pdfFileName = Path.GetFileNameWithoutExtension(vdxPath) + ".pdf";
                        string pdfPath = Path.Combine(outputFolder, pdfFileName);

                        // Save the diagram as PDF using the custom options
                        diagram.Save(pdfPath, pdfOptions);
                    }

                    Console.WriteLine($"Converted: {Path.GetFileName(vdxPath)} -> PDF");
                }

                Console.WriteLine("Batch conversion completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }