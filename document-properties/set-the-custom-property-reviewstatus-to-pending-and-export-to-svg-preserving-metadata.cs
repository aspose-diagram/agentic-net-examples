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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Add or update the custom document property "ReviewStatus".
                // ------------------------------------------------------------
                CustomProp reviewProp = new CustomProp();
                reviewProp.Name = "ReviewStatus";
                reviewProp.PropType = PropType.String;
                // Set the value to "Pending".
                reviewProp.CustomValue.ValueString = "Pending";

                // Add the custom property to the document's custom properties collection.
                diagram.DocumentProps.CustomProps.Add(reviewProp);

                // ------------------------------------------------------------
                // Configure SVG export options to preserve metadata.
                // ------------------------------------------------------------
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Preserve comments (metadata) in the SVG.
                    IsExportComments = true,
                    // Export guide shapes if present.
                    ExportGuideShapes = true,
                    // Fit the drawing to the SVG viewport.
                    SVGFitToViewPort = true,
                    // Export all pages starting from the first page.
                    PageIndex = 0
                    // PageCount property is optional; default behavior exports all pages.
                };

                // ------------------------------------------------------------
                // Save the diagram as SVG using the configured options.
                // ------------------------------------------------------------
                diagram.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"Diagram exported successfully to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
