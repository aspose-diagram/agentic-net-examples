using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input folder and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Get all VDX files in the input folder.
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx");
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified input folder.");
            return;
        }

        // Process each VDX file.
        foreach (string vdxPath in vdxFiles)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found: {vdxPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block.
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    // Configure custom PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Use Arial as fallback font.
                        DefaultFont = "Arial",
                        // Do not export hidden pages. (BOOL replaced with bool)
                        ExportHiddenPage = false,
                        // Do not split multi‑page diagrams into separate PDFs. (BOOL replaced with bool)
                        SplitMultiPages = false
                    };

                    // Build the output PDF file path.
                    string pdfFileName = Path.GetFileNameWithoutExtension(vdxPath) + ".pdf";
                    string pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save the diagram as PDF using the configured options.
                    diagram.Save(pdfPath, pdfOptions);

                    Console.WriteLine($"Successfully converted '{vdxPath}' to PDF: '{pdfPath}'");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
            }
        }
    }
}
