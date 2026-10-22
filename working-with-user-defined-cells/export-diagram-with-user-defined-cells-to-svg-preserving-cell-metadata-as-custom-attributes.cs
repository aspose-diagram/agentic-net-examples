using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,      // Do not export hidden pages
                    ExportGuideShapes = false,    // Do not export guide shapes
                    SVGFitToViewPort = true,      // Preserve viewbox
                    PageIndex = 0                 // Export first page
                    // PageCount property is not available in this version; omitted
                };

                // Save diagram to SVG
                diagram.Save(outputPath, svgOptions);
            }

            // Load the generated SVG for post‑processing
            XDocument svgDoc = XDocument.Load(outputPath);

            // Re‑load the diagram to access user‑defined cells
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process each user‑defined cell (User row)
                        foreach (User userField in shape.Users)
                        {
                            // Shape IDs are used as the 'id' attribute in the SVG
                            string shapeId = shape.ID.ToString();

                            // Find the SVG element with matching id
                            XElement element = FindElementById(svgDoc.Root, shapeId);
                            if (element != null)
                            {
                                // Create a custom attribute name prefixed with 'data-'
                                string attributeName = $"data-{userField.NameU}";

                                // Assign the cell value as the attribute value
                                element.SetAttributeValue(attributeName, userField.Value.Val);
                            }
                        }
                    }
                }
            }

            // Save the modified SVG back to the same file
            svgDoc.Save(outputPath);
            Console.WriteLine($"SVG exported successfully with custom attributes: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursive helper to locate an element with a specific id attribute
    private static XElement FindElementById(XElement root, string id)
    {
        if (root == null) return null;

        XAttribute idAttr = root.Attribute("id");
        if (idAttr != null && idAttr.Value == id)
            return root;

        foreach (XElement child in root.Elements())
        {
            XElement result = FindElementById(child, id);
            if (result != null)
                return result;
        }

        return null;
    }
}
