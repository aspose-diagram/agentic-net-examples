using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output SVG file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // NOTE: Aspose.Diagram retains OLE objects in SVG export by default.
            // If a specific property existed to control this behavior, it would be set here.
            // Example (hypothetical): svgOptions.PreserveOleObjects = BOOL.True;

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram successfully saved to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}