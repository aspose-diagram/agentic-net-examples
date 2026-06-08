using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchVsdxToPdf
{
    static void Main(string[] args)
    {
        // Folder containing VSDX files – change as needed
        string sourceFolder = @"C:\VisioFiles";
        // Destination folder for PDFs – change as needed
        string outputFolder = @"C:\VisioPdf";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each VSDX file in the source folder
        foreach (string vsdxPath in Directory.GetFiles(sourceFolder, "*.vsdx"))
        {
            // Load the diagram using the constructor that accepts a file path
            using (Diagram diagram = new Diagram(vsdxPath))
            {
                // Remove hidden information (personal info, shapes, masters, styles, data record sets)
                int removeMask = (int)(
                    RemoveHiddenInfoItem.PersonalInfo |
                    RemoveHiddenInfoItem.Shapes |
                    RemoveHiddenInfoItem.Masters |
                    RemoveHiddenInfoItem.Styles |
                    RemoveHiddenInfoItem.DataRecordSets);

                diagram.RemoveHiddenInformation(removeMask);

                // Remove any VBA/macros that may be present
                diagram.RemoveMacro();

                // Prepare PDF save options – do not export hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    ExportHiddenPage = false
                };

                // Build output PDF file path (same name, .pdf extension)
                string pdfFileName = Path.GetFileNameWithoutExtension(vsdxPath) + ".pdf";
                string pdfPath = Path.Combine(outputFolder, pdfFileName);

                // Save the diagram as PDF using the Save method with SaveOptions
                diagram.Save(pdfPath, pdfOptions);
            }
        }
    }
}
