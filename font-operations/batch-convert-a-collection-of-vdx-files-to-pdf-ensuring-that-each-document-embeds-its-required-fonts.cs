using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchConvertVdxToPdf <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Gather all .vdx files in the input folder (non‑recursive).
        string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx");
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified input folder.");
            return;
        }

        // Prepare system‑installed font collection for validation.
        InstalledFontCollection systemFonts = new InstalledFontCollection();

        // Register the system font directory with Aspose.Diagram so that fonts can be embedded if supported.
        // Retrieve the default font directory from a temporary diagram instance.
        try
        {
            // Use a dummy diagram to obtain the default font directory.
            Diagram dummy = new Diagram();
            // GetDefaultFontDir returns an ArrayList of directories; use the first entry.
            ArrayList fontDirs = dummy.GetDefaultFontDir();
            if (fontDirs != null && fontDirs.Count > 0)
            {
                string defaultFontDir = fontDirs[0] as string;
                if (!string.IsNullOrEmpty(defaultFontDir))
                {
                    // Register the directory (recursive = true).
                    FontConfigs.SetFontFolder(defaultFontDir, true);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to configure font folder: {ex.Message}");
            // Continue; font configuration may still work without explicit registration.
        }

        // Set a fallback default font for rendering.
        FontConfigs.DefaultFontName = "Arial";

        foreach (string vdxPath in vdxFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but keep for safety).
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found, skipping: {vdxPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(vdxPath);

                // Validate that every font used in the diagram is installed on the system.
                List<string> missingFonts = new List<string>();
                foreach (Font font in diagram.Fonts)
                {
                    bool isInstalled = systemFonts.Families.Any(f => string.Equals(f.Name, font.Name, StringComparison.OrdinalIgnoreCase));
                    if (!isInstalled)
                    {
                        missingFonts.Add(font.Name);
                    }
                }

                if (missingFonts.Count > 0)
                {
                    Console.WriteLine($"Warning: The following fonts used in '{Path.GetFileName(vdxPath)}' are not installed on the system:");
                    foreach (string fName in missingFonts)
                    {
                        Console.WriteLine($"  - {fName}");
                    }
                    Console.WriteLine("The conversion will use the default fallback font where necessary.");
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Use a fallback font when a required font is missing.
                    DefaultFont = "Arial",
                    // Do not export hidden pages.
                    ExportHiddenPage = false
                };

                // Determine output PDF path.
                string outputFileName = Path.GetFileNameWithoutExtension(vdxPath) + ".pdf";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the diagram as PDF.
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Successfully converted '{Path.GetFileName(vdxPath)}' to PDF.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
                // Continue with next file.
            }
        }
    }
}
