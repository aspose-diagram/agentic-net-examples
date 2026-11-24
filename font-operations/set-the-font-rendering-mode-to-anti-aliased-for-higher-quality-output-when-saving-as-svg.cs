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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // NOTE: The Aspose.Diagram API does not expose a FontRenderingMode
                // property for SVGSaveOptions. If such a feature becomes available,
                // set it here. For now, the default rendering is used.
                // TODO: Set font rendering mode to anti‑aliased when supported.

                // Save the diagram as SVG using the configured options.
                diagram.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"Diagram successfully saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}