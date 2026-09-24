using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Collect pages into a typed list for Parallel.ForEach
                    List<Page> pages = new List<Page>();
                    foreach (Page page in diagram.Pages)
                    {
                        pages.Add(page);
                    }

                    // Apply a preset theme to each page in parallel
                    Parallel.ForEach(pages, page =>
                    {
                        // Example: apply the "Bubble" theme with variant 1
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    });

                    // Save the modified diagram
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Theme applied to all pages and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }