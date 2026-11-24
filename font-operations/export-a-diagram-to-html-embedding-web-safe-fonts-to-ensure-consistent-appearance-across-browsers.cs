using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        // Guard: ensure the input file exists
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (optional argument)
        string outputDir = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputDir))
        {
            outputDir = Directory.GetCurrentDirectory();
        }

        // Guard: ensure the output directory exists or can be created
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: Unable to create output directory '{outputDir}'. {ex.Message}");
            return;
        }

        try
        {
            // Configure font handling: add system font folder and set a web‑safe default font
            string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            FontConfigs.SetFontFolder(systemFontFolder, true);
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build a set of installed font family names for validation
                var installedFamilyNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var installedFonts = new InstalledFontCollection();
                foreach (var family in installedFonts.Families)
                {
                    // The family object provides a Name property
                    installedFamilyNames.Add(family.Name);
                }

                // Verify that each font used in the diagram is installed on the system
                foreach (Font diagramFont in diagram.Fonts)
                {
                    string fontName = diagramFont.Name ?? string.Empty;
                    if (!installedFamilyNames.Contains(fontName))
                    {
                        Console.Error.WriteLine($"Warning: Font '{fontName}' used in diagram is not installed on the system. It will be substituted with the default font.");
                    }
                }

                // Export each page of the diagram to a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure HTML save options for the current page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,
                        PageCount = 1,
                        ExportHiddenPage = false // do not export hidden pages
                    };

                    // Build output file name (e.g., page_1.html)
                    string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                    // Save the page as HTML
                    diagram.Save(outputPath, htmlOptions);
                    Console.WriteLine($"Page {i + 1} exported to HTML: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}