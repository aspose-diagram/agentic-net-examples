using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Validate folder existence.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Get all Visio files (VSDX and VSD) in the folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");
        string[] legacyFiles = Directory.GetFiles(folderPath, "*.vsd");
        string[] allFiles = new string[diagramFiles.Length + legacyFiles.Length];
        diagramFiles.CopyTo(allFiles, 0);
        legacyFiles.CopyTo(allFiles, diagramFiles.Length);

        if (allFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        // Process each diagram file.
        foreach (string inputPath in allFiles)
        {
            // Guard: ensure the file exists.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine("File not found: " + inputPath);
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply a preset theme to the shape.
                            // Example theme: Bubble. Adjust as needed.
                            shape.PresetTheme = PresetThemeValue.Bubble;

                            // Optionally apply a variant (write‑only property).
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        }
                    }

                    // Construct output file name (original name with "_themed" suffix).
                    string directory = Path.GetDirectoryName(inputPath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(directory, fileNameWithoutExt + "_themed.vsdx");

                    // Save the modified diagram using the required overload.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}