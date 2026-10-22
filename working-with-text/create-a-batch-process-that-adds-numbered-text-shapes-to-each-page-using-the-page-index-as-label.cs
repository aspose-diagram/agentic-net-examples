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

        // Ensure output folder exists (create if necessary).
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

        // Get all files in the input folder (process Visio files only).
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Guard: file must exist (redundant but required by rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block (mandatory pattern).
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page and add a numbered text shape.
                    int pageCounter = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        pageCounter++;

                        // Define position and size for the new text shape.
                        double pinX = 1.0;      // Horizontal position (in inches).
                        double pinY = 1.0;      // Vertical position (in inches).
                        double width = 2.0;     // Width of the text box.
                        double height = 0.5;    // Height of the text box.

                        // Text to display: use the page index as the label.
                        string label = $"Page {pageCounter}";

                        // Add the text shape to the current page.
                        page.AddText(pinX, pinY, width, height, label);
                    }

                    // Build the output file path (original name with suffix).
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_labeled.vsdx");

                    // Save the modified diagram as VSDX.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}