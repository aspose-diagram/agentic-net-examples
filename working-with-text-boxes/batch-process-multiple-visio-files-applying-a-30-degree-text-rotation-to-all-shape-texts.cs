using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter folder path containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Validate the folder path.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("The specified folder does not exist.");
            return;
        }

        // Define supported Visio file extensions.
        string[] visioExtensions = new[]
        {
            ".vsdx", ".vsd", ".vsx", ".vtx",
            ".vssx", ".vss", ".vstx", ".vst",
            ".vstm", ".vssm", ".vsdm"
        };

        // Prepare output directory.
        string outputDir = Path.Combine(folderPath, "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each Visio file in the folder.
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            // Skip files that do not have a Visio extension.
            if (!visioExtensions.Any(ext => ext.Equals(Path.GetExtension(filePath), StringComparison.OrdinalIgnoreCase)))
                continue;

            // Guard against missing files.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ignore logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply a 30-degree rotation to the shape's text.
                        // TextXForm.TxtAngle expects radians.
                        double angleRadians = Math.PI / 6.0; // 30 degrees = π/6 radians
                        shape.TextXForm.TxtAngle.Value = angleRadians;
                    }
                }

                // Build the output file path (preserve original name, add suffix).
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_rotated.vsdx";
                string outputPath = Path.Combine(outputDir, outputFileName);

                // Save the modified diagram as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}