using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath> <reportPath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];
        string reportPath = args[2];

        // Guard against missing input file
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Ensure the directory for CSV output exists
        try
        {
            string csvDir = Path.GetDirectoryName(csvPath);
            if (!string.IsNullOrEmpty(csvDir) && !Directory.Exists(csvDir))
                Directory.CreateDirectory(csvDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating CSV directory: {ex.Message}");
            return;
        }

        // Load Visio diagram and export shape metadata to CSV
        try
        {
            Diagram diagram = new Diagram(visioPath);
            // Export to CSV using Aspose.Diagram built‑in support
            diagram.Save(csvPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
            return;
        }

        // Ensure the directory for the report exists
        try
        {
            string reportDir = Path.GetDirectoryName(reportPath);
            if (!string.IsNullOrEmpty(reportDir) && !Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating report directory: {ex.Message}");
            return;
        }

        // Generate a simple summary report from the CSV
        try
        {
            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"Error: CSV file not found at '{csvPath}'.");
                return;
            }

            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                Console.Error.WriteLine("Error: CSV file is empty.");
                return;
            }

            // First line is header
            int totalShapes = lines.Length - 1; // exclude header

            // Count shapes per page (assuming first column is Page)
            Dictionary<string, int> shapesPerPage = new Dictionary<string, int>();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                // Simple split; CSV produced by Aspose does not contain commas in fields
                string[] parts = line.Split(',');
                if (parts.Length == 0)
                    continue;

                string pageKey = parts[0]; // page identifier
                if (shapesPerPage.ContainsKey(pageKey))
                    shapesPerPage[pageKey]++;
                else
                    shapesPerPage[pageKey] = 1;
            }

            // Build report content
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("Visio Diagram Summary Report");
                writer.WriteLine($"Source Visio File: {visioPath}");
                writer.WriteLine($"Generated CSV: {csvPath}");
                writer.WriteLine();
                writer.WriteLine($"Total Shapes: {totalShapes}");
                writer.WriteLine();

                writer.WriteLine("Shapes per Page:");
                foreach (var kvp in shapesPerPage)
                {
                    writer.WriteLine($"  Page {kvp.Key}: {kvp.Value} shape(s)");
                }
            }

            Console.WriteLine($"Report generated successfully at '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating report: {ex.Message}");
            return;
        }
    }
}