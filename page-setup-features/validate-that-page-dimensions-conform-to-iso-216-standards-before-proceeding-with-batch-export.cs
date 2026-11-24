using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // ISO 216 A series paper sizes in millimeters (width x height)
    private static readonly (double Width, double Height)[] IsoASeries = new (double, double)[]
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

    // Tolerance for size comparison in millimeters
    private const double ToleranceMm = 1.0;

    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard for input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Guard for output folder existence; create if missing
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Get all Visio files (VSDX) in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.vsdx");
        if (files.Length == 0)
        {
            Console.WriteLine("No Visio files found to process.");
            return;
        }

        foreach (string filePath in files)
        {
            // Guard for file existence (redundant but safe)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Validate each page's dimensions against ISO 216
                bool allPagesValid = true;
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Retrieve width and height in inches
                    double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Convert to millimeters
                    double widthMm = widthInches * 25.4;
                    double heightMm = heightInches * 25.4;

                    if (!IsIso216Size(widthMm, heightMm))
                    {
                        Console.Error.WriteLine($"File '{Path.GetFileName(filePath)}', Page {i + 1} dimensions ({widthMm:F1}mm x {heightMm:F1}mm) do not match any ISO 216 A series size.");
                        allPagesValid = false;
                        break; // Stop checking further pages for this file
                    }
                }

                if (!allPagesValid)
                {
                    Console.Error.WriteLine($"Skipping export for '{Path.GetFileName(filePath)}' due to invalid page size.");
                    continue;
                }

                // All pages are valid – proceed with export (PDF in this example)
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format
                pdfOptions.ExportHiddenPage = false; // Example setting; adjust as needed

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Exported '{Path.GetFileName(filePath)}' to '{outputFileName}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }

    // Checks whether the given dimensions (in mm) match any ISO 216 A series size within tolerance.
    private static bool IsIso216Size(double widthMm, double heightMm)
    {
        foreach (var size in IsoASeries)
        {
            // Direct orientation match
            if (Math.Abs(widthMm - size.Width) <= ToleranceMm && Math.Abs(heightMm - size.Height) <= ToleranceMm)
                return true;

            // Swapped orientation match (landscape vs portrait)
            if (Math.Abs(widthMm - size.Height) <= ToleranceMm && Math.Abs(heightMm - size.Width) <= ToleranceMm)
                return true;
        }
        return false;
    }
}