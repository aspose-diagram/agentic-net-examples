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
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using Aspose.Diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect unique font names from the diagram.
            HashSet<string> uniqueFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Font font in diagram.Fonts)
            {
                // Guard against null or empty font names.
                if (!string.IsNullOrWhiteSpace(font.Name))
                {
                    uniqueFonts.Add(font.Name);
                }
            }

            // Export the unique font names to a CSV file.
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header.
                writer.WriteLine("FontName");

                // Write each font name, escaping double quotes if present.
                foreach (string name in uniqueFonts)
                {
                    string escaped = name.Replace("\"", "\"\"");
                    writer.WriteLine($"\"{escaped}\"");
                }
            }

            Console.WriteLine($"Successfully extracted {uniqueFonts.Count} unique font(s) to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing the Visio file: {ex.Message}");
        }
    }
}