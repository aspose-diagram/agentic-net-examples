using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VdxToPdfBatchConverter
{
    static void Main()
    {
        try
        {

            // Folder containing VDX files
            string inputFolder = @"C:\Visio\VDXFiles";
            // Folder where PDF files will be saved
            string outputFolder = @"C:\Visio\PDFOutput";

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Get all VDX files in the input folder
            string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

            foreach (string vdxPath in vdxFiles)
            {
                // Load the VDX diagram using the provided constructor
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    // Configure PDF save options (customize as needed)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Example customizations
                        EnlargePage = true,                     // Enlarge page to fit drawing content
                        ExportHiddenPage = false,               // Do not export hidden pages
                        ExportGuideShapes = false,              // Do not export guide shapes
                        IsExportComments = true,                // Export comments if present
                        PageCount = int.MaxValue,               // Export all pages
                        DefaultFont = "Arial"                   // Fallback font for Unicode characters
                    };

                    // Build output PDF file path
                    string pdfFileName = Path.GetFileNameWithoutExtension(vdxPath) + ".pdf";
                    string pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save the diagram as PDF using the provided Save method and PdfSaveOptions
                    diagram.Save(pdfPath, pdfOptions);
                }
            }

            Console.WriteLine("Batch conversion completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
