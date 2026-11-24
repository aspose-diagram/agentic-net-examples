using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all Visio files in the folder (VSDX and VSD formats).
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No diagram files found in the specified folder.");
            return;
        }

        Console.WriteLine("Header/Footer Report");
        Console.WriteLine("--------------------");

        foreach (string filePath in diagramFiles)
        {
            // Process only supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsdm" && ext != ".vsdm")
            {
                continue;
            }

            // Guard: ensure the file exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // ----- Batch modification of headers -----
                // Example modifications – adjust as needed.
                diagram.HeaderFooter.HeaderLeft = "Left Header";
                diagram.HeaderFooter.HeaderCenter = "Center Header";
                diagram.HeaderFooter.HeaderRight = "Right Header";

                // Optionally modify header/footer font (demonstrates BOOL usage).
                var headerFont = diagram.HeaderFooter.HeaderFooterFont;
                headerFont.FaceName = "Arial";
                headerFont.Height = 12;               // Font size in points.
                headerFont.Weight = 700;              // 700 = Bold.
                headerFont.Underline = BOOL.False;    // No underline.

                // Save the modified diagram with a new name to avoid overwriting the original.
                string directory = Path.GetDirectoryName(filePath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string modifiedPath = Path.Combine(directory, $"{fileNameWithoutExt}_modified.vsdx");
                diagram.Save(modifiedPath, SaveFileFormat.Vsdx);

                // Retrieve header texts after modification.
                string left = diagram.HeaderFooter.HeaderLeft ?? string.Empty;
                string center = diagram.HeaderFooter.HeaderCenter ?? string.Empty;
                string right = diagram.HeaderFooter.HeaderRight ?? string.Empty;

                // Output the report line for this diagram.
                Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Header Left   : {left}");
                Console.WriteLine($"  Header Center : {center}");
                Console.WriteLine($"  Header Right  : {right}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}