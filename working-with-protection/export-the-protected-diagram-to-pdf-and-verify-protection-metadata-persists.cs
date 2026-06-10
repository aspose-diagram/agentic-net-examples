using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file (protected diagram) and output PDF path
                string visioPath = "protected_diagram.vsdx";
                string pdfPath = "protected_diagram.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Capture protection metadata before export
                BOOL protectBkgndsBefore = diagram.DocumentSettings.ProtectBkgnds;
                BOOL protectMastersBefore = diagram.DocumentSettings.ProtectMasters;
                BOOL protectShapesBefore = diagram.DocumentSettings.ProtectShapes;
                BOOL protectStylesBefore = diagram.DocumentSettings.ProtectStyles;

                Console.WriteLine("Protection metadata before export:");
                Console.WriteLine($"ProtectBkgnds: {protectBkgndsBefore}");
                Console.WriteLine($"ProtectMasters: {protectMastersBefore}");
                Console.WriteLine($"ProtectShapes: {protectShapesBefore}");
                Console.WriteLine($"ProtectStyles: {protectStylesBefore}");

                // Configure PDF save options (optional encryption can be added here)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                // Example: enable encryption (user password: "user", owner password: "owner")
                // pdfOptions.EncryptionDetails = new PdfEncryptionDetails("user", "owner", PdfEncryptionAlgorithm.RC4_128);
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format

                // Export diagram to PDF
                diagram.Save(pdfPath, pdfOptions);

                // Verify that protection metadata still matches after export
                BOOL protectBkgndsAfter = diagram.DocumentSettings.ProtectBkgnds;
                BOOL protectMastersAfter = diagram.DocumentSettings.ProtectMasters;
                BOOL protectShapesAfter = diagram.DocumentSettings.ProtectShapes;
                BOOL protectStylesAfter = diagram.DocumentSettings.ProtectStyles;

                Console.WriteLine("\nProtection metadata after export:");
                Console.WriteLine($"ProtectBkgnds: {protectBkgndsAfter}");
                Console.WriteLine($"ProtectMasters: {protectMastersAfter}");
                Console.WriteLine($"ProtectShapes: {protectShapesAfter}");
                Console.WriteLine($"ProtectStyles: {protectStylesAfter}");

                // Simple validation – throw if any value changed
                if (protectBkgndsBefore != protectBkgndsAfter ||
                    protectMastersBefore != protectMastersAfter ||
                    protectShapesBefore != protectShapesAfter ||
                    protectStylesBefore != protectStylesAfter)
                {
                    throw new Exception("Protection metadata changed during PDF export.");
                }

                Console.WriteLine("\nExport completed successfully. Protection metadata persisted.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }