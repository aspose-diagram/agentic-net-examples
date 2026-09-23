using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Path to the output CSV file
            string csvPath = "fonts.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Use a HashSet to store unique font names
            HashSet<string> uniqueFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all fonts defined in the diagram
            foreach (Font font in diagram.Fonts)
            {
                if (!string.IsNullOrEmpty(font.Name))
                {
                    uniqueFonts.Add(font.Name);
                }
            }

            // Write the unique font names to a CSV file
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Optional header
                writer.WriteLine("FontName");

                foreach (string fontName in uniqueFonts)
                {
                    // Escape double quotes if needed
                    string escaped = fontName.Replace("\"", "\"\"");
                    writer.WriteLine($"\"{escaped}\"");
                }
            }

            Console.WriteLine($"Extracted {uniqueFonts.Count} unique font(s) to '{csvPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
