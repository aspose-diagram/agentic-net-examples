using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vdx");

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                PageIndex = 0,               // Export the first page
                SVGFitToViewPort = true      // Fit SVG to viewport
            };

            // Save the diagram as an SVG file
            string svgPath = "output.svg";
            diagram.Save(svgPath, svgOptions);

            // Load the generated SVG for verification
            XDocument svgDoc = XDocument.Load(svgPath);

            // Namespace for xlink href attributes
            XNamespace xlink = "http://www.w3.org/1999/xlink";

            // Find all hyperlink (<a>) elements with an href attribute
            var hyperlinkElements = svgDoc
                .Descendants()
                .Where(e => e.Name.LocalName == "a" && e.Attribute(xlink + "href") != null)
                .ToList();

            // Output found hyperlinks
            foreach (var link in hyperlinkElements)
            {
                Console.WriteLine("Found hyperlink: " + link.Attribute(xlink + "href").Value);
            }

            // Simple test: ensure at least one hyperlink exists
            if (!hyperlinkElements.Any())
            {
                throw new Exception("No hyperlinks were found in the generated SVG.");
            }

            Console.WriteLine("Hyperlink verification passed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
