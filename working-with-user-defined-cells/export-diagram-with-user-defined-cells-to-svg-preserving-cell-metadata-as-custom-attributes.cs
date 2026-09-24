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

            // Paths for input Visio file and output SVG
            string inputPath = "input.vsdx";
            string outputSvgPath = "output.svg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                SVGFitToViewPort = true
            };

            // Export the diagram to SVG
            diagram.Save(outputSvgPath, svgOptions);

            // Load the generated SVG for post‑processing
            XDocument svgDoc = XDocument.Load(outputSvgPath);

            // Iterate through all shapes and embed user‑defined cells as custom attributes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Find the SVG element that corresponds to the shape ID
                    var element = svgDoc.Descendants()
                        .FirstOrDefault(e => (string)e.Attribute("id") == shape.ID.ToString());

                    if (element != null)
                    {
                        // Add each user‑defined cell as an attribute on the SVG element
                        foreach (User userCell in shape.Users)
                        {
                            // Use the cell name as attribute name and its value as attribute value
                            element.SetAttributeValue(userCell.Name, userCell.Value.Val);
                        }
                    }
                }
            }

            // Save the modified SVG with custom attributes
            svgDoc.Save(outputSvgPath);

            Console.WriteLine($"Diagram exported to SVG with custom attributes at: {outputSvgPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
