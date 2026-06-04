using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class DiagramCustomPropertiesSummary
{
    static void Main(string[] args)
    {
        // Directory containing Visio diagram files (e.g., .vsdx, .vsd)
        string diagramsFolder = @"C:\Diagrams";

        // Output CSV file path
        string csvPath = @"C:\Diagrams\CustomPropertiesSummary.csv";

        // Prepare a StringBuilder for CSV content
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Filename,CustomPropertyCount");

        // Enumerate all supported Visio files in the folder
        foreach (string filePath in Directory.EnumerateFiles(diagramsFolder, "*.*", SearchOption.TopDirectoryOnly))
        {
            // Filter by typical Visio extensions
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vsdm" && extension != ".vssx" && extension != ".vss")
                continue;

            // Load the diagram using the provided constructor
            using (var diagram = new Diagram(filePath))
            {
                // Access the collection of custom properties
                var customProps = diagram.DocumentProps.CustomProps;

                // Count the custom properties (if collection is null, count is zero)
                int count = customProps?.Count ?? 0;

                // Append the result to CSV
                string fileName = Path.GetFileName(filePath);
                csvBuilder.AppendLine($"{fileName},{count}");
            }
        }

        // Write the CSV content to the file system
        File.WriteAllText(csvPath, csvBuilder.ToString());

        Console.WriteLine($"Summary CSV created at: {csvPath}");
    }
}
