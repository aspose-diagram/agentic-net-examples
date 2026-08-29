using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to hold shape ID -> list of paragraph font names
                Dictionary<long, List<string>> shapeFonts = new Dictionary<long, List<string>>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Use a HashSet to collect unique font names for the shape
                        HashSet<string> fontSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                        // Character-level font names (paragraph fonts are often stored here)
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch != null && !string.IsNullOrEmpty(ch.FontName.Value))
                            {
                                fontSet.Add(ch.FontName.Value);
                            }
                        }

                        // If any fonts were found, add them to the dictionary
                        if (fontSet.Count > 0)
                        {
                            shapeFonts[shape.ID] = new List<string>(fontSet);
                        }
                    }
                }

                // Output the collected font information
                foreach (KeyValuePair<long, List<string>> entry in shapeFonts)
                {
                    Console.WriteLine($"Shape ID: {entry.Key}");
                    Console.WriteLine("  Fonts:");
                    foreach (string fontName in entry.Value)
                    {
                        Console.WriteLine($"    {fontName}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }