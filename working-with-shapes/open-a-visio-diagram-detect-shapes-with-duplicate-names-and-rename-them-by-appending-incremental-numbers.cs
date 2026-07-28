using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to track occurrences of each shape name (case‑insensitive)
            Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the universal name (NameU) as the base name; treat null as empty string
                    string baseName = shape.NameU ?? string.Empty;

                    if (nameCounts.ContainsKey(baseName))
                    {
                        // Duplicate found – increment counter and rename
                        nameCounts[baseName] += 1;
                        string newName = $"{baseName}_{nameCounts[baseName]}";
                        shape.NameU = newName;
                        shape.Name = newName;
                    }
                    else
                    {
                        // First occurrence of this name
                        nameCounts[baseName] = 1;
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
