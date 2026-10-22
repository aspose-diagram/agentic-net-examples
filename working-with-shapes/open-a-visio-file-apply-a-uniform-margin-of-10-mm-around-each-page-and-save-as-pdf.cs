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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Convert 10 mm to inches (1 inch = 25.4 mm).
                double marginInInches = 10.0 / 25.4;

                // Apply the same margin to all four sides of every page.
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginInInches;
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the output format.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Enable text compression to reduce PDF size.
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Successfully saved PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}