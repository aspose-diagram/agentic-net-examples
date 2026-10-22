using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output SVG file path
        string outputPath = "output.svg";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // TODO: Set font rendering mode to anti‑aliased if supported by future API versions.
            // Current Aspose.Diagram SVGSaveOptions does not expose a FontRenderingMode property.

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}