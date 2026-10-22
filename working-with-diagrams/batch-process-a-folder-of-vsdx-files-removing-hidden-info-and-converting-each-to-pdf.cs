using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing VSDX files (argument or user input).
        string folderPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing VSDX files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Retrieve all VSDX files in the folder.
        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx");
        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the specified folder.");
            return;
        }

        // Process each VSDX file.
        foreach (string inputPath in vsdxFiles)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Determine the output PDF path (same folder, same name with .pdf extension).
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Remove hidden shapes and masters from the diagram.
                    diagram.RemoveHiddenInformation(
                        (int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                    // Configure PDF save options (optional: set default font).
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Save the cleaned diagram as PDF.
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine($"Successfully converted: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any errors that occur during processing to the error stream.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}