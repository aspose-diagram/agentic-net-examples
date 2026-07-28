using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class SvgConversion
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            const string inputPath = "input.vsdx";
            // Desired output SVG file path
            const string outputPath = "output.svg";

            try
            {
                // Load the Visio diagram using the built‑in constructor (lifecycle rule)
                using (var diagram = new Diagram(inputPath))
                {
                    // Create SVG save options (lifecycle rule)
                    var svgOptions = new SVGSaveOptions
                    {
                        // Example option: fit the generated SVG to the view port
                        SVGFitToViewPort = true
                    };

                    // Save the diagram as SVG using the Save method with SaveOptions (lifecycle rule)
                    diagram.Save(outputPath, svgOptions);
                }

                Console.WriteLine("Diagram successfully converted to SVG.");
            }
            catch (DiagramException ex)
            {
                // Log error details when a DiagramException occurs during conversion
                Console.Error.WriteLine($"Error converting diagram to SVG: {ex.Message}");
                Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
