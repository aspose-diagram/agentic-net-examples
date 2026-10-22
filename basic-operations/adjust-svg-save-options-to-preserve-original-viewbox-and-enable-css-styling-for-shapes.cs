using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Preserve the original viewbox by fitting the SVG to the viewport
            svgOptions.SVGFitToViewPort = true; // corrected: use bool instead of BOOL

            // NOTE: Aspose.Diagram does not provide a direct option to export shapes with external CSS.
            // Shapes are exported with inline style attributes. To apply CSS styling, post‑process the
            // generated SVG file (e.g., extract style attributes and write a separate CSS file).

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"SVG file saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
