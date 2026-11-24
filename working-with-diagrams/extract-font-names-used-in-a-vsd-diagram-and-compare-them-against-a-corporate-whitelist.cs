using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input diagram path (from args or user input)
        string diagramPath;
        if (args.Length > 0)
        {
            diagramPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio diagram (VSD/VSDX): ");
            diagramPath = Console.ReadLine();
        }

        // Guard: ensure the file exists before proceeding
        if (string.IsNullOrEmpty(diagramPath) || !File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - '{diagramPath}'.");
            return;
        }

        // Define corporate whitelist of allowed font names (case‑insensitive)
        var whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Arial",
            "Calibri",
            "Times New Roman"
            // Add additional allowed fonts here
        };

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Collect font names used in the diagram
                var usedFonts = new List<string>();
                foreach (Font font in diagram.Fonts)
                {
                    if (font != null && !string.IsNullOrEmpty(font.Name))
                    {
                        usedFonts.Add(font.Name);
                    }
                }

                // Determine which fonts are not in the whitelist
                var disallowedFonts = new List<string>();
                foreach (string fontName in usedFonts.Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    if (!whitelist.Contains(fontName))
                    {
                        disallowedFonts.Add(fontName);
                    }
                }

                // Output the list of fonts used in the diagram
                Console.WriteLine("Fonts used in the diagram:");
                foreach (string fontName in usedFonts.Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"- {fontName}");
                }

                Console.WriteLine();

                // Report any fonts that are not allowed by the corporate whitelist
                if (disallowedFonts.Count == 0)
                {
                    Console.WriteLine("All fonts are within the corporate whitelist.");
                }
                else
                {
                    Console.WriteLine("Fonts NOT allowed by the corporate whitelist:");
                    foreach (string fontName in disallowedFonts)
                    {
                        Console.WriteLine($"- {fontName}");
                    }
                }

                // Optional: list installed system fonts using Aspose.Drawing.Text
                // Demonstrates usage of InstalledFontCollection as per guidelines
                var installedFonts = new InstalledFontCollection();
                Console.WriteLine();
                Console.WriteLine("Installed system fonts (first 10 shown):");
                int count = 0;
                foreach (var sysFont in installedFonts.Families)
                {
                    Console.WriteLine($"* {sysFont}");
                    if (++count >= 10) break;
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