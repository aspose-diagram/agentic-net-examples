using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <visio-file>");
            return;
        }

        string visioPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Dictionary to hold original shape IDs and their names.
        var originalShapeInfo = new Dictionary<long, string>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages and shapes to capture IDs.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Store the shape's ID and its original name.
                    originalShapeInfo[shape.ID] = shape.NameU;
                }
            }

            // Rename each shape (e.g., prefix with "Renamed_").
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Preserve the original ID; only change the name.
                    shape.NameU = "Renamed_" + shape.NameU;
                }
            }

            // Validate that shape IDs are still the same after renaming.
            bool idsConsistent = true;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // If a shape ID is missing from the original collection, flag inconsistency.
                    if (!originalShapeInfo.ContainsKey(shape.ID))
                    {
                        Console.Error.WriteLine($"Inconsistent ID detected: {shape.ID}");
                        idsConsistent = false;
                    }
                }
            }

            // Report validation result.
            if (idsConsistent)
                Console.WriteLine("Validation successful: Shape IDs remain consistent after renaming.");
            else
                Console.WriteLine("Validation failed: Some shape IDs changed after renaming.");

            // Optionally, save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", 
                                            Path.GetFileNameWithoutExtension(visioPath) + "_renamed.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Renamed diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}