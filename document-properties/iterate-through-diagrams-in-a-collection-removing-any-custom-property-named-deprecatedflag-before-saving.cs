using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Create output folder if it does not exist.
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

        // Get all Visio files in the input folder (including subfolders if needed).
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Process only supported Visio formats.
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vssx" && extension != ".vstx")
            {
                continue;
            }

            // Guard against missing file.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Access the custom properties collection.
                    var customProps = diagram.DocumentProps.CustomProps;
                    if (customProps != null && customProps.Count > 0)
                    {
                        // Collect properties named "DeprecatedFlag" for removal.
                        var toRemove = new List<CustomProp>();
                        foreach (CustomProp prop in customProps)
                        {
                            if (prop.Name == "DeprecatedFlag")
                            {
                                toRemove.Add(prop);
                            }
                        }

                        // Remove the identified custom properties.
                        foreach (CustomProp prop in toRemove)
                        {
                            customProps.Remove(prop);
                        }
                    }

                    // Determine output path (overwrite in output folder).
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram back to VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}