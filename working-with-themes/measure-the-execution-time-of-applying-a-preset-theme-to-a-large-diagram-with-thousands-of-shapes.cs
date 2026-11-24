using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Start measuring execution time.
            Stopwatch sw = Stopwatch.StartNew();

            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply a preset theme to each page and each shape.
                foreach (Page page in diagram.Pages)
                {
                    // Apply theme at page level.
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                    // Iterate through all shapes on the page.
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];
                        // Apply the same preset theme to the shape.
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }
                }

                // Stop the timer after theme application is complete.
                sw.Stop();

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Output the elapsed time.
            Console.WriteLine($"Theme applied in {sw.Elapsed.TotalMilliseconds} ms.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}