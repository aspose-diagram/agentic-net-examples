using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (change as needed)
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Rotate each non-deleted shape by 45 degrees (π/4 radians)
                double rotationRadians = Math.PI / 4;
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Add the rotation to the existing angle
                        shape.XForm.Angle.Value += rotationRadians;
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram rotated and saved to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
