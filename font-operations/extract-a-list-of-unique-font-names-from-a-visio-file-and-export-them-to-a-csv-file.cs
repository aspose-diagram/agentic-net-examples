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
            Console.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect unique font names from the diagram.
                HashSet<string> uniqueFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Font font in diagram.Fonts)
                {
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        uniqueFonts.Add(font.Name);
                    }
                }

                // Write the unique font names to a CSV file (one name per line).
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    foreach (string fontName in uniqueFonts)
                    {
                        writer.WriteLine(fontName);
                    }
                }

                Console.WriteLine($"Extracted {uniqueFonts.Count} unique font(s) to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}