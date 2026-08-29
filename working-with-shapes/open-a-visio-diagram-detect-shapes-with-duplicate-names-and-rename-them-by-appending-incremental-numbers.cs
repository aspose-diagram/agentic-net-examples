using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class RenameDuplicateShapes
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Dictionary to keep track of how many times each shape name appears
            var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without a name
                    if (string.IsNullOrEmpty(shape.Name))
                        continue;

                    // If the name has been seen before, rename the shape
                    if (nameCounts.ContainsKey(shape.Name))
                    {
                        // Increment the occurrence count
                        nameCounts[shape.Name]++;

                        // Append an incremental number to make the name unique
                        shape.Name = $"{shape.Name}_{nameCounts[shape.Name]}";
                    }
                    else
                    {
                        // First occurrence of this name
                        nameCounts[shape.Name] = 1;
                    }
                }
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
