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

            // Folder containing source VDX files (first argument or default)
            string inputFolder = args.Length > 0 ? args[0] : "InputVdx";

            // Folder where PDF files will be written (second argument or default)
            string outputFolder = args.Length > 1 ? args[1] : "OutputPdf";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Retrieve all VDX files from the input directory
            string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

            foreach (string vdxPath in vdxFiles)
            {
                // Load the Visio diagram from the VDX file
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();

                    // Set a fallback font to guarantee characters are rendered if the original font is missing
                    pdfOptions.DefaultFont = "Arial";

                    // Build the output PDF file path
                    string pdfPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(vdxPath) + ".pdf");

                    // Save the diagram as PDF using the specified options (fonts are embedded by default)
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
