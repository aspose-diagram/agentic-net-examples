using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line argument for the target directory.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: Please provide the directory path containing VDX files as the first argument.");
            return;
        }

        string directoryPath = args[0];

        // Guard: ensure the directory exists.
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Error: Directory does not exist – \"{directoryPath}\"");
            return;
        }

        // Retrieve all .vdx files in the directory (non‑recursive).
        string[] vdxFiles = Directory.GetFiles(directoryPath, "*.vdx");

        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified directory.");
            return;
        }

        // Process each VDX file.
        foreach (string filePath in vdxFiles)
        {
            // Guard: double‑check file existence (defensive programming).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Warning: File not found – \"{filePath}\". Skipping.");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Update header text with the current date.
                // Using the standard Visio field code &d for date.
                diagram.HeaderFooter.HeaderCenter = $"Date: {DateTime.Now:yyyy-MM-dd}";

                // Optionally configure header font appearance.
                var headerFont = diagram.HeaderFooter.HeaderFooterFont;
                headerFont.FaceName = "Arial";
                headerFont.Height = 12;          // Font size in points.
                headerFont.Weight = 700;        // 700 = bold.
                headerFont.Underline = BOOL.False;

                // Save the diagram back to the same file (preserving VDX format).
                diagram.Save(filePath, SaveFileFormat.Vdx);
                Console.WriteLine($"Successfully updated header for \"{Path.GetFileName(filePath)}\".");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing \"{filePath}\": {ex.Message}");
            }
        }
    }
}