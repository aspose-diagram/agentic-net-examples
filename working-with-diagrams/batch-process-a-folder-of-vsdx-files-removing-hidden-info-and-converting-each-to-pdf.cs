using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchVsdxToPdf
{
    static void Main(string[] args)
    {
        // Folder containing VSDX files – change as needed
        string inputFolder = @"C:\VisioFiles";
        // Destination folder for PDFs – change as needed
        string outputFolder = @"C:\VisioPdf";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each .vsdx file in the input folder
        foreach (string vsdxPath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            // Load the Visio diagram from file
            using (Diagram diagram = new Diagram(vsdxPath))
            {
                // Remove hidden information (personal info, shapes, masters, styles, data record sets)
                int hiddenInfoFlags =
                    (int)RemoveHiddenInfoItem.PersonalInfo |
                    (int)RemoveHiddenInfoItem.Shapes |
                    (int)RemoveHiddenInfoItem.Masters |
                    (int)RemoveHiddenInfoItem.Styles |
                    (int)RemoveHiddenInfoItem.DataRecordSets;

                diagram.RemoveHiddenInformation(hiddenInfoFlags);

                // Remove any VBA macros
                diagram.RemoveMacro();

                // Prepare PDF save options – do not export hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    ExportHiddenPage = false
                };

                // Build output PDF file path
                string pdfFileName = Path.GetFileNameWithoutExtension(vsdxPath) + ".pdf";
                string pdfPath = Path.Combine(outputFolder, pdfFileName);

                // Save the diagram as PDF using the specified options
                diagram.Save(pdfPath, pdfOptions);
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
