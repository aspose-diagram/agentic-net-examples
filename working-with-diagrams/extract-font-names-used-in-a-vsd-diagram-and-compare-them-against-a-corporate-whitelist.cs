using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <whitelistPath>");
            return;
        }

        string diagramPath = args[0];
        string whitelistPath = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the whitelist file exists.
        if (!File.Exists(whitelistPath))
        {
            Console.Error.WriteLine($"Whitelist file not found: {whitelistPath}");
            return;
        }

        // Load the corporate whitelist (one font name per line, case‑insensitive).
        HashSet<string> whitelistSet;
        try
        {
            List<string> whitelistLines = new List<string>();
            foreach (string line in File.ReadAllLines(whitelistPath))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    whitelistLines.Add(trimmed);
                }
            }
            whitelistSet = new HashSet<string>(whitelistLines, StringComparer.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading whitelist file: {ex.Message}");
            return;
        }

        // Collect font names used in the diagram.
        HashSet<string> usedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over the diagram's Font collection.
                foreach (Font font in diagram.Fonts)
                {
                    // Font.Name holds the font's name.
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        usedFonts.Add(font.Name);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Output all fonts found in the diagram.
        Console.WriteLine("Fonts used in the diagram:");
        foreach (string fontName in usedFonts)
        {
            Console.WriteLine($"- {fontName}");
        }

        // Determine which fonts are not present in the whitelist.
        List<string> nonWhitelistedFonts = usedFonts.Where(f => !whitelistSet.Contains(f)).ToList();

        // Report fonts that violate the corporate whitelist.
        if (nonWhitelistedFonts.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Fonts NOT allowed by the corporate whitelist:");
            foreach (string fontName in nonWhitelistedFonts)
            {
                Console.WriteLine($"- {fontName}");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("All fonts used are allowed by the corporate whitelist.");
        }
    }
}