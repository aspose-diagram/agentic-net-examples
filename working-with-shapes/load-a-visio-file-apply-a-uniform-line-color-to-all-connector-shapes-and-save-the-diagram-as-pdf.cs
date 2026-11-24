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

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the uniform line color (hex RGB). Change as needed.
                const string uniformLineColor = "#FF0000"; // Red

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    // Shapes collection is zero‑based; iterate safely.
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Identify connector shapes: they are 1‑D shapes.
                        if (shape.OneD)
                        {
                            // Apply the uniform line color.
                            shape.Line.LineColor.Value = uniformLineColor;
                        }
                    }
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optional: set a fallback font.
                pdfOptions.DefaultFont = "Arial";

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}