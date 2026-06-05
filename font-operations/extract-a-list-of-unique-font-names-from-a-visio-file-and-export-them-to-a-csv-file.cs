using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (modify as needed)
                string visioPath = "input.vsdx";

                // Path to the output CSV file
                string csvPath = "fonts.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Collect unique font names
                HashSet<string> uniqueFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Font font in diagram.Fonts)
                {
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        uniqueFonts.Add(font.Name);
                    }
                }

                // Export the font names to a CSV file (one name per line)
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    foreach (string fontName in uniqueFonts)
                    {
                        writer.WriteLine(fontName);
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