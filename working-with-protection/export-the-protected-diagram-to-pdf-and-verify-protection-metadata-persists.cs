using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio diagram (protected) and the PDF output.
            string diagramPath = "protected_diagram.vsdx";
            string pdfPath = "protected_diagram.pdf";

            // Load the protected Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Read global document protection settings.
            BOOL protectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
            BOOL protectMasters = diagram.DocumentSettings.ProtectMasters;
            BOOL protectShapes = diagram.DocumentSettings.ProtectShapes;
            BOOL protectStyles = diagram.DocumentSettings.ProtectStyles;

            // Output the protection metadata to the console.
            Console.WriteLine($"ProtectBkgnds : {protectBkgnds}");
            Console.WriteLine($"ProtectMasters: {protectMasters}");
            Console.WriteLine($"ProtectShapes : {protectShapes}");
            Console.WriteLine($"ProtectStyles : {protectStyles}");

            // Verify that at least one protection flag is set (example validation).
            if (protectBkgnds == BOOL.False &&
                protectMasters == BOOL.False &&
                protectShapes == BOOL.False &&
                protectStyles == BOOL.False)
            {
                throw new Exception("No protection metadata found in the diagram.");
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Optional: set a default font to avoid missing font warnings.
            pdfOptions.DefaultFont = "Arial";

            // Export the diagram to PDF.
            diagram.Save(pdfPath, pdfOptions);

            // Confirm that the PDF file was created.
            if (!System.IO.File.Exists(pdfPath))
            {
                throw new Exception("PDF export failed; output file not found.");
            }

            Console.WriteLine("Diagram exported to PDF successfully. Protection metadata persisted.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
