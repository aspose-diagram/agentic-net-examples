using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Input CSV file path (second argument or default)
        string csvPath = args.Length > 1 ? args[1] : "data.csv";
        // Output Visio file path (third argument or default)
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard against missing files
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read CSV lines (UTF-8 by default, preserves Unicode)
            using (StreamReader reader = new StreamReader(csvPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // Skip empty lines

                    // Simple CSV split – assumes commas are not part of the text.
                    // If commas can appear in the text, a more robust parser would be needed.
                    string[] parts = line.Split(',');
                    if (parts.Length < 2)
                    {
                        Console.Error.WriteLine($"Invalid CSV line (expected at least 2 columns): {line}");
                        continue;
                    }

                    // Parse shape ID
                    if (!long.TryParse(parts[0].Trim(), out long shapeId))
                    {
                        Console.Error.WriteLine($"Invalid shape ID in line: {line}");
                        continue;
                    }

                    // Reconstruct the text (join remaining parts to allow commas inside text)
                    string text = string.Join(",", parts, 1, parts.Length - 1).Trim();

                    // Find the shape across all pages
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        // Shape IDs are long, but GetShape expects int; cast safely if within range
                        if (shapeId >= int.MinValue && shapeId <= int.MaxValue)
                        {
                            Shape shape = page.Shapes.GetShape((int)shapeId);
                            if (shape != null)
                            {
                                targetShape = shape;
                                break;
                            }
                        }
                    }

                    if (targetShape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                        continue;
                    }

                    // Replace existing text while preserving Unicode characters
                    // Clear any existing text fragments
                    targetShape.Text.Value.Clear();

                    // Add new text fragment
                    targetShape.Text.Value.Add(new Txt(text));
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}