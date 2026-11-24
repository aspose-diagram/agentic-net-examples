using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments or use defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to keep track of how many times each shape name has been seen.
            // Using case‑insensitive comparison to treat names like "Process" and "process" as duplicates.
            Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through every page and every shape on each page.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current universal name of the shape.
                    string currentName = shape.NameU;

                    // If this name hasn't been encountered yet, initialise its count.
                    if (!nameCounts.ContainsKey(currentName))
                    {
                        nameCounts[currentName] = 1; // First occurrence – keep original name.
                    }
                    else
                    {
                        // Duplicate found – increment the counter and rename the shape.
                        nameCounts[currentName]++; // e.g., second occurrence becomes count = 2.
                        int suffix = nameCounts[currentName];
                        string newName = $"{currentName}_{suffix}";

                        // Rename the shape by setting its NameU property.
                        shape.NameU = newName;

                        Console.WriteLine($"Renamed duplicate shape (ID={shape.ID}) from \"{currentName}\" to \"{newName}\".");
                    }
                }
            }

            // Save the modified diagram to the specified output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}