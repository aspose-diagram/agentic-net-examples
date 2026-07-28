using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VdxToPdfBatchConverter
{
    static void Main()
    {
        // Folder containing the VDX files
        string inputFolder = @"C:\VisioFiles";

        // Get all VDX files in the folder
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);

        foreach (string vdxPath in vdxFiles)
        {
            // Load the Visio diagram from the VDX file
            using (Diagram diagram = new Diagram(vdxPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Specify a default font to ensure characters are rendered correctly
                    // when the original font is not available on the system.
                    DefaultFont = "Arial",

                    // Optional: embed all fonts (Aspose.Diagram embeds fonts by default;
                    // setting this flag ensures the behavior is explicit if supported).
                    // EnlargePage = true // uncomment if page size adjustment is needed
                };

                // Determine the output PDF file path
                string pdfPath = Path.ChangeExtension(vdxPath, ".pdf");

                // Save the diagram as PDF using the configured options
                diagram.Save(pdfPath, pdfOptions);
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
