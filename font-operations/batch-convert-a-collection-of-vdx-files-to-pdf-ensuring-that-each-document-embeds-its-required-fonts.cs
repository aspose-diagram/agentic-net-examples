using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output directories as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputDir> <outputDir>");
            return;
        }

        string inputDir = args[0];
        string outputDir = args[1];

        // Guard against missing input directory.
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        // Prepare system font folder configuration (must be set before loading diagrams).
        try
        {
            // Add the OS fonts folder to Aspose's font search paths.
            string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            // The second argument indicates recursive search.
            FontConfigs.SetFontFolder(systemFontFolder, true);
            // Set a generic fallback font name.
            FontConfigs.DefaultFontName = "Arial";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to configure font folders: {ex.Message}");
            // Continue; font configuration failure will be reported per file.
        }

        // Enumerate VDX files in the input directory.
        string[] vdxFiles = Directory.GetFiles(inputDir, "*.vdx", SearchOption.TopDirectoryOnly);
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified input directory.");
            return;
        }

        // Build a set of installed font names for validation.
        HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            // Families may not expose a strongly typed FontFamily; use dynamic access.
            foreach (var family in installedFonts.Families)
            {
                // Use reflection‑safe approach to get the name property.
                var nameProp = family.GetType().GetProperty("Name");
                if (nameProp != null)
                {
                    string name = nameProp.GetValue(family) as string;
                    if (!string.IsNullOrEmpty(name))
                    {
                        installedFontNames.Add(name);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to enumerate installed fonts: {ex.Message}");
            // Proceed without validation if enumeration fails.
        }

        // Process each VDX file.
        foreach (string vdxPath in vdxFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but required by rules).
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found: {vdxPath}");
                continue;
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vdxPath);
            string pdfPath = Path.Combine(outputDir, fileNameWithoutExt + ".pdf");

            try
            {
                // Load the diagram inside a using block as mandated.
                using (Diagram diagram = new Diagram(vdxPath))
                {
                    // Validate that all fonts used in the diagram are installed.
                    List<string> missingFonts = new List<string>();
                    foreach (Font font in diagram.Fonts)
                    {
                        if (!installedFontNames.Contains(font.Name))
                        {
                            missingFonts.Add(font.Name);
                        }
                    }

                    if (missingFonts.Count > 0)
                    {
                        Console.WriteLine($"Warning: Diagram '{fileNameWithoutExt}' uses fonts not installed on the system:");
                        foreach (string missing in missingFonts)
                        {
                            Console.WriteLine($"  - {missing}");
                        }
                        // The DefaultFont set in PdfSaveOptions will be used as fallback.
                    }

                    // Configure PDF save options.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";          // Fallback font.
                    pdfOptions.ExportHiddenPage = false;       // Plain bool, not BOOL.

                    // Save the diagram as PDF.
                    diagram.Save(pdfPath, pdfOptions);
                }

                Console.WriteLine($"Successfully converted '{vdxPath}' to PDF at '{pdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
            }
        }
    }
}