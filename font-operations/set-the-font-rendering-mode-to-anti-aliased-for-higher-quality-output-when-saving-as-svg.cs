using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the output SVG file path
        string outputPath = "output.svg";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Initialize SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // NOTE: FontRenderingMode is not available in this version of Aspose.Diagram.
            // Anti‑aliased font rendering is the default behavior for SVG export,
            // so no explicit property needs to be set.

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}