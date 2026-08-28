using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with actual path)
        string inputPath = "protected_diagram.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPdfPath = "protected_diagram.pdf";

        // Variables to hold original protection states
        BOOL protectBkgndsOrig = BOOL.False;
        BOOL protectMastersOrig = BOOL.False;
        BOOL protectShapesOrig = BOOL.False;
        BOOL protectStylesOrig = BOOL.False;

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve document protection metadata (BOOL values are returned directly, no .Value)
            protectBkgndsOrig = diagram.DocumentSettings.ProtectBkgnds;
            protectMastersOrig = diagram.DocumentSettings.ProtectMasters;
            protectShapesOrig = diagram.DocumentSettings.ProtectShapes;
            protectStylesOrig = diagram.DocumentSettings.ProtectStyles;

            // Log original protection settings
            Console.WriteLine("Original protection metadata:");
            Console.WriteLine($"  ProtectBkgnds : {protectBkgndsOrig}");
            Console.WriteLine($"  ProtectMasters: {protectMastersOrig}");
            Console.WriteLine($"  ProtectShapes : {protectShapesOrig}");
            Console.WriteLine($"  ProtectStyles : {protectStylesOrig}");

            // Configure PDF save options (set default font to avoid missing font warnings)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
            };

            // Export the diagram to PDF
            diagram.Save(outputPdfPath, pdfOptions);
            Console.WriteLine($"Diagram exported to PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
            return;
        }

        try
        {
            // Reload the diagram to verify that protection metadata persisted after export
            Diagram diagramAfter = new Diagram(inputPath);

            // Retrieve protection metadata again (direct BOOL values)
            BOOL protectBkgndsAfter = diagramAfter.DocumentSettings.ProtectBkgnds;
            BOOL protectMastersAfter = diagramAfter.DocumentSettings.ProtectMasters;
            BOOL protectShapesAfter = diagramAfter.DocumentSettings.ProtectShapes;
            BOOL protectStylesAfter = diagramAfter.DocumentSettings.ProtectStyles;

            // Compare before/after values and report any discrepancy
            bool allMatch = true;

            if (protectBkgndsAfter != protectBkgndsOrig)
            {
                Console.Error.WriteLine("Mismatch in ProtectBkgnds after export.");
                allMatch = false;
            }
            if (protectMastersAfter != protectMastersOrig)
            {
                Console.Error.WriteLine("Mismatch in ProtectMasters after export.");
                allMatch = false;
            }
            if (protectShapesAfter != protectShapesOrig)
            {
                Console.Error.WriteLine("Mismatch in ProtectShapes after export.");
                allMatch = false;
            }
            if (protectStylesAfter != protectStylesOrig)
            {
                Console.Error.WriteLine("Mismatch in ProtectStyles after export.");
                allMatch = false;
            }

            if (allMatch)
            {
                Console.WriteLine("Protection metadata verified: all values persisted correctly.");
            }
            else
            {
                Console.Error.WriteLine("Protection metadata verification failed.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during verification
            Console.Error.WriteLine($"Error during verification: {ex.Message}");
        }
    }
}