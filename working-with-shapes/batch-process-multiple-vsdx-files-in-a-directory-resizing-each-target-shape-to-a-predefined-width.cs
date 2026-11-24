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
            Console.Error.WriteLine("Usage: Program <directoryPath> <targetShapeName> [widthInInches]");
            return;
        }

        string directoryPath = args[0];
        string targetShapeName = args[1];
        double targetWidth = 2.0; // Default width in inches.

        // Optional width argument.
        if (args.Length >= 3)
        {
            if (!double.TryParse(args[2], out targetWidth) || targetWidth <= 0)
            {
                Console.Error.WriteLine("Invalid width value. It must be a positive number.");
                return;
            }
        }

        // Guard: ensure the directory exists.
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Get all VSDX files in the directory.
        string[] vsdxFiles = Directory.GetFiles(directoryPath, "*.vsdx");
        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the specified directory.");
            return;
        }

        foreach (string filePath in vsdxFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted (inactive) shapes.
                        if (shape.Del != BOOL.False)
                            continue;

                        // Check if the shape name matches the target (case‑insensitive).
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Resize the shape's width to the predefined value.
                            shape.XForm.Width.Value = targetWidth;
                        }
                    }
                }

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}