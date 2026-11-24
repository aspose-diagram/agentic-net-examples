using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing output directory.
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        string svgPath = Path.Combine(outputDir, "exported.svg");

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Draw a circle (ellipse with equal width and height) on the first page.
                // Parameters: pinX, pinY, width, height.
                diagram.Pages[0].DrawEllipse(1.0, 1.0, 2.0, 2.0);

                // Prepare SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                // Export only the first page.
                svgOptions.PageIndex = 0;
                // Optional: fit SVG to viewport.
                svgOptions.SVGFitToViewPort = true;

                // Save the diagram as SVG.
                diagram.Save(svgPath, svgOptions);
            }

            // Verify that the exported SVG contains a correct <ellipse> element.
            string svgContent = File.ReadAllText(svgPath);

            // Regex to capture rx and ry attributes of the first <ellipse> element.
            Match match = Regex.Match(svgContent, @"<ellipse[^>]*rx\s*=\s*""([^""]+)""[^>]*ry\s*=\s*""([^""]+)""", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                throw new Exception("No <ellipse> element found in the exported SVG.");
            }

            string rxStr = match.Groups[1].Value;
            string ryStr = match.Groups[2].Value;

            if (!double.TryParse(rxStr, out double rx) || !double.TryParse(ryStr, out double ry))
            {
                throw new Exception("Failed to parse rx or ry values from the <ellipse> element.");
            }

            // For a circle, rx and ry should be equal (within a small tolerance).
            const double tolerance = 1e-6;
            if (Math.Abs(rx - ry) > tolerance)
            {
                throw new Exception($"Ellipse is not a circle: rx ({rx}) != ry ({ry}).");
            }

            Console.WriteLine("SVG verification succeeded: a correct <ellipse> element representing a circle was found.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}