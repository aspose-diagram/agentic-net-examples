using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <MasterName> [OutputFolder]");
            return;
        }

        string visioPath = args[0];
        string masterName = args[1];
        string outputFolder = args.Length >= 3 ? args[2] : Path.Combine(Path.GetDirectoryName(visioPath) ?? "", "ExportedShapes");

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Guard: ensure output folder exists or create it.
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Prepare SVG save options (default configuration).
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Counter for exported shapes to generate unique filenames.
            int exportedCount = 0;

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an associated master.
                    if (shape.Master == null)
                        continue;

                    // Filter by the specified master name.
                    if (string.Equals(shape.Master.Name, masterName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Build a unique filename for the SVG output.
                        string fileName = $"Shape_{shape.ID}_{Guid.NewGuid():N}.svg";
                        string outputPath = Path.Combine(outputFolder, fileName);

                        try
                        {
                            // Export the shape to SVG using the shape's ToSvg method.
                            shape.ToSvg(outputPath, svgOptions);
                            exportedCount++;
                            Console.WriteLine($"Exported shape ID {shape.ID} to '{outputPath}'.");
                        }
                        catch (Exception exportEx)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {exportEx.Message}");
                        }
                    }
                }
            }

            Console.WriteLine($"Export completed. Total shapes exported: {exportedCount}");
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred while processing the Visio file: {ex.Message}");
        }
    }
}