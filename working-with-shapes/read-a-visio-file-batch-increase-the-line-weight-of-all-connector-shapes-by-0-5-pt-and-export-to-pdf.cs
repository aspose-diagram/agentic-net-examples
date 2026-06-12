using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Increment line weight of all connector shapes by 0.5 pt (0.5/72 inches)
            double increment = 0.5 / 72.0;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connector shapes are 1‑D shapes
                    if (shape.OneD)
                    {
                        double currentWeight = shape.Line.LineWeight.Value;
                        shape.Line.LineWeight.Value = currentWeight + increment;
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format tracking

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
