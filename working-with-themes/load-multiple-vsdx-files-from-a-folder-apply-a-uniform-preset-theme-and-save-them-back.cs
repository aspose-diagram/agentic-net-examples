using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Verify the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all VSDX files in the specified folder.
        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx");

        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the folder.");
            return;
        }

        // Process each VSDX file.
        foreach (string filePath in vsdxFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Apply the preset theme to every page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set a uniform preset theme (Bubble) and a variant.
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Save the modified diagram back to the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Successfully processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Write any Aspose or IO errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}