using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Verify that input and output folder arguments are provided
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchVdxToPdf <inputFolder> <outputFolder>");
            return;
        }

        // Assign input and output folder paths
        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Guard: ensure output folder exists (create if missing)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Retrieve all VDX files in the input folder
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx");

        // Iterate over each VDX file
        foreach (string filePath in vdxFiles)
        {
            // Guard: ensure the VDX file still exists
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Processing file: {filePath}");

            try
            {
                // Load the Visio diagram from the VDX file
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate over each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the page orientation to Portrait
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }

                    // Prepare PDF save options (optional: set default font)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Build the output PDF file path (same name, .pdf extension)
                    string outputPdfPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".pdf");

                    // Save the diagram as PDF using the specified options
                    diagram.Save(outputPdfPath, pdfOptions);
                }

                Console.WriteLine($"Successfully saved PDF: {Path.GetFileName(outputFolder)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}