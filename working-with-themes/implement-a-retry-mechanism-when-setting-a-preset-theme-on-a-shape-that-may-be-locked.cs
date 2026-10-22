using System;
using System.IO;
using System.Threading;
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page and one shape.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page firstPage = diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the first page.
            Shape shape = firstPage.Shapes[0];

            // Define the preset theme and variant to apply.
            PresetThemeValue theme = PresetThemeValue.Bubble;
            PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;

            // Retry mechanism: attempt to set the theme up to 3 times.
            const int maxRetries = 3;
            int attempt = 0;
            bool applied = false;

            while (attempt < maxRetries && !applied)
            {
                try
                {
                    // Apply the preset theme and variant.
                    shape.PresetTheme = theme;
                    shape.PresetThemeVariant = variant;

                    // If no exception, mark as successful.
                    applied = true;
                }
                catch (Exception ex)
                {
                    attempt++;
                    Console.Error.WriteLine($"Attempt {attempt} to set theme failed: {ex.Message}");

                    // If we have more attempts left, wait briefly before retrying.
                    if (attempt < maxRetries)
                    {
                        Thread.Sleep(500); // 0.5 second pause
                    }
                    else
                    {
                        Console.Error.WriteLine("All retry attempts exhausted. Theme was not applied.");
                    }
                }
            }

            // Save the modified diagram if the theme was applied.
            if (applied)
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors from Aspose.Diagram operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}