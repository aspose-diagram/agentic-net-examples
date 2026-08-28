using System;
using System.IO;
using Aspose.Diagram;

class DiagramCustomPropertiesSummary
{
    static void Main()
    {
        // Folder containing Visio diagram files
        string diagramsFolder = @"C:\Diagrams";

        // Output CSV file path
        string csvPath = @"C:\Diagrams\summary.csv";

        // Get all Visio files (adjust extensions as needed)
        string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");

        // Create CSV and write header
        using (var csvWriter = new StreamWriter(csvPath))
        {
            csvWriter.WriteLine("Filename,CustomPropertyCount");

            foreach (string filePath in diagramFiles)
            {
                // Load diagram from file
                using (var diagram = new Diagram(filePath))
                {
                    // Count custom properties in the document
                    int customPropCount = diagram.DocumentProps.CustomProps.Count;

                    // Write result line to CSV
                    string fileName = Path.GetFileName(filePath);
                    csvWriter.WriteLine($"{fileName},{customPropCount}");
                }
            }
        }

        Console.WriteLine("Summary CSV created at: " + csvPath);
    }
}
