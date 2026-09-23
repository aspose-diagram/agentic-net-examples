using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class SvgEllipseVerifier
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Export the diagram to SVG (lifecycle rule)
            diagram.Save("output.svg", SaveFileFormat.Svg);

            // Read the generated SVG content
            string svgContent = File.ReadAllText("output.svg");

            // Parse SVG XML
            XDocument svgDoc = XDocument.Parse(svgContent);

            // Namespace handling (Visio may include default namespace)
            XNamespace ns = svgDoc.Root.GetDefaultNamespace();

            // Find all <ellipse> elements
            var ellipses = svgDoc.Descendants(ns + "ellipse").ToList();

            if (!ellipses.Any())
            {
                Console.WriteLine("No <ellipse> elements found in the SVG.");
                return;
            }

            // Verify each ellipse corresponds to a circle (rx == ry)
            foreach (var ellipse in ellipses)
            {
                string rxStr = ellipse.Attribute("rx")?.Value;
                string ryStr = ellipse.Attribute("ry")?.Value;

                if (string.IsNullOrEmpty(rxStr) || string.IsNullOrEmpty(ryStr))
                {
                    Console.WriteLine("<ellipse> missing 'rx' or 'ry' attributes.");
                    continue;
                }

                if (double.TryParse(rxStr, out double rx) && double.TryParse(ryStr, out double ry))
                {
                    if (Math.Abs(rx - ry) < 0.001)
                    {
                        Console.WriteLine($"Valid circle found: cx={ellipse.Attribute("cx")?.Value}, cy={ellipse.Attribute("cy")?.Value}, r={rx}");
                    }
                    else
                    {
                        Console.WriteLine($"Ellipse is not a circle (rx={rx}, ry={ry}).");
                    }
                }
                else
                {
                    Console.WriteLine("Unable to parse 'rx' or 'ry' as numbers.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
