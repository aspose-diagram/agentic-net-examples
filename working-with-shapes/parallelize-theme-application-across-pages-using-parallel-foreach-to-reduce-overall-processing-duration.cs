using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Gather all pages into a typed list for Parallel.ForEach
                    List<Page> pages = new List<Page>();
                    foreach (Page p in diagram.Pages)
                    {
                        pages.Add(p);
                    }

                    // Apply a preset theme to each page in parallel
                    Parallel.ForEach(pages, page =>
                    {
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    });

                    // Save the modified diagram
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