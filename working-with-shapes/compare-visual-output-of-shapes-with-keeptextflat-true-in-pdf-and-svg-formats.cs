using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input VSDX, output PDF, output SVG
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <output.pdf> <output.svg>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string pdfPath = args[1];
        string svgPath = args[2];

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to enable KeepTextFlat
            foreach (Page page in diagram.Pages)
            {
                // Iterate each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a ThreeDFormat (always present) and set KeepTextFlat to true
                    shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;
                }
            }

            // ---------- Export to PDF ----------
            // Create PDF save options (default font fallback can be set if needed)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Save the diagram as PDF using the options
            diagram.Save(pdfPath, pdfOptions);

            // ---------- Export to SVG ----------
            // Create SVG save options (no special settings required)
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Save the diagram as SVG using the options
            diagram.Save(svgPath, svgOptions);
        }
        catch (Exception ex)
        {
            // Log any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
            return;
        }

        // ---------- Compare the resulting files ----------
        try
        {
            // Verify both output files exist before comparison
            if (!File.Exists(pdfPath) || !File.Exists(svgPath))
            {
                Console.Error.WriteLine("One or both output files were not created.");
                return;
            }

            // Retrieve file sizes for a simple visual output comparison
            long pdfSize = new FileInfo(pdfPath).Length;
            long svgSize = new FileInfo(svgPath).Length;

            // Output the sizes and a basic assessment
            Console.WriteLine($"PDF size: {pdfSize} bytes");
            Console.WriteLine($"SVG size: {svgSize} bytes");

            if (pdfSize == svgSize)
            {
                Console.WriteLine("Both files have identical sizes – visual output may be similar.");
            }
            else
            {
                Console.WriteLine("File sizes differ – visual output may differ (e.g., text flattening).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}