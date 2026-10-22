using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // ISO 216 A‑series paper sizes in millimetres (width × height, portrait)
    static readonly List<(double WidthMm, double HeightMm)> IsoASeries = new List<(double, double)>
    {
        (841, 1189), // A0
        (594, 841),  // A1
        (420, 594),  // A2
        (297, 420),  // A3
        (210, 297),  // A4
        (148, 210),  // A5
        (105, 148),  // A6
        (74, 105),   // A7
        (52, 74),    // A8
        (37, 52),    // A9
        (26, 37)     // A10
    };

    // Allowed tolerance when comparing dimensions (mm)
    const double ToleranceMm = 1.0;

    static void Main(string[] args)
    {
        // -----------------------------------------------------------------
        // Validate command‑line arguments
        // -----------------------------------------------------------------
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // -----------------------------------------------------------------
        // Guard input folder existence
        // -----------------------------------------------------------------
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // -----------------------------------------------------------------
        // Ensure output folder exists (create if necessary)
        // -----------------------------------------------------------------
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Process each Visio file (*.vsdx) in the input folder
        // -----------------------------------------------------------------
        string[] files = Directory.GetFiles(inputFolder, "*.vsdx");
        foreach (string filePath in files)
        {
            // Guard file existence (redundant but required by rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Validate all pages against ISO 216 sizes
                if (!ValidateDiagramPages(diagram))
                {
                    Console.Error.WriteLine($"File '{Path.GetFileName(filePath)}' contains pages with non‑ISO dimensions. Skipping export.");
                    continue;
                }

                // Export each page to a separate PDF file
                ExportDiagramPagesToPdf(diagram, filePath, outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // -----------------------------------------------------------------
    // Validates that every page in the diagram matches an ISO A‑series size
    // -----------------------------------------------------------------
    static bool ValidateDiagramPages(Diagram diagram)
    {
        foreach (Page page in diagram.Pages)
        {
            // Retrieve page dimensions (inches) from the PageSheet
            double widthInches = page.PageSheet.PageProps.PageWidth.Value;
            double heightInches = page.PageSheet.PageProps.PageHeight.Value;

            // Convert inches to millimetres (1 inch = 25.4 mm)
            double widthMm = widthInches * 25.4;
            double heightMm = heightInches * 25.4;

            // Normalise to portrait orientation for comparison
            double w = Math.Min(widthMm, heightMm);
            double h = Math.Max(widthMm, heightMm);

            bool matches = false;
            foreach (var (isoW, isoH) in IsoASeries)
            {
                if (Math.Abs(w - isoW) <= ToleranceMm && Math.Abs(h - isoH) <= ToleranceMm)
                {
                    matches = true;
                    break;
                }
            }

            if (!matches)
            {
                Console.Error.WriteLine($"Page {page.ID} dimensions ({widthMm:F1} mm × {heightMm:F1} mm) do not match any ISO A‑series size.");
                return false;
            }
        }

        return true;
    }

    // -----------------------------------------------------------------
    // Exports each page of the diagram to a separate PDF file
    // -----------------------------------------------------------------
    static void ExportDiagramPagesToPdf(Diagram diagram, string sourceFilePath, string outputFolder)
    {
        string baseName = Path.GetFileNameWithoutExtension(sourceFilePath);

        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            try
            {
                // Configure PDF save options for a single page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    SaveFormat = SaveFileFormat.Pdf,   // explicit format tracking
                    PageIndex = i,                     // zero‑based page index
                    PageCount = 1,                     // export only this page
                    ExportHiddenPage = false           // do not export hidden pages (bool, not BOOL)
                };

                // Build output file path
                string outputPath = Path.Combine(outputFolder, $"{baseName}_Page{i + 1}.pdf");

                // Save the selected page as PDF
                diagram.Save(outputPath, pdfOptions);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to export page {i + 1} of '{sourceFilePath}': {ex.Message}");
            }
        }
    }
}