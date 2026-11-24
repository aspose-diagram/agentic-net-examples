using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Explicitly set the target format (required for SaveOptions).
                SaveFormat = SaveFileFormat.Svg,

                // Retain OLE objects – default behavior; no specific flag exists.
                // Additional options can be set here if needed, e.g., export guide shapes.
                ExportGuideShapes = true,
                IsExportComments = true
            };

            // Save the diagram as SVG while preserving OLE objects.
            diagram.Save(outputPath, svgOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}