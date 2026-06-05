using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Dictionary to hold font name and its occurrence count on the current page
                    Dictionary<string, int> fontUsage = new Dictionary<string, int>();

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through character formatting runs of the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Retrieve the font name used in this character run
                            string fontName = ch.FontName.Value;

                            // Ignore empty font names
                            if (string.IsNullOrEmpty(fontName))
                                continue;

                            // Increment the count for this font
                            if (fontUsage.ContainsKey(fontName))
                                fontUsage[fontName]++;
                            else
                                fontUsage[fontName] = 1;
                        }
                    }

                    // Output the font usage report for the current page
                    Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) font usage:");
                    foreach (KeyValuePair<string, int> entry in fontUsage)
                    {
                        Console.WriteLine($"  Font: {entry.Key}, Count: {entry.Value}");
                    }
                    Console.WriteLine();
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }