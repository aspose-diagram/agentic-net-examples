using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Predefined width (in inches) to set for target shapes.
    private const double TargetWidthInches = 2.0;
    // NameU of the shape to resize. Adjust as needed.
    private const string TargetShapeNameU = "TargetShape";

    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <directoryPath>");
            return;
        }

        string directoryPath = args[0];

        // Verify the directory exists.
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

        // Process each VSDX file.
        foreach (string filePath in vsdxFiles)
        {
            // Guard against missing files (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only active (non‑deleted) shapes that match the target name.
                        if (shape.Del == BOOL.False && shape.NameU == TargetShapeNameU)
                        {
                            // Set the shape's width to the predefined value.
                            shape.XForm.Width.Value = TargetWidthInches;
                        }
                    }
                }

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading, processing, or saving.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}