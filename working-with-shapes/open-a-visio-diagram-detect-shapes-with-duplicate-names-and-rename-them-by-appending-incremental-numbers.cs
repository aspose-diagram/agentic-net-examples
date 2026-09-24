using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Dictionary to keep track of name occurrences (case‑insensitive)
            var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the current shape name
                    string currentName = shape.Name;

                    // If the name has been seen before, rename it
                    if (nameCounts.ContainsKey(currentName))
                    {
                        // Increment the counter for this base name
                        int duplicateIndex = ++nameCounts[currentName];

                        // Create a new unique name by appending the index
                        string newName = $"{currentName}_{duplicateIndex}";

                        // Apply the new name to the shape
                        shape.Name = newName;
                    }
                    else
                    {
                        // First occurrence of this name – initialize its counter
                        nameCounts[currentName] = 1;
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
