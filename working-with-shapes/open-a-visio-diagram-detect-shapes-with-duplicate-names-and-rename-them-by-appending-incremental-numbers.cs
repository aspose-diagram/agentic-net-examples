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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path where the modified file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram using the constructor that accepts a file path
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to keep track of how many times each name appears
                var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        string name = shape.Name;
                        if (string.IsNullOrEmpty(name))
                            continue; // Skip shapes without a name

                        if (nameCounts.ContainsKey(name))
                        {
                            // Duplicate detected – increment the counter and rename
                            int index = ++nameCounts[name];
                            shape.Name = $"{name}_{index}";
                        }
                        else
                        {
                            // First occurrence of this name
                            nameCounts[name] = 1;
                        }
                    }
                }

                // Save the modified diagram using the Save method that accepts a file path and format
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
