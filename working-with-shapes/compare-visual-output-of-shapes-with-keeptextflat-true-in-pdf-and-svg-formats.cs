using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists (create if missing)
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

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes to enable KeepTextFlat
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // ThreeDFormat is always present; set KeepTextFlat to true
                        if (shape.ThreeDFormat != null)
                        {
                            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;
                        }
                    }
                }

                // Configure PDF save options (export first page only)
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = 0,
                    PageCount = 1
                };

                // Configure SVG save options (export first page only)
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    PageIndex = 0
                    // PageCount property is omitted to avoid compatibility issues
                };

                // Define output file paths
                string pdfPath = Path.Combine(outputFolder, "output_keeptextflat.pdf");
                string svgPath = Path.Combine(outputFolder, "output_keeptextflat.svg");

                // Save diagram as PDF
                diagram.Save(pdfPath, pdfOptions);

                // Save diagram as SVG
                diagram.Save(svgPath, svgOptions);

                // Report file sizes as a simple visual comparison indicator
                long pdfSize = new FileInfo(pdfPath).Length;
                long svgSize = new FileInfo(svgPath).Length;

                Console.WriteLine($"PDF saved to: {pdfPath} (size: {pdfSize} bytes)");
                Console.WriteLine($"SVG saved to: {svgPath} (size: {svgSize} bytes)");

                if (pdfSize > 0 && svgSize > 0)
                {
                    Console.WriteLine("Both PDF and SVG files were generated successfully with KeepTextFlat enabled.");
                }
                else
                {
                    Console.WriteLine("One of the output files appears to be empty.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}