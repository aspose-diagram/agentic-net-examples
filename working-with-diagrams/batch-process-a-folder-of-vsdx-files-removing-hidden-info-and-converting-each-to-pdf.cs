using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Define the folder containing VSDX files.
            // You can change this path or pass it as a command‑line argument.
            string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            // Verify the folder exists.
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Folder not found: {inputFolder}");
                return;
            }

            // Get all VSDX files in the folder.
            string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
            if (vsdxFiles.Length == 0)
            {
                Console.WriteLine("No VSDX files found in the specified folder.");
                return;
            }

            // Process each file.
            foreach (string filePath in vsdxFiles)
            {
                try
                {
                    Console.WriteLine($"Processing: {Path.GetFileName(filePath)}");

                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx);

                    // Remove hidden information (shapes and masters in this example).
                    diagram.RemoveHiddenInformation(
                        (int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                    // Prepare PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Fallback font if the original font is missing.
                        DefaultFont = "Arial"
                    };

                    // Build the output PDF path (same name, .pdf extension, placed in the same folder).
                    string outputPdfPath = Path.Combine(
                        Path.GetDirectoryName(filePath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(filePath) + ".pdf");

                    // Save the cleaned diagram as PDF.
                    diagram.Save(outputPdfPath, pdfOptions);

                    Console.WriteLine($"Saved PDF: {Path.GetFileName(outputPdfPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }