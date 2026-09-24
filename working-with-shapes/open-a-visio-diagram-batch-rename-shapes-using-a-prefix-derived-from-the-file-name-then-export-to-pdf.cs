using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. If a path is supplied as an argument, use it; otherwise use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Process all Visio files with the .vsdx extension in the folder.
            string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            if (visioFiles.Length == 0)
            {
                Console.WriteLine("No Visio (.vsdx) files found in the specified folder.");
                return;
            }

            foreach (string visioPath in visioFiles)
            {
                try
                {
                    // Derive a prefix from the file name (without extension) and add an underscore.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(visioPath);
                    string prefix = fileNameWithoutExt + "_";

                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(visioPath);

                    // Iterate through each page and each shape on the page.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Rename the shape using the derived prefix.
                            // Both NameU (universal name) and Name (local name) are updated.
                            shape.NameU = prefix + shape.NameU;
                            shape.Name = prefix + shape.Name;
                        }
                    }

                    // Prepare PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Set a default font to avoid missing font warnings.
                        DefaultFont = "Arial",
                        // Explicitly indicate the output format.
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Define the output PDF file path.
                    string pdfPath = Path.Combine(folderPath, fileNameWithoutExt + ".pdf");

                    // Save the diagram as PDF.
                    diagram.Save(pdfPath, pdfOptions);

                    Console.WriteLine($"Processed '{visioPath}' and saved PDF to '{pdfPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
                }
            }
        }
    }