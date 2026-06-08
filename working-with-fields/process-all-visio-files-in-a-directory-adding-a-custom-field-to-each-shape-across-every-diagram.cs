using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the directory to process. Use the first argument if provided,
            // otherwise use the current working directory.
            string inputDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine($"Error: Directory \"{inputDirectory}\" does not exist.");
                return;
            }

            // Create an output subfolder to store the modified diagrams.
            string outputDirectory = Path.Combine(inputDirectory, "Processed");
            Directory.CreateDirectory(outputDirectory);

            // Define Visio file extensions to process.
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vsdm", ".vssx", ".vss", ".vstx", ".vst" };

            // Gather all matching files.
            var files = Directory.GetFiles(inputDirectory, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var filePath in files)
            {
                if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLower()) < 0)
                    continue; // Skip non‑Visio files.

                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page and each shape.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Create a new custom field.
                            Field customField = new Field();

                            // Assign a value to the field. Adjust as needed.
                            customField.Value.Val = "CustomValue";

                            // Add the field to the shape.
                            shape.Fields.Add(customField);
                        }
                    }

                    // Save the modified diagram to the output folder in VSDX format.
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to process \"{filePath}\": {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }