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
        string outputPdfPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists (create if necessary).
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and set orientation to Landscape.
                foreach (Page page in diagram.Pages)
                {
                    // Set the print orientation to Landscape.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Verify that each page now has width greater than height (Landscape).
                bool allLandscape = true;
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    Console.WriteLine($"Page ID {page.ID}: Width = {width} inches, Height = {height} inches");

                    if (width <= height)
                    {
                        allLandscape = false;
                        Console.Error.WriteLine($"Page ID {page.ID} is not landscape after orientation change.");
                    }
                }

                if (!allLandscape)
                {
                    Console.Error.WriteLine("One or more pages are not in landscape orientation. Aborting PDF export.");
                    return;
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracking flag.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF.
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"PDF successfully saved to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}