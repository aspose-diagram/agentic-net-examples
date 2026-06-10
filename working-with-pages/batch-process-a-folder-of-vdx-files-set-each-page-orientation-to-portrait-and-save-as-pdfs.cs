using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Folder containing VDX files – modify as needed
        string inputFolder = @"C:\VisioFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all VDX files in the folder
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx");

        foreach (string vdxPath in vdxFiles)
        {
            try
            {
                // Load the Visio diagram
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    // Set each page's print orientation to Portrait
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Determine output PDF file name
                    string pdfPath = Path.ChangeExtension(vdxPath, ".pdf");

                    // Save the diagram as PDF
                    diagram.Save(pdfPath, pdfOptions);

                    Console.WriteLine($"Converted: {Path.GetFileName(vdxPath)} → {Path.GetFileName(pdfPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{vdxPath}': {ex.Message}");
            }
        }
    }
}
