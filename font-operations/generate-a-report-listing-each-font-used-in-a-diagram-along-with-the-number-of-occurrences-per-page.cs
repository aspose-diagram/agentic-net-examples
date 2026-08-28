using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the diagram file path
                string diagramPath;
                if (args.Length > 0)
                {
                    diagramPath = args[0];
                }
                else
                {
                    Console.Write("Enter the path to the Visio diagram file: ");
                    diagramPath = Console.ReadLine();
                }

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect all font names used in the diagram
                HashSet<string> allFontNames = new HashSet<string>();
                foreach (Font font in diagram.Fonts) // explicit type as required
                {
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        allFontNames.Add(font.Name);
                    }
                }

                // Iterate through each page and count font occurrences
                foreach (Page page in diagram.Pages) // explicit type
                {
                    // Initialize count dictionary for this page
                    Dictionary<string, int> fontCounts = new Dictionary<string, int>();
                    foreach (string fn in allFontNames)
                    {
                        fontCounts[fn] = 0;
                    }

                    // Examine each shape on the page
                    foreach (Shape shape in page.Shapes) // explicit type
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Count fonts used in character formatting runs
                        foreach (Aspose.Diagram.Char ch in shape.Chars) // explicit type
                        {
                            string fontName = ch.FontName.Value;
                            if (!string.IsNullOrEmpty(fontName) && fontCounts.ContainsKey(fontName))
                            {
                                fontCounts[fontName]++;
                            }
                        }
                    }

                    // Output the report for the current page
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");
                    bool anyFont = false;
                    foreach (var kvp in fontCounts)
                    {
                        if (kvp.Value > 0)
                        {
                            Console.WriteLine($"  Font: {kvp.Key} - Occurrences: {kvp.Value}");
                            anyFont = true;
                        }
                    }

                    if (!anyFont)
                    {
                        Console.WriteLine("  No fonts detected on this page.");
                    }

                    Console.WriteLine(); // Blank line between pages
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }