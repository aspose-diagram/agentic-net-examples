using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramTextExport <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list to hold CSV rows
                List<string> csvLines = new List<string>();
                // Add header row
                csvLines.Add("PageName,ShapeID,ShapeName,Text");

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string text = shape.Text.Value.Text ?? string.Empty;

                        // Clean up the text for CSV (remove line breaks and commas)
                        text = text.Replace("\r\n", " ").Replace("\n", " ").Replace(",", " ");

                        // Prepare CSV line (escape double quotes if present)
                        string safeText = text.Contains("\"") ? $"\"{text.Replace("\"", "\"\"")}\"" : text;
                        string safePageName = page.Name?.Contains(",") == true ? $"\"{page.Name}\"" : page.Name;
                        string safeShapeName = shape.Name?.Contains(",") == true ? $"\"{shape.Name}\"" : shape.Name;

                        string csvLine = $"{safePageName},{shape.ID},{safeShapeName},{safeText}";
                        csvLines.Add(csvLine);
                    }
                }

                // Write all lines to the CSV file
                try
                {
                    File.WriteAllLines(outputPath, csvLines);
                    Console.WriteLine($"Export completed successfully. CSV saved to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing CSV file: {ex.Message}");
                }
            }
        }
    }