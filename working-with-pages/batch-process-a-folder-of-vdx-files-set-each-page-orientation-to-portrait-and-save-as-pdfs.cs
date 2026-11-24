using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input folder path from command‑line arguments or prompt the user.
        string folderPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing VDX files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("The specified folder does not exist.");
            return;
        }

        // Get all VDX files in the folder.
        string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx");
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified folder.");
            return;
        }

        // Process each VDX file.
        foreach (string vdxPath in vdxFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found: {vdxPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(vdxPath);

                // Set each page's orientation to Portrait.
                foreach (Page page in diagram.Pages)
                {
                    // PrintPageOrientation is part of the page's print properties.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the target format.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Build the output PDF file path (same name, .pdf extension).
                string pdfPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(vdxPath) + ".pdf");

                // Save the diagram as PDF using the configured options.
                diagram.Save(pdfPath, pdfOptions);

                Console.WriteLine($"Successfully converted '{vdxPath}' to PDF.");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
            }
        }
    }
}