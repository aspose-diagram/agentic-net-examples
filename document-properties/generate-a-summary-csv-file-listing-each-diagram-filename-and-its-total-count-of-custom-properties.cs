using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder containing diagrams and output CSV file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <inputFolder> <outputCsv>");
            return;
        }

        string inputFolder = args[0];
        string outputCsv = args[1];

        var csvLines = new List<string>();
        csvLines.Add("Filename,CustomPropertiesCount");

        // Retrieve all Visio diagram files (adjust extension filter as needed)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

        foreach (string filePath in diagramFiles)
        {
            // Load diagram using Aspose.Diagram constructor (load rule)
            using (var diagram = new Diagram(filePath))
            {
                // Count custom properties in the document
                int customPropCount = diagram.DocumentProps.CustomProps.Count;

                // Add entry to CSV
                string fileName = Path.GetFileName(filePath);
                csvLines.Add($"{fileName},{customPropCount}");
            }
        }

        // Write the summary CSV file
        File.WriteAllLines(outputCsv, csvLines);
    }
}
