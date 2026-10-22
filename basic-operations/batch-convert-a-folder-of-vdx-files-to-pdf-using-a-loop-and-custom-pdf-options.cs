using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolderPath> <outputFolderPath>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Get all .vdx files in the input folder (non‑recursive)
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found to convert.");
            return;
        }

        // Process each VDX file
        foreach (string vdxPath in vdxFiles)
        {
            // Guard: file must exist (redundant but required by rules)
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found: {vdxPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(vdxPath);

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Fallback font if the original font is missing
                    DefaultFont = "Arial",
                    // Do not export hidden pages (bool, not BOOL)
                    ExportHiddenPage = false,
                    // Do not split multi‑page diagrams into separate PDFs (bool, not BOOL)
                    SplitMultiPages = false
                };

                // Determine output PDF file path
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vdxPath);
                string pdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                // Save the diagram as PDF using the configured options
                diagram.Save(pdfPath, pdfOptions);

                Console.WriteLine($"Successfully converted: {vdxPath} -> {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
            }
        }
    }
}
