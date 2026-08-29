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
        try
        {

            // Input Visio file path (default if not provided)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            // Desired SVG output path (default if not provided)
            string outputPath = args.Length > 1 ? args[1] : "output.svg";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false; // do not export hidden pages

            // Save to a temporary SVG file first
            string tempSvgPath = Path.ChangeExtension(Path.GetTempFileName(), ".svg");
            diagram.Save(tempSvgPath, svgOptions);

            // Load the generated SVG as XML
            XDocument svgDoc = XDocument.Load(tempSvgPath);

            // Iterate all pages and shapes to embed user‑defined cells as custom attributes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Find the SVG element that corresponds to this shape (by id attribute)
                    XElement svgElement = svgDoc.Descendants()
                                                .FirstOrDefault(e => (string)e.Attribute("id") == shape.ID.ToString());

                    if (svgElement != null)
                    {
                        // Add each user‑defined cell as an attribute on the SVG element
                        foreach (User userCell in shape.Users)
                        {
                            // Use the cell's name as attribute name and its value as attribute value
                            svgElement.SetAttributeValue(userCell.Name, userCell.Value.Val);
                        }
                    }
                }
            }

            // Save the modified SVG to the final output location
            svgDoc.Save(outputPath);

            // Clean up the temporary file
            try
            {
                File.Delete(tempSvgPath);
            }
            catch
            {
                // If deletion fails, ignore – the temp file will be removed later by the OS
            }

            Console.WriteLine($"Diagram exported to SVG with custom attributes at: {outputPath}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
