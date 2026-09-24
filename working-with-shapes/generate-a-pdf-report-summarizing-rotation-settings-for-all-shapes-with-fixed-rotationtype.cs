using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output PDF file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure the directory for the PDF exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a new PDF document (fully qualified to avoid namespace clash).
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            // Add a page to the PDF.
            Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

            // Prepare a simple line spacing counter.
            double cursorY = 0;
            const double lineHeight = 12; // points

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check for Fixed RotationType (RotationType cell value 0 = Fixed).
                    // The RotationType cell is accessed via shape.ThreeDFormat.RotationType.
                    // If the cell is undefined, treat it as not Fixed.
                    bool isFixed = false;
                    if (shape.ThreeDFormat != null && shape.ThreeDFormat.RotationType != null)
                    {
                        // RotationTypeValue.Undefined indicates no explicit setting.
                        isFixed = shape.ThreeDFormat.RotationType.Value == RotationTypeValue.Undefined;
                    }

                    // Only report shapes with Fixed rotation.
                    if (isFixed)
                    {
                        // Retrieve the shape's rotation angle (in degrees) from XForm.Angle.
                        double angleDeg = shape.XForm.Angle.Value;

                        // Build a summary line.
                        string line = $"Page: {page.NameU}, Shape ID: {shape.ID}, Name: {shape.NameU}, Angle: {angleDeg}°";

                        // Create a text fragment for the PDF.
                        Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(line);
                        tf.Position = new Aspose.Pdf.Text.Position(0, cursorY);
                        pdfPage.Paragraphs.Add(tf);

                        // Move cursor down for the next line.
                        cursorY += lineHeight;
                    }
                }
            }

            // Save the PDF document.
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF report generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}