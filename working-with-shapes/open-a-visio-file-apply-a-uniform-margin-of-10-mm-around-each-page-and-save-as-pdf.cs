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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Convert 10 mm to inches (1 inch = 25.4 mm).
                double marginInInches = 10.0 / 25.4; // ≈0.3937 inches

                // Apply the same margin to all four sides of every page.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginInInches;
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker as required.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as a PDF file.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}