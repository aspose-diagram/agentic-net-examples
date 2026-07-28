using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path
        string inputPath = "input.vsdx";
        // Guard to ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dictionary to store shape ID and its distinct font names
        Dictionary<long, List<string>> shapeFontMap = new Dictionary<long, List<string>>();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has character collection
                    if (shape.Chars == null)
                        continue;

                    List<string> fonts = new List<string>();

                    // Iterate through each character formatting entry in the shape
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Font name is stored in the FontName cell (use .Value)
                        if (ch.FontName != null && !string.IsNullOrEmpty(ch.FontName.Value))
                        {
                            string fontName = ch.FontName.Value;
                            // Add only distinct font names for this shape
                            if (!fonts.Contains(fontName))
                                fonts.Add(fontName);
                        }
                    }

                    // Record the fonts for the shape if any were found
                    if (fonts.Count > 0)
                        shapeFontMap[shape.ID] = fonts;
                }
            }

            // Output the collected font information
            foreach (KeyValuePair<long, List<string>> entry in shapeFontMap)
            {
                Console.WriteLine($"Shape ID: {entry.Key}");
                Console.WriteLine("Fonts: " + string.Join(", ", entry.Value));
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}