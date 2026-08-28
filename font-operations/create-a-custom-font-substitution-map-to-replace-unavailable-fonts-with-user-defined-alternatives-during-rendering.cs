using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Configure custom font folder (replace with your actual path)
                // The second parameter indicates whether to search subfolders recursively.
                FontConfigs.SetFontFolder(@"C:\CustomFonts", true);

                // Set a global fallback font for any missing fonts during rendering.
                FontConfigs.DefaultFontName = "Arial";

                // Load the Visio diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Retrieve the list of installed system fonts using Aspose.Drawing.Text.
                InstalledFontCollection installedFontCollection = new InstalledFontCollection();
                HashSet<string> installedFontNames = new HashSet<string>(
                    installedFontCollection.Families.Select(f => f.Name),
                    StringComparer.OrdinalIgnoreCase);

                // Enumerate fonts used in the diagram and report any that are not installed.
                foreach (Font font in diagram.Fonts)
                {
                    if (!installedFontNames.Contains(font.Name))
                    {
                        Console.WriteLine($"Missing font detected: '{font.Name}'. It will be substituted with the default font '{FontConfigs.DefaultFontName}'.");
                    }
                }

                // Prepare PDF save options and ensure the default font is set.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = FontConfigs.DefaultFontName
                };

                // Save the diagram to PDF, applying the font substitution.
                diagram.Save("output.pdf", pdfOptions);

                Console.WriteLine("Diagram saved successfully with font substitution.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }