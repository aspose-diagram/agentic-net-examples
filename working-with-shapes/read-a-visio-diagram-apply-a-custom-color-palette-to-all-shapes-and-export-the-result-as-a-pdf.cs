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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define a custom color palette (hex strings)
            string[] palette = new string[]
            {
                "#FF5733", // reddish
                "#33FF57", // greenish
                "#3357FF", // bluish
                "#F1C40F", // yellow
                "#9B59B6", // purple
                "#E67E22"  // orange
            };

            int colorIndex = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply fill color from the palette
                    string fillColor = palette[colorIndex % palette.Length];
                    shape.Fill.FillForegnd.Value = fillColor;
                    shape.Fill.FillPattern.Value = 1; // solid fill

                    // Optionally set line color and weight
                    shape.Line.LineColor.Value = "#000000"; // black border
                    shape.Line.LineWeight.Value = 0.02; // thickness in inches

                    colorIndex++;
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Export the diagram as PDF
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram exported to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
