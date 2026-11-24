using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Define a simple custom color palette (hex strings).
        string[] palette = new string[]
        {
            "#FF5733", // reddish
            "#33FF57", // greenish
            "#3357FF", // bluish
            "#F1C40F", // yellow
            "#9B59B6", // purple
            "#E67E22", // orange
            "#1ABC9C", // teal
            "#ECF0F1"  // light gray
        };

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and shapes to apply the palette.
                int colorIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Assign a solid fill pattern.
                        shape.Fill.FillPattern.Value = 1; // 1 = solid

                        // Apply the next color from the palette (wrap around if needed).
                        string colorHex = palette[colorIndex % palette.Length];
                        shape.Fill.FillForegnd.Value = colorHex;

                        // Move to the next palette entry.
                        colorIndex++;
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format.
                pdfOptions.ExportHiddenPage = false;       // Do not export hidden pages.
                pdfOptions.HorizontalResolution = 300;    // Optional high‑resolution setting.
                pdfOptions.VerticalResolution = 300;
                pdfOptions.JpegQuality = 100;             // Preserve image quality.

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}