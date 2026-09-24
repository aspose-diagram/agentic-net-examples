using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual path)
                string inputPath = "large_diagram.vsdx";
                // Path for the output file after applying the theme
                string outputPath = "large_diagram_themed.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Start timing the theme application
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Apply a preset theme to each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Example theme; adjust as needed
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Stop timing
                stopwatch.Stop();

                // Output the elapsed time
                Console.WriteLine($"Applying preset theme took: {stopwatch.Elapsed.TotalMilliseconds} ms");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }