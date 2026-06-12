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
            const string sourcePath = "input.vsdx";
            // Path where the SVG output will be saved
            const string outputPath = "output.svg";

            try
            {
                // Load the Visio diagram using the provided constructor (load rule)
                using (var diagram = new Diagram(sourcePath))
                {
                    // Create SVG save options (save rule)
                    var svgOptions = new SVGSaveOptions
                    {
                        // Example option: fit the generated SVG to the view port
                        SVGFitToViewPort = true
                    };

                    // Save the diagram as SVG using the save method with options (save rule)
                    diagram.Save(outputPath, svgOptions);
                }

                Console.WriteLine("SVG conversion completed successfully.");
            }
            catch (DiagramException ex)
            {
                // Log detailed error information when a DiagramException occurs
                Console.Error.WriteLine($"Diagram conversion failed: {ex.Message}");
                Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
