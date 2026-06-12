using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(diagramPath);
            Dictionary<long, List<string>> shapeParagraphFonts = new Dictionary<long, List<string>>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    // Collect font names from character formatting within the shape
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        string fontName = ch.FontName.Value;
                        if (string.IsNullOrWhiteSpace(fontName))
                            continue;

                        if (!shapeParagraphFonts.TryGetValue(shape.ID, out List<string> fonts))
                        {
                            fonts = new List<string>();
                            shapeParagraphFonts[shape.ID] = fonts;
                        }

                        if (!fonts.Contains(fontName))
                            fonts.Add(fontName);
                    }
                }
            }

            foreach (var kvp in shapeParagraphFonts)
            {
                Console.WriteLine($"Shape ID: {kvp.Key}");
                Console.WriteLine(" Paragraph Fonts: " + string.Join(", ", kvp.Value));
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}