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

            // Path to the Visio file to be validated
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Track all layer names across the document
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var duplicateReports = new List<string>();

            // Iterate through each page and its layers
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    string name = layer.Name.Value;

                    // If the name already exists, record a duplicate
                    if (!seenNames.Add(name))
                    {
                        duplicateReports.Add($"Duplicate layer name \"{name}\" found on page \"{page.Name}\".");
                    }
                }
            }

            // Output the validation results
            if (duplicateReports.Count == 0)
            {
                Console.WriteLine("All layer names are unique.");
            }
            else
            {
                Console.WriteLine("Duplicate layer names detected:");
                foreach (string report in duplicateReports)
                {
                    Console.WriteLine(report);
                }
            }

            // Save the diagram (optional, demonstrates proper save usage)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
