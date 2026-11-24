using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> [targetFormat]");
            return;
        }

        string diagramPath = args[0];
        // Target format is kept for extensibility; not used in this example
        string targetFormat = args.Length >= 2 ? args[1] : "Pdf";

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve installed system fonts using Aspose.Drawing.Text
                InstalledFontCollection installedFonts = new InstalledFontCollection();
                HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var family in installedFonts.Families)
                {
                    // Add each font family name to the set
                    installedFontNames.Add(family.Name);
                }

                int pageNumber = 0; // Manual page counter (Page.Index does not exist)

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Text object
                        if (shape.Text == null)
                            continue;

                        // Get the plain text of the shape
                        string plainText = shape.Text.Value.Text;
                        if (string.IsNullOrWhiteSpace(plainText))
                            continue; // Skip shapes without visible text

                        // Inspect each character run for its font name
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            Aspose.Diagram.Char ch = shape.Chars[i];
                            string fontName = ch.FontName?.Value ?? string.Empty;

                            // Skip if the font name is empty
                            if (string.IsNullOrWhiteSpace(fontName))
                                continue;

                            // Report fonts that are not installed on the system
                            if (!installedFontNames.Contains(fontName))
                            {
                                Console.WriteLine("Unsupported font detected:");
                                Console.WriteLine($"  Page Number : {pageNumber}");
                                Console.WriteLine($"  Shape ID    : {shape.ID}");
                                Console.WriteLine($"  Shape NameU: {shape.NameU}");
                                Console.WriteLine($"  Font Name   : {fontName}");
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}