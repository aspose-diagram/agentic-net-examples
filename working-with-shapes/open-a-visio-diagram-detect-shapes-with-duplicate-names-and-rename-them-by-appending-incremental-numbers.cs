using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by API guidelines).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to track how many times each original shape name has been seen.
                Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all top‑level shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        ProcessShape(shape, nameCounts);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively processes a shape (and its children if it is a group) to ensure unique NameU values.
    /// </summary>
    /// <param name="shape">The shape to process.</param>
    /// <param name="nameCounts">Dictionary tracking occurrence counts of each original name.</param>
    private static void ProcessShape(Shape shape, Dictionary<string, int> nameCounts)
    {
        // Skip logically deleted shapes.
        if (shape.Del == BOOL.True)
            return;

        // Retrieve the current universal name (use empty string if null).
        string originalName = shape.NameU ?? string.Empty;

        if (nameCounts.TryGetValue(originalName, out int count))
        {
            // Duplicate found – increment count and rename.
            count++;
            string newName = $"{originalName}_{count}";
            shape.NameU = newName;
            nameCounts[originalName] = count;
        }
        else
        {
            // First occurrence – record it.
            nameCounts[originalName] = 1;
        }

        // If the shape is a group, process its child shapes recursively.
        if (shape.Type == TypeValue.Group)
        {
            foreach (Shape child in shape.Shapes)
            {
                ProcessShape(child, nameCounts);
            }
        }
    }
}