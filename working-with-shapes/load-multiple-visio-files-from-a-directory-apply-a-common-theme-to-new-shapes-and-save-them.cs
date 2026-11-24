using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory containing Visio files.
        string inputDirectory;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputDirectory = args[0];
        }
        else
        {
            Console.Write("Enter the path to the directory containing Visio files: ");
            inputDirectory = Console.ReadLine();
        }

        // Guard against missing directory.
        if (string.IsNullOrWhiteSpace(inputDirectory) || !Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("The specified directory does not exist.");
            return;
        }

        // Get all Visio files with common extensions.
        string[] visioFiles = Directory.GetFiles(inputDirectory, "*.*", SearchOption.TopDirectoryOnly);
        if (visioFiles.Length == 0)
        {
            Console.Error.WriteLine("No files found in the specified directory.");
            return;
        }

        foreach (string filePath in visioFiles)
        {
            // Process only supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsx" && ext != ".vtx")
            {
                continue;
            }

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
                    // Apply a common preset theme to each page and its shapes.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the page's preset theme.
                        page.PresetTheme = PresetThemeValue.Bubble;

                        // Apply the same theme to every shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            shape.PresetTheme = PresetThemeValue.Bubble;
                        }
                    }

                    // Build the output file name (original name with "_themed" suffix).
                    string directory = Path.GetDirectoryName(filePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_themed.vsdx");

                    // Save the modified diagram using the required overload.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}