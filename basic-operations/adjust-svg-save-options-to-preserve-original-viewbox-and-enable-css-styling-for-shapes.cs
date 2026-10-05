using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output SVG file path
        string outputPath = "output.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Preserve the original viewbox by fitting SVG to viewport
            svgOptions.SVGFitToViewPort = true;

            // TODO: Enable CSS styling for shapes if supported by a future API version.
            // Currently Aspose.Diagram does not expose a property to enable CSS styling.

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram successfully saved to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}