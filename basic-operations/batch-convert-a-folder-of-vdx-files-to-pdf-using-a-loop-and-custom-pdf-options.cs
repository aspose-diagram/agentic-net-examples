using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VdxToPdfBatch
{
    static void Main(string[] args)
    {
        try
        {

            // Input folder containing VDX files (default if not provided)
            string inputFolder = args.Length > 0 ? args[0] : @"C:\InputVdx";
            // Output folder for generated PDFs (default if not provided)
            string outputFolder = args.Length > 1 ? args[1] : @"C:\OutputPdf";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Retrieve all VDX files in the input directory
            string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

            foreach (string vdxPath in vdxFiles)
            {
                // Load the diagram using the provided constructor
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    // Configure custom PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        EnlargePage = true,               // Enlarge page to fit drawing content
                        ExportHiddenPage = false,         // Do not export hidden pages
                        ExportGuideShapes = false,        // Do not export guide shapes
                        DefaultFont = "Arial"             // Fallback font for Unicode characters
                    };

                    // Build the output PDF file path
                    string pdfFileName = Path.GetFileNameWithoutExtension(vdxPath) + ".pdf";
                    string pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save the diagram as PDF using the provided Save method with SaveOptions
                    diagram.Save(pdfPath, pdfOptions);
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
