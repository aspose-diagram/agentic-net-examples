using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file – adjust as needed.
        string inputPath = "input.vsdx";
        // Verify that the Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the intermediate CSV file that will hold shape metadata.
        string csvPath = "shapes.csv";

        // --------------------------------------------------------------------
        // STEP 1: Load the Visio diagram and extract shape metadata into CSV.
        // --------------------------------------------------------------------
        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to collect CSV lines; the first line is the header.
            List<string> csvLines = new List<string>
            {
                "PageName,ShapeID,ShapeName,ShapeNameU,MasterName,Text,CustomPropsCount"
            };

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve basic shape information, handling possible nulls.
                    string pageName = page.Name ?? string.Empty;
                    string shapeId = shape.ID.ToString();
                    string shapeName = shape.Name ?? string.Empty;
                    string shapeNameU = shape.NameU ?? string.Empty;
                    string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;

                    // Extract plain text from the shape, sanitising commas and line breaks.
                    string rawText = shape.Text.Value.Text;
                    string cleanText = rawText.Replace("\r", " ").Replace("\n", " ").Replace(",", " ");

                    // Count the number of custom properties (Props) attached to the shape.
                    int customPropsCount = shape.Props != null ? shape.Props.Count : 0;

                    // Assemble a CSV line with the collected data.
                    string csvLine = $"{pageName},{shapeId},{shapeName},{shapeNameU},{masterName},{cleanText},{customPropsCount}";
                    csvLines.Add(csvLine);
                }
            }

            // Write all CSV lines to the output file.
            File.WriteAllLines(csvPath, csvLines);
        }
        catch (Exception ex)
        {
            // Report any errors that occurred while processing the diagram.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // STEP 2: Read the CSV and generate a simple summary report.
        // --------------------------------------------------------------------
        try
        {
            // Read all lines from the CSV file.
            string[] allLines = File.ReadAllLines(csvPath);

            // Ensure there is data beyond the header.
            if (allLines.Length <= 1)
            {
                Console.WriteLine("No shape data found in CSV.");
                return;
            }

            // Counters for the summary.
            int totalShapes = 0;
            Dictionary<string, int> masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Process each data line (skip header at index 0).
            for (int i = 1; i < allLines.Length; i++)
            {
                // Split the CSV line into its constituent fields.
                string[] fields = allLines[i].Split(',');

                // Guard against malformed lines.
                if (fields.Length < 7) continue;

                totalShapes++;

                // The master name is the fifth column (index 4).
                string master = fields[4];

                // Tally the occurrence of each master type.
                if (masterCounts.ContainsKey(master))
                    masterCounts[master]++;
                else
                    masterCounts[master] = 1;
            }

            // Output the summary to the console.
            Console.WriteLine($"Total shapes processed: {totalShapes}");
            Console.WriteLine("Shapes per master type:");
            foreach (KeyValuePair<string, int> entry in masterCounts)
            {
                Console.WriteLine($"  {entry.Key}: {entry.Value}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred while generating the report.
            Console.Error.WriteLine($"Error generating report: {ex.Message}");
        }
    }
}