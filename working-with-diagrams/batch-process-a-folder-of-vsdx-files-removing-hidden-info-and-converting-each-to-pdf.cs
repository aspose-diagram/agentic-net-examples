using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: BatchConvertVsdx <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        // Guard: ensure the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Determine output folder (optional second argument).
        string outputFolder = args.Length >= 2 ? args[1] : inputFolder;
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Retrieve all VSDX files in the input directory.
        string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the specified folder.");
            return;
        }

        // Process each VSDX file.
        foreach (string vsdxPath in vsdxFiles)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(vsdxPath))
            {
                Console.Error.WriteLine($"File not found: {vsdxPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(vsdxPath))
                {
                    // Remove hidden shapes and masters from the diagram.
                    diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                    // Configure PDF save options (optional settings).
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure hidden pages are not exported (already removed, but set for safety).
                        ExportHiddenPage = false
                    };

                    // Build the output PDF file path.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vsdxPath);
                    string pdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                    // Save the cleaned diagram as PDF.
                    diagram.Save(pdfPath, pdfOptions);

                    Console.WriteLine($"Converted '{vsdxPath}' to PDF successfully.");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{vsdxPath}': {ex.Message}");
            }
        }
    }
}