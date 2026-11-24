using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect pages into a typed list to enable Parallel.ForEach (type inference issue).
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Apply a preset theme to each page in parallel.
                Parallel.ForEach(pages, page =>
                {
                    try
                    {
                        // Set the preset theme for the current page.
                        page.PresetTheme = PresetThemeValue.Bubble;
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while processing an individual page.
                        Console.Error.WriteLine($"Error applying theme to page ID {page.ID}: {ex.Message}");
                    }
                });

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during loading, processing, or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}