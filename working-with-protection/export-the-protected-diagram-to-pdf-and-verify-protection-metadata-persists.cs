using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the protected Visio diagram
        string inputPath = "protected_diagram.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the exported PDF
        string outputPath = "protected_diagram.pdf";

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Read protection settings before export (properties are of type BOOL)
                BOOL protectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
                BOOL protectMasters = diagram.DocumentSettings.ProtectMasters;
                BOOL protectShapes = diagram.DocumentSettings.ProtectShapes;
                BOOL protectStyles = diagram.DocumentSettings.ProtectStyles;

                Console.WriteLine("Protection settings before export:");
                Console.WriteLine($"  ProtectBkgnds: {protectBkgnds}");
                Console.WriteLine($"  ProtectMasters: {protectMasters}");
                Console.WriteLine($"  ProtectShapes: {protectShapes}");
                Console.WriteLine($"  ProtectStyles: {protectStyles}");

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    ExportHiddenPage = false,               // Do not export hidden pages
                    SaveFormat = SaveFileFormat.Pdf         // Explicitly set the format
                };

                // Export the diagram to PDF
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram exported to PDF: {outputPath}");

                // Re‑read protection settings after export to verify they persisted
                BOOL protectBkgndsAfter = diagram.DocumentSettings.ProtectBkgnds;
                BOOL protectMastersAfter = diagram.DocumentSettings.ProtectMasters;
                BOOL protectShapesAfter = diagram.DocumentSettings.ProtectShapes;
                BOOL protectStylesAfter = diagram.DocumentSettings.ProtectStyles;

                Console.WriteLine("Protection settings after export:");
                Console.WriteLine($"  ProtectBkgnds: {protectBkgndsAfter}");
                Console.WriteLine($"  ProtectMasters: {protectMastersAfter}");
                Console.WriteLine($"  ProtectShapes: {protectShapesAfter}");
                Console.WriteLine($"  ProtectStyles: {protectStylesAfter}");

                // Verify that protection flags have not changed
                if (protectBkgnds != protectBkgndsAfter ||
                    protectMasters != protectMastersAfter ||
                    protectShapes != protectShapesAfter ||
                    protectStyles != protectStylesAfter)
                {
                    throw new Exception("Protection metadata changed after PDF export.");
                }

                Console.WriteLine("Verification succeeded: protection metadata persisted.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
