using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <csvPath> [outputPath]");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard against missing files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Build a lookup dictionary of shape name (NameU) to Shape instance.
            Dictionary<string, Shape> shapeLookup = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Some shapes may have empty or null names; skip them.
                    if (!string.IsNullOrEmpty(shape.NameU))
                    {
                        // If duplicate names exist, the last one wins.
                        shapeLookup[shape.NameU] = shape;
                    }
                }
            }

            // Read CSV rows and bind data to matching shapes.
            using (StreamReader reader = new StreamReader(csvPath))
            {
                string line;
                int lineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    // Skip empty lines.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Simple CSV split – assumes no commas inside quoted fields.
                    string[] parts = line.Split(',');
                    if (parts.Length < 2)
                    {
                        Console.Error.WriteLine($"Invalid CSV format at line {lineNumber}: \"{line}\"");
                        continue;
                    }

                    string shapeName = parts[0].Trim();
                    string dataValue = parts[1].Trim();

                    if (shapeLookup.TryGetValue(shapeName, out Shape targetShape))
                    {
                        // Bind the CSV value to the shape's Data1 property.
                        targetShape.Data1 = dataValue;
                    }
                    else
                    {
                        Console.Error.WriteLine($"Shape \"{shapeName}\" not found in diagram (line {lineNumber}).");
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}