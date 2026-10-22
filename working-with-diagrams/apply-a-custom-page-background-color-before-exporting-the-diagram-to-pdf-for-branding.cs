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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [backgroundHexColor]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        // Default background color is red if not supplied.
        string backgroundColor = args.Length > 2 ? args[2] : "#FF0000";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages to apply the background color.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center point for the rectangle shape.
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add a rectangle that spans the whole page.
                    long rectShapeId = page.DrawRectangle(centerX, centerY, pageWidth, pageHeight);

                    // Retrieve the newly added shape.
                    Shape rectShape = page.Shapes.GetShape(rectShapeId);

                    // Set a solid fill pattern and the desired background color.
                    rectShape.Fill.FillPattern.Value = 1;               // 1 = solid fill
                    rectShape.Fill.FillForegnd.Value = backgroundColor; // Hex color string

                    // Remove the outline stroke.
                    rectShape.Line.LinePattern.Value = 0; // No line

                    // Send the rectangle to the back so other content appears above it.
                    rectShape.SendToBack();

                    // Make the background shape non‑selectable.
                    rectShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.DefaultFont = "Arial";

                // Export the diagram to PDF with the custom background.
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}