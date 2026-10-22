using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputSvgPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect shape IDs to later assign CSS classes
            var shapeIds = new List<long>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Store the shape's unique ID
                    shapeIds.Add(shape.ID);
                }
            }

            // Export the entire diagram to SVG using default options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Example option settings (adjust as needed)
            svgOptions.SVGFitToViewPort = true;
            svgOptions.ExportGuideShapes = false;
            svgOptions.IsExportComments = false;

            diagram.Save(outputSvgPath, svgOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        try
        {
            // Read the generated SVG content
            string svgContent = File.ReadAllText(outputSvgPath);

            // Inject CSS class attributes based on shape IDs
            // This simple replacement assumes each shape element has an id attribute matching the shape ID.
            // Example: id="5" becomes id="5" class="shape-5"
            foreach (long id in GetShapeIdsFromSvg(svgContent))
            {
                string idAttribute = $"id=\"{id}\"";
                string replacement = $"id=\"{id}\" class=\"shape-{id}\"";
                svgContent = svgContent.Replace(idAttribute, replacement);
            }

            // Write the modified SVG back to the file
            File.WriteAllText(outputSvgPath, svgContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while adding CSS classes to SVG: {ex.Message}");
        }
    }

    // Helper method to extract shape IDs present in the SVG file.
    // This implementation looks for id="numeric" patterns.
    private static IEnumerable<long> GetShapeIdsFromSvg(string svgContent)
    {
        var ids = new List<long>();
        int index = 0;
        while ((index = svgContent.IndexOf("id=\"", index, StringComparison.Ordinal)) != -1)
        {
            int start = index + 4;
            int end = svgContent.IndexOf('"', start);
            if (end > start)
            {
                string idStr = svgContent.Substring(start, end - start);
                if (long.TryParse(idStr, out long id))
                {
                    ids.Add(id);
                }
                index = end + 1;
            }
            else
            {
                break;
            }
        }
        return ids;
    }
}