using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VdxToPdfBatchConverter
{
    // Converts all VDX files in the specified input folder to PDF files in the output folder.
    // Each PDF will embed fonts by specifying a default fallback font.
    public static void ConvertFolder(string inputFolder, string outputFolder)
    {
        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all VDX files in the input folder (non‑recursive)
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx");

        foreach (string vdxPath in vdxFiles)
        {
            // Load the Visio diagram from the VDX file
            Diagram diagram = new Diagram(vdxPath);

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set a default font to be used when the original font is missing.
                // This also forces embedding of the specified font if it is installed.
                DefaultFont = "Arial",

                // Use PDF 1.5 compliance (fonts are embedded by default).
                Compliance = PdfCompliance.Pdf15,

                // Render all pages
                PageCount = int.MaxValue,

                // Ensure hidden pages are also exported if needed
                ExportHiddenPage = true
            };

            // Build output PDF file path (same name, .pdf extension)
            string pdfFileName = Path.GetFileNameWithoutExtension(vdxPath) + ".pdf";
            string pdfPath = Path.Combine(outputFolder, pdfFileName);

            // Save the diagram as PDF using the specified options
            diagram.Save(pdfPath, pdfOptions);

            // Release resources held by the diagram
            diagram.Dispose();
        }
    }

    // Example usage
    static void Main(string[] args)
    {
        try
        {

            // Input folder containing VDX files
            string inputFolder = @"C:\Visio\VDXFiles";

            // Output folder for generated PDFs
            string outputFolder = @"C:\Visio\PDFOutputs";

            ConvertFolder(inputFolder, outputFolder);

            Console.WriteLine("Batch conversion completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
