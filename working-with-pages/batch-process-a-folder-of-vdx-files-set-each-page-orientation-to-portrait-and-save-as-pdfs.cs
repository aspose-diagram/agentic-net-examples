using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing VDX files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing VDX files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all .vdx files in the folder.
        string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx");
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified folder.");
            return;
        }

        // Process each VDX file.
        foreach (string vdxPath in vdxFiles)
        {
            // Guard: ensure the file exists (redundant due to GetFiles, but required by rules).
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
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // PrintPageOrientationValue.Portrait sets portrait orientation.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the save format (required by some versions).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Determine output PDF path (same name, .pdf extension).
                string pdfPath = Path.ChangeExtension(vdxPath, ".pdf");

                // Save the diagram as PDF using the configured options.
                diagram.Save(pdfPath, pdfOptions);

                Console.WriteLine($"Successfully processed: {Path.GetFileName(vdxPath)} -> {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
            }
        }
    }
}