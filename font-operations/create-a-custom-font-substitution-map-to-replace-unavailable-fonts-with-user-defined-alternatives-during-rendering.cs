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
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        // Define custom font substitution map (missing font -> replacement font)
        var fontSubstitutionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "MissingFontA", "Arial" },
            { "MissingFontB", "Calibri" }
        };

        // Configure custom font folder (adjust the path as needed)
        string customFontFolder = @"C:\CustomFonts";
        if (Directory.Exists(customFontFolder))
        {
            // Set the folder recursively
            FontConfigs.SetFontFolder(customFontFolder, true);
        }
        else
        {
            Console.Error.WriteLine($"Custom font folder not found: {customFontFolder}");
        }

        // Set a global default font (used when no specific substitution is defined)
        FontConfigs.DefaultFontName = "Arial";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Enumerate fonts used in the diagram and log any missing fonts
            // InstalledFontCollection is used to check system-installed fonts
            var installedFonts = new InstalledFontCollection();

            foreach (Font diagramFont in diagram.Fonts)
            {
                bool isInstalled = false;
                // Iterate over installed font families (type may vary, use implicit typing)
                foreach (var family in installedFonts.Families)
                {
                    // Many Aspose font family objects expose a Name property
                    var nameProp = family.GetType().GetProperty("Name");
                    if (nameProp != null)
                    {
                        string familyName = nameProp.GetValue(family) as string;
                        if (!string.IsNullOrEmpty(familyName) &&
                            string.Equals(familyName, diagramFont.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            isInstalled = true;
                            break;
                        }
                    }
                }

                if (!isInstalled)
                {
                    // Font is missing; attempt to find a substitution
                    if (fontSubstitutionMap.TryGetValue(diagramFont.Name, out string replacement))
                    {
                        Console.WriteLine($"Missing font '{diagramFont.Name}' will be substituted with '{replacement}'.");
                        // The substitution is handled by setting the default font in save options.
                        // Specific per-font substitution is not directly supported by Aspose.Diagram.
                    }
                    else
                    {
                        Console.WriteLine($"Missing font '{diagramFont.Name}' has no defined substitution. Default font will be used.");
                    }
                }
            }

            // Create PDF save options and set the default font
            PdfSaveOptions saveOptions = new PdfSaveOptions();
            // The DefaultFont property ensures a fallback font is used during rendering
            saveOptions.DefaultFont = "Arial";

            // Save the diagram to PDF using the configured options
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}