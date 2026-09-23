using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to scan. Use the first argument if provided; otherwise, use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Prepare CSV content with a header row.
            List<string> csvLines = new List<string>();
            csvLines.Add("Filename,CustomPropertyCount");

            // Retrieve all files in the specified folder.
            string[] files = Directory.GetFiles(folderPath);

            foreach (string filePath in files)
            {
                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Count the custom properties in the document.
                    int customPropCount = diagram.DocumentProps.CustomProps.Count;

                    // Extract just the file name for the CSV.
                    string fileName = Path.GetFileName(filePath);

                    // Add the result line to the CSV collection.
                    csvLines.Add($"{fileName},{customPropCount}");
                }
                catch (Exception ex)
                {
                    // If the file cannot be processed, note the error in the CSV and write a message to the console.
                    string fileName = Path.GetFileName(filePath);
                    csvLines.Add($"{fileName},Error");
                    Console.WriteLine($"Error processing '{fileName}': {ex.Message}");
                }
            }

            // Write the CSV file to the same folder.
            string outputPath = Path.Combine(folderPath, "DiagramCustomPropertiesSummary.csv");
            File.WriteAllLines(outputPath, csvLines);
            Console.WriteLine($"Summary CSV created at: {outputPath}");
        }
    }