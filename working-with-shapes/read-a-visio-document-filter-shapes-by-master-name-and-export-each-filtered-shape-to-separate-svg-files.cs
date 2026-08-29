using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – use first argument or a default placeholder.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Master name to filter shapes – use second argument or a default.
        string targetMasterName = args.Length > 1 ? args[1] : "Rectangle";

        // Output directory for generated SVG files.
        string outputDir = "ExportedSvgs";
        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that have no master (e.g., connectors, foreign objects).
                    if (shape.Master == null)
                        continue;

                    // Compare the shape's master name with the target master name.
                    if (string.Equals(shape.Master.Name, targetMasterName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Build a unique SVG file name using shape ID and page index.
                        string svgFileName = $"Page{pageIndex + 1}_Shape{shape.ID}_{targetMasterName}.svg";
                        string svgPath = Path.Combine(outputDir, svgFileName);

                        // Configure SVG save options (default options are sufficient for most cases).
                        SVGSaveOptions svgOptions = new SVGSaveOptions();

                        // Export the individual shape to an SVG file.
                        shape.ToSvg(svgPath, svgOptions);

                        // Inform the user about the successful export.
                        Console.WriteLine($"Exported shape ID {shape.ID} to '{svgPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}