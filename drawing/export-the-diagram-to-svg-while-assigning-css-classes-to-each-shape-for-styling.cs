using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToSvgWithCss
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsd");

            // Configure SVG save options
            var svgOptions = new SVGSaveOptions
            {
                // Make the generated SVG fit the viewport
                SVGFitToViewPort = true
            };

            // Export the entire diagram to an SVG file
            string svgPath = "output.svg";
            diagram.Save(svgPath, svgOptions);

            // Load the generated SVG for post‑processing
            XDocument svgDoc = XDocument.Load(svgPath);
            XNamespace svgNs = "http://www.w3.org/2000/svg";

            // Iterate through shapes on the first page and assign a CSS class
            // based on the shape's ID (e.g., class="shape-5")
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                // The ID property is an integer unique to each shape
                string shapeId = shape.ID.ToString();

                // In the SVG, shapes are typically given an id attribute like "shape5"
                // Find the element with a matching id attribute
                var element = svgDoc
                    .Descendants()
                    .FirstOrDefault(e => (string)e.Attribute("id") == $"shape{shapeId}");

                if (element != null)
                {
                    // Assign a CSS class to the element for styling
                    element.SetAttributeValue("class", $"shape-{shapeId}");
                }
            }

            // Save the modified SVG back to disk
            svgDoc.Save(svgPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
