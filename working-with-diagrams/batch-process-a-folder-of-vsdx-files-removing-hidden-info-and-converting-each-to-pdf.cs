using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchVsdxToPdf
{
    static void Main(string[] args)
    {
        try
        {

            // Input folder containing VSDX files
            string inputFolder = @"C:\InputVsdx";
            // Output folder for generated PDFs
            string outputFolder = @"C:\OutputPdf";

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Get all VSDX files in the input folder
            string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string vsdxPath in vsdxFiles)
            {
                // Determine output PDF file path
                string pdfFileName = Path.GetFileNameWithoutExtension(vsdxPath) + ".pdf";
                string pdfPath = Path.Combine(outputFolder, pdfFileName);

                // Load the diagram using the constructor that accepts a file path
                using (Diagram diagram = new Diagram(vsdxPath))
                {
                    // Remove hidden information (personal info, shapes, masters, styles, data record sets)
                    int removeFlags = (int)(
                        RemoveHiddenInfoItem.PersonalInfo |
                        RemoveHiddenInfoItem.Shapes |
                        RemoveHiddenInfoItem.Masters |
                        RemoveHiddenInfoItem.Styles |
                        RemoveHiddenInfoItem.DataRecordSets);

                    diagram.RemoveHiddenInformation(removeFlags);

                    // Optionally remove macros/VBA if present
                    diagram.RemoveMacro();

                    // Configure PDF save options to exclude hidden pages
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        ExportHiddenPage = false
                    };

                    // Save the diagram as PDF using the Save method with SaveOptions
                    diagram.Save(pdfPath, pdfOptions);
                }

                Console.WriteLine($"Converted '{vsdxPath}' to PDF at '{pdfPath}'.");
            }

            Console.WriteLine("Batch processing completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
