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
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Dictionary to store font usage counts for the current page
                    Dictionary<string, int> fontCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Count fonts used in character formatting of the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            string fontName = ch.FontName.Value;
                            if (!string.IsNullOrEmpty(fontName))
                            {
                                if (fontCount.ContainsKey(fontName))
                                    fontCount[fontName]++;
                                else
                                    fontCount[fontName] = 1;
                            }
                        }
                    }

                    // Output the font usage for the current page
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                    if (fontCount.Count == 0)
                    {
                        Console.WriteLine("  No fonts found on this page.");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, int> kvp in fontCount)
                        {
                            Console.WriteLine($"  Font: {kvp.Key}, Occurrences: {kvp.Value}");
                        }
                    }
                    Console.WriteLine();
                }

                // Optional: list all fonts defined in the diagram
                Console.WriteLine("Fonts defined in the diagram:");
                foreach (Aspose.Diagram.Font font in diagram.Fonts)
                {
                    Console.WriteLine($"  {font.Name}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }