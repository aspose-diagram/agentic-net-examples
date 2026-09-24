using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file
        string diagramPath = "input.vsdx";
        // Verify the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Dictionary to store shape ID and its distinct paragraph font names
        var shapeParagraphFonts = new Dictionary<long, List<string>>();

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // List to collect unique font names for this shape
                    var fonts = new List<string>();

                    // Paragraph‑level font information is stored in the Char collection.
                    // Each Char has a FontName cell; we gather distinct values.
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Retrieve the font name from the Char cell
                        string fontName = ch.FontName.Value;

                        // Add non‑empty, non‑duplicate font names
                        if (!string.IsNullOrEmpty(fontName) && !fonts.Contains(fontName))
                        {
                            fonts.Add(fontName);
                        }
                    }

                    // Store the collected fonts (empty list if none found)
                    shapeParagraphFonts[shape.ID] = fonts;
                }
            }

            // Output the results for verification
            foreach (KeyValuePair<long, List<string>> entry in shapeParagraphFonts)
            {
                Console.WriteLine($"Shape ID: {entry.Key}");
                if (entry.Value.Count == 0)
                {
                    Console.WriteLine("  No paragraph fonts found.");
                }
                else
                {
                    foreach (string font in entry.Value)
                    {
                        Console.WriteLine($"  Paragraph Font: {font}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}