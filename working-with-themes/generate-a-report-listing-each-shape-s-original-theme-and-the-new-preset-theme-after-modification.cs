using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare output paths
        string outputDiagramPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Themes1_modified.vsdx");
        string reportPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "ThemeReport.txt");

        // List to hold report lines
        List<string> reportLines = new List<string>();
        // Add CSV header
        reportLines.Add("ShapeID,ShapeNameU,OriginalTheme,NewTheme");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the current page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Retrieve shape identifiers
                        long shapeId = shape.ID; // Shape.ID is a long, no .Value needed
                        string shapeNameU = shape.NameU ?? string.Empty;

                        // Original theme cannot be read (write‑only), record as "N/A"
                        string originalTheme = "N/A";

                        // Apply a new preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Record the new theme information (the values we just set)
                        string newTheme = "Bubble Variant1";

                        // Add a line to the report
                        reportLines.Add($"{shapeId},{shapeNameU},{originalTheme},{newTheme}");
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            }

            // Write the report to a text file
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Theme modification completed. Report saved to: {reportPath}");
            Console.WriteLine($"Modified diagram saved to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}