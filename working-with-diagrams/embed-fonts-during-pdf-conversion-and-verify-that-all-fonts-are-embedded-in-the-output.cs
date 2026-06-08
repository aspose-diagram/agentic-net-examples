using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string visioPath = "input.vsdx";

                // Path to the output PDF file
                string pdfPath = "output.pdf";

                // Configure font folder (system fonts) before loading the diagram
                string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                // The second parameter indicates recursive search
                FontConfigs.SetFontFolder(systemFontFolder, true);

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Verify that every font used in the diagram is installed on the system
                VerifyFontsEmbedded(diagram);

                // Set PDF save options with a fallback default font
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                // Save the diagram as PDF (fonts will be embedded if available)
                diagram.Save(pdfPath, pdfOptions);

                Console.WriteLine("PDF saved successfully with fonts embedded.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Checks that all fonts referenced by the diagram exist in the installed font collection
        private static void VerifyFontsEmbedded(Diagram diagram)
        {
            // Gather installed font names
            var installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            InstalledFontCollection installedFonts = new InstalledFontCollection();

            foreach (var family in installedFonts.Families)
            {
                // The family object may not have a strongly typed name property; use dynamic access
                var nameProperty = family.GetType().GetProperty("Name");
                if (nameProperty != null)
                {
                    string name = nameProperty.GetValue(family) as string;
                    if (!string.IsNullOrEmpty(name))
                    {
                        installedFontNames.Add(name);
                    }
                }
            }

            // Iterate over fonts used in the diagram
            foreach (Font font in diagram.Fonts)
            {
                if (!installedFontNames.Contains(font.Name))
                {
                    throw new Exception($"Required font '{font.Name}' is not installed on the system.");
                }
            }
        }
    }