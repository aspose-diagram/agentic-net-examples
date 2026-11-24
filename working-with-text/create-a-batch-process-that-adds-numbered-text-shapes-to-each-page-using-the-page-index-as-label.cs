using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard: input folder must exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Guard: ensure output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Collect Visio files (VSDX and VSD) from the input folder.
        string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd");
        string[] allFiles = new string[vsdxFiles.Length + vsdFiles.Length];
        vsdxFiles.CopyTo(allFiles, 0);
        vsdFiles.CopyTo(allFiles, vsdxFiles.Length);

        // Process each file individually.
        foreach (string filePath in allFiles)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block (mandatory pattern).
                using (Diagram diagram = new Diagram(filePath))
                {
                    int pageNumber = 1; // 1‑based page label.

                    // Iterate through all pages in the diagram.
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Define position and size for the new text shape (in inches).
                        double pinX = 1.0;   // Horizontal position.
                        double pinY = 1.0;   // Vertical position.
                        double width = 2.0;  // Shape width.
                        double height = 0.5; // Shape height.

                        // Use the page index as the label text.
                        string label = pageNumber.ToString();

                        // Insert the text shape onto the current page.
                        page.AddText(pinX, pinY, width, height, label);

                        pageNumber++;
                    }

                    // Determine the output file path.
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified diagram as VSDX.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}