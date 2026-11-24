using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        string outputDir = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists or create it.
        try
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory '{outputDir}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes to enable KeepTextFlat.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Enable KeepTextFlat for 3‑D rendering.
                        shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;
                    }
                }

                // -------------------- Export to PDF --------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Use a common font to avoid missing‑font warnings.
                    DefaultFont = "Arial"
                };

                string pdfPath = Path.Combine(outputDir,
                    Path.GetFileNameWithoutExtension(inputPath) + "_KeepTextFlat.pdf");

                diagram.Save(pdfPath, pdfOptions);
                Console.WriteLine($"PDF exported to: {pdfPath}");

                // -------------------- Export to SVG --------------------
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Export the first page only (adjust if needed).
                    PageIndex = 0
                    // PageCount is optional; omitted to avoid version‑specific issues.
                };

                string svgPath = Path.Combine(outputDir,
                    Path.GetFileNameWithoutExtension(inputPath) + "_KeepTextFlat.svg");

                diagram.Save(svgPath, svgOptions);
                Console.WriteLine($"SVG exported to: {svgPath}");

                // -------------------- Simple visual comparison --------------------
                FileInfo pdfInfo = new FileInfo(pdfPath);
                FileInfo svgInfo = new FileInfo(svgPath);
                Console.WriteLine($"PDF size: {pdfInfo.Length} bytes");
                Console.WriteLine($"SVG size: {svgInfo.Length} bytes");
                Console.WriteLine("Export completed. Manually inspect the PDF and SVG files to verify that text appears flat.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}