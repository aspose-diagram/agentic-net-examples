using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output directories
            string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "PdfOutput");

            // Create output directory if it does not exist
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Configure font folder (adjust path as needed for the environment)
            // This ensures that Aspose.Diagram can locate system fonts for embedding.
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            FontConfigs.DefaultFontName = "Arial";

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Process each VDX file in the input folder
            string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);
            foreach (string vdxPath in vdxFiles)
            {
                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(vdxPath, LoadFileFormat.Vdx);

                    // Build output PDF file path
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vdxPath);
                    string pdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                    // Save as PDF with the configured options
                    diagram.Save(pdfPath, pdfOptions);

                    Console.WriteLine($"Converted: {vdxPath} -> {pdfPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{vdxPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch conversion completed.");
        }
    }