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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output PDF file
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Convert all shape colors to grayscale to reduce file size
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process fill foreground color
                    if (!string.IsNullOrEmpty(shape.Fill.FillForegnd.Value))
                    {
                        shape.Fill.FillForegnd.Value = ToGrayscaleHex(shape.Fill.FillForegnd.Value);
                    }

                    // Process fill background color
                    if (!string.IsNullOrEmpty(shape.Fill.FillBkgnd.Value))
                    {
                        shape.Fill.FillBkgnd.Value = ToGrayscaleHex(shape.Fill.FillBkgnd.Value);
                    }

                    // Process line color
                    if (!string.IsNullOrEmpty(shape.Line.LineColor.Value))
                    {
                        shape.Line.LineColor.Value = ToGrayscaleHex(shape.Line.LineColor.Value);
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Exclude hidden pages to keep the file size minimal
            pdfOptions.ExportHiddenPage = false;
            // Use default font fallback to avoid missing font issues
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Converts a hex color string (e.g., "#FFAA33") to its grayscale equivalent.
    private static string ToGrayscaleHex(string hexColor)
    {
        // Ensure the string starts with '#'
        if (!hexColor.StartsWith("#") || hexColor.Length != 7)
            return hexColor; // Return original if format is unexpected

        // Parse RGB components
        int r = Convert.ToInt32(hexColor.Substring(1, 2), 16);
        int g = Convert.ToInt32(hexColor.Substring(3, 2), 16);
        int b = Convert.ToInt32(hexColor.Substring(5, 2), 16);

        // Compute average for grayscale
        int gray = (r + g + b) / 3;

        // Clamp to 0-255 just in case
        gray = Math.Max(0, Math.Min(255, gray));

        // Return new hex string
        return $"#{gray:X2}{gray:X2}{gray:X2}";
    }
}
