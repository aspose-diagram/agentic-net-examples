using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Define the folder containing VDX files.
            // You can change this path or pass it as a command‑line argument.
            string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";
            string outputFolder = Path.Combine(inputFolder, "PdfOutput");

            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all VDX files in the input folder.
            string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

            foreach (string vdxPath in vdxFiles)
            {
                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(vdxPath);

                    // Configure PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Use a fallback font in case the diagram references missing fonts.
                        DefaultFont = "Arial",
                        // Export hidden pages as well.
                        ExportHiddenPage = true,
                        // Set PDF/A compliance (optional).
                        Compliance = PdfCompliance.PdfA1b
                    };

                    // Build the output PDF file path.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vdxPath);
                    string pdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                    // Save the diagram as PDF with the specified options.
                    diagram.Save(pdfPath, pdfOptions);

                    Console.WriteLine($"Successfully converted: {vdxPath} -> {pdfPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch conversion completed.");
        }
    }