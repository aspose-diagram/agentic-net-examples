using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "Drawing1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output SVG file path
        string outputSvgPath = "output.svg";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Draw a circle (ellipse with equal width and height) on the first page
            // Parameters: pinX, pinY, width, height
            diagram.Pages[0].DrawEllipse(1.0, 1.0, 2.0, 2.0);

            // Prepare SVG save options (default options are sufficient)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Save the diagram as SVG
            diagram.Save(outputSvgPath, svgOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Verify that the exported SVG contains an <ellipse> element
        try
        {
            // Guard: ensure the SVG file was created
            if (!File.Exists(outputSvgPath))
            {
                Console.Error.WriteLine($"SVG file not found after export: {outputSvgPath}");
                return;
            }

            // Load the SVG XML
            XDocument svgDoc = XDocument.Load(outputSvgPath);
            XNamespace svgNs = "http://www.w3.org/2000/svg";

            // Find the first <ellipse> element
            XElement ellipseElement = svgDoc.Descendants(svgNs + "ellipse").FirstOrDefault();

            if (ellipseElement == null)
            {
                throw new Exception("No <ellipse> element found in the exported SVG.");
            }

            // Verify required attributes exist (cx, cy, rx, ry)
            var requiredAttrs = new[] { "cx", "cy", "rx", "ry" };
            foreach (var attr in requiredAttrs)
            {
                if (ellipseElement.Attribute(attr) == null)
                {
                    throw new Exception($"<ellipse> element is missing required attribute '{attr}'.");
                }
            }

            Console.WriteLine("Verification succeeded: SVG contains a valid <ellipse> element.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification error: {ex.Message}");
        }
    }
}