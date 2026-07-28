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

            // Paths to the source Visio file and the resulting PDF.
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Desired line thickness for all connectors (in inches).
                double uniformLineWeight = 0.02; // ~0.5 mm

                // Iterate over every page and shape.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes: 1‑D shapes whose master is "Dynamic connector".
                        if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                        {
                            // Apply the uniform line thickness.
                            shape.Line.LineWeight.Value = uniformLineWeight;
                        }
                    }
                }

                // Set up PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export the diagram to PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Export completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
