using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing VSDX files (argument or user input)
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing VSDX files: ");
            folderPath = Console.ReadLine();
        }

        // Validate folder existence
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("The specified folder does not exist.");
            return;
        }

        // Get all VSDX files in the folder
        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx");
        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the specified folder.");
            return;
        }

        // Process each VSDX file
        foreach (string filePath in vsdxFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Apply a uniform preset theme to every page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Save the modified diagram back to the original file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Successfully processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}