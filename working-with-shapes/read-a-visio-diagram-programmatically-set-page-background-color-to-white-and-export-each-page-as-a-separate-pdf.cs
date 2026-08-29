using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments are provided: input Visio file and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputDirectory>");
            return;
        }

        // Guard the input file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard the output directory (create it if it does not exist).
        string outputDir = args[1];
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the current page.
                Page page = diagram.Pages[i];

                // Obtain page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center point for a rectangle that covers the whole page.
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Draw a rectangle shape that spans the entire page.
                long rectId = page.DrawRectangle(centerX, centerY, pageWidth, pageHeight);

                // Retrieve the shape object using the returned ID.
                Shape backgroundShape = page.Shapes.GetShape((int)rectId);

                // Set a solid fill pattern.
                backgroundShape.Fill.FillPattern.Value = 1; // 1 = solid fill

                // Apply white color to the fill.
                backgroundShape.Fill.FillForegnd.Value = "#FFFFFF";

                // Remove any border by setting line pattern to none.
                backgroundShape.Line.LinePattern.Value = 0;

                // Send the background shape to the back so other content appears on top.
                backgroundShape.SendToBack();

                // Lock the shape to prevent accidental selection/editing.
                backgroundShape.Protection.LockSelect.Value = BOOL.True;

                // Configure PDF save options to export only the current page.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = i,          // Zero‑based index of the page to export
                    PageCount = 1,          // Export a single page
                    DefaultFont = "Arial", // Fallback font for missing glyphs
                    SaveFormat = SaveFileFormat.Pdf
                };

                // Build the output PDF file name (e.g., Page_1.pdf).
                string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.pdf");

                // Export the page as a PDF file.
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}