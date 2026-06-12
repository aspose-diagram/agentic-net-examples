using System.IO;
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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Convert the PageCollection to a List<Page> for Parallel.ForEach
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Apply a preset theme to each page concurrently
                Parallel.ForEach(pages, page =>
                {
                    // Example: apply the Bubble theme with Variant1
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                });

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
