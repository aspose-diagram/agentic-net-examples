using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Dictionary to hold font name and its occurrence count on the current page
                    Dictionary<string, int> fontCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through character formatting runs within the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Retrieve the font name; FontName is a Cell, so use .Value
                            string fontName = ch.FontName.Value;

                            if (!string.IsNullOrEmpty(fontName))
                            {
                                if (fontCounts.ContainsKey(fontName))
                                    fontCounts[fontName]++;
                                else
                                    fontCounts[fontName] = 1;
                            }
                        }
                    }

                    // Output the report for the current page
                    Console.WriteLine($"Page: {page.NameU}");
                    if (fontCounts.Count == 0)
                    {
                        Console.WriteLine("  No fonts found on this page.");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, int> entry in fontCounts)
                        {
                            Console.WriteLine($"  Font: {entry.Key}, Occurrences: {entry.Value}");
                        }
                    }

                    Console.WriteLine(); // Blank line between pages
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }