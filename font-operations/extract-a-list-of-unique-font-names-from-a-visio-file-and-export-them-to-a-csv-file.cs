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

                // Input Visio file path
                string visioFilePath = @"C:\Path\To\Your\Diagram.vsdx";

                // Output CSV file path
                string csvOutputPath = @"C:\Path\To\Output\fonts.csv";

                // Load the Visio diagram using Aspose.Diagram
                Diagram diagram = new Diagram(visioFilePath);

                // Collect unique font names
                HashSet<string> uniqueFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // The Fonts collection contains Font objects; each Font has a Name property
                foreach (Font font in diagram.Fonts)
                {
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        uniqueFonts.Add(font.Name);
                    }
                }

                // Write the unique font names to a CSV file
                using (StreamWriter writer = new StreamWriter(csvOutputPath, false))
                {
                    // Optional header
                    writer.WriteLine("FontName");

                    foreach (string fontName in uniqueFonts)
                    {
                        // Escape double quotes if present
                        string escaped = fontName.Replace("\"", "\"\"");
                        writer.WriteLine($"\"{escaped}\"");
                    }
                }

                // Clean up
                diagram.Dispose();

                Console.WriteLine($"Extracted {uniqueFonts.Count} unique font(s) to '{csvOutputPath}'.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }