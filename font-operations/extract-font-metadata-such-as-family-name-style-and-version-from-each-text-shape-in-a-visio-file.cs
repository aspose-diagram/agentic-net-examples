using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains visible text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                        // Use a HashSet to avoid duplicate font entries per shape
                        HashSet<string> processedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                        // Iterate through each character formatting run in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Retrieve the font name used by this character
                            string fontName = ch.FontName?.Value ?? "Unknown";

                            // If this font has already been reported for the current shape, skip it
                            if (processedFonts.Contains(fontName))
                                continue;

                            processedFonts.Add(fontName);

                            // Font style and version are not exposed by Aspose.Diagram.Font
                            string fontStyle = "N/A";
                            string fontVersion = "N/A";

                            // Verify if the font exists in the diagram's font resources
                            bool fontFoundInDiagram = false;
                            foreach (Font font in diagram.Fonts)
                            {
                                if (string.Equals(font.Name, fontName, StringComparison.OrdinalIgnoreCase))
                                {
                                    fontFoundInDiagram = true;
                                    break;
                                }
                            }

                            Console.WriteLine($"  Font Name: {fontName}");
                            Console.WriteLine($"    Style: {fontStyle}");
                            Console.WriteLine($"    Version: {fontVersion}");
                            Console.WriteLine($"    Present in diagram fonts: {(fontFoundInDiagram ? "Yes" : "No")}");
                        }

                        Console.WriteLine(); // Blank line for readability between shapes
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
