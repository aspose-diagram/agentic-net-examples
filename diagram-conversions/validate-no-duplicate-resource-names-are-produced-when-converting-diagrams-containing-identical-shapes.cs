using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class DiagramResourceValidator
{
    static void Main()
    {
        try
        {

            // Load the source diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // HashSet to track unique resource (shape) names
            HashSet<string> resourceNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            // List to collect any duplicate names found
            List<string> duplicateNames = new List<string>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Use the universal name (NameU) as the resource identifier
                    string name = shape.NameU;

                    // If the name is already present, record it as duplicate
                    if (!resourceNames.Add(name))
                    {
                        duplicateNames.Add(name);
                    }
                }
            }

            // Report the validation result
            if (duplicateNames.Count == 0)
            {
                Console.WriteLine("Validation passed: No duplicate resource names found.");
            }
            else
            {
                Console.WriteLine("Validation failed: Duplicate resource names detected:");
                foreach (string dup in duplicateNames)
                {
                    Console.WriteLine($"- {dup}");
                }
            }

            // Optionally, save the diagram after validation (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
