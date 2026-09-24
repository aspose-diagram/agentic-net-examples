using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath> [pageIndex]");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path.
        string outputPath = args[1];
        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Optional page index (default 0).
        int pageIndex = 0;
        if (args.Length >= 3 && !int.TryParse(args[2], out pageIndex))
        {
            Console.Error.WriteLine("Invalid page index argument.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Validate page index range.
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the target page.
            Page page = diagram.Pages[pageIndex];

            bool anyMismatch = false;

            // Iterate all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes without a master (e.g., group shapes) – they cannot inherit line formatting.
                if (shape.Master == null)
                {
                    continue;
                }

                // Access the shape's own line formatting.
                string lineColor = shape.Line.LineColor.Value;
                double lineWeight = shape.Line.LineWeight.Value;
                LinePatternValue linePattern = shape.Line.LinePattern.Value;

                // Access the inherited line formatting from the master.
                string inheritColor = shape.InheritLine.LineColor.Value;
                double inheritWeight = shape.InheritLine.LineWeight.Value;
                LinePatternValue inheritPattern = shape.InheritLine.LinePattern.Value;

                // Compare each property; if any differ, report a mismatch.
                if (!string.Equals(lineColor, inheritColor, StringComparison.OrdinalIgnoreCase) ||
                    Math.Abs(lineWeight - inheritWeight) > 0.0001 ||
                    linePattern != inheritPattern)
                {
                    anyMismatch = true;
                    Console.Error.WriteLine($"Shape ID {shape.ID} does not inherit line formatting from its master.");
                    Console.Error.WriteLine($"  Own   - Color: {lineColor}, Weight: {lineWeight}, Pattern: {linePattern}");
                    Console.Error.WriteLine($"  Inh.  - Color: {inheritColor}, Weight: {inheritWeight}, Pattern: {inheritPattern}");
                }
            }

            // If mismatches were found, optionally abort export.
            if (anyMismatch)
            {
                Console.Error.WriteLine("Validation failed: some shapes do not inherit line formatting.");
                // Uncomment the following line to stop the program before exporting.
                // return;
            }
            else
            {
                Console.WriteLine("All shapes inherit line formatting from their masters.");
            }

            // Prepare PDF save options (set a default font to avoid missing font warnings).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Export the diagram to PDF.
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}