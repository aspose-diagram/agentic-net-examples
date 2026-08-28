using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Path for the output CSV file
            string outputCsv = "fonts.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect unique font names
            HashSet<string> uniqueFonts = new HashSet<string>();
            foreach (Font font in diagram.Fonts)
            {
                if (!string.IsNullOrEmpty(font.Name))
                {
                    uniqueFonts.Add(font.Name);
                }
            }

            // Export the unique font names to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                foreach (string fontName in uniqueFonts)
                {
                    // Enclose each font name in quotes to handle commas in names
                    writer.WriteLine($"\"{fontName}\"");
                }
            }

            Console.WriteLine($"Extracted {uniqueFonts.Count} unique fonts to '{outputCsv}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
