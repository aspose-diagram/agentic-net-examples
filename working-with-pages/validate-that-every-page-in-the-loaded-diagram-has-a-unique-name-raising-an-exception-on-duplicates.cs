using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class DiagramPageValidator
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Validate that each page has a unique universal name (NameU)
            EnsureUniquePageNames(diagram);

            // Continue with further processing or saving if needed
            // diagram.Save("output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void EnsureUniquePageNames(Diagram diagram)
    {
        // HashSet to track encountered page names
        HashSet<string> pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Iterate through all pages in the diagram
        foreach (Page page in diagram.Pages)
        {
            string name = page.NameU ?? string.Empty;

            // If the name already exists, raise an exception
            if (!pageNames.Add(name))
            {
                throw new InvalidOperationException(
                    $"Duplicate page name detected: \"{name}\". All pages must have unique names.");
            }
        }
    }
}
