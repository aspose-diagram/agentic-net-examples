using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <dataFeedCsvPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string dataFeedPath = args[1];
        string outputPath = args[2];

        // Guard against missing input Visio file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputPath}");
            return;
        }

        // Guard against missing data‑feed file.
        if (!File.Exists(dataFeedPath))
        {
            Console.Error.WriteLine($"Data feed file not found: {dataFeedPath}");
            return;
        }

        // Load the external data feed (CSV with two columns: ShapeNameU,Url).
        // Example line: Rectangle1,https://example.com
        Dictionary<string, string> urlMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            string[] lines = File.ReadAllLines(dataFeedPath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(new char[] { ',' }, 2);
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine($"Invalid CSV line (expected 2 columns): {line}");
                    continue;
                }

                string shapeName = parts[0].Trim();
                string url = parts[1].Trim();

                if (!string.IsNullOrEmpty(shapeName) && !string.IsNullOrEmpty(url))
                {
                    // Store the mapping; later shapes with the same name will use the last URL read.
                    urlMap[shapeName] = url;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading data feed: {ex.Message}");
            return;
        }

        // Load the Visio diagram, modify shapes, and save.
        try
        {
            // Mandatory using pattern for Diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's NameU matches a key in the URL map.
                        if (shape.NameU != null && urlMap.TryGetValue(shape.NameU, out string hyperlinkUrl))
                        {
                            // Create a new Hyperlink instance.
                            Hyperlink link = new Hyperlink()
                            {
                                // Assign a friendly name; can be the same as the shape name.
                                Name = shape.NameU
                            };
                            // Set the target address.
                            link.Address.Value = hyperlinkUrl;

                            // Add the hyperlink to the shape's collection.
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Hyperlinks have been populated successfully.");
    }
}