using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input Visio file and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing output folder (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare SVG save options for the current shape
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        svgOptions.SaveFormat = SaveFileFormat.Svg;
                        svgOptions.SVGFitToViewPort = true;          // Fit SVG to viewport (changed from BOOL.True)
                        svgOptions.ExportGuideShapes = false;        // Do not export guide shapes (changed from BOOL.False)
                        svgOptions.IsExportComments = false;        // Do not export comments (changed from BOOL.False)
                        svgOptions.PageIndex = page.ID;              // Export the current page
                        // svgOptions.PageCount = 1;                 // Removed: property does not exist

                        // Build output file name: Page_<PageId>_Shape_<ShapeId>.svg
                        string outputFileName = $"Page_{page.ID}_Shape_{shape.ID}.svg";
                        string outputPath = Path.Combine(outputFolder, outputFileName);

                        // Export the shape to SVG using the shape's ToSvg method
                        try
                        {
                            shape.ToSvg(outputPath, svgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                            continue;
                        }

                        // Embed shape custom properties as metadata attributes in the SVG
                        try
                        {
                            XmlDocument svgDoc = new XmlDocument();
                            svgDoc.Load(outputPath);

                            // The root element is typically <svg>
                            XmlElement root = svgDoc.DocumentElement;
                            if (root != null && shape.Props != null)
                            {
                                foreach (Prop prop in shape.Props)
                                {
                                    // Ensure property name is a valid XML attribute name
                                    string attrName = prop.Name;
                                    string attrValue = prop.Value?.Val ?? string.Empty; // Use Prop.Value.Val safely
                                    if (!string.IsNullOrEmpty(attrName))
                                    {
                                        // Add or update the attribute on the <svg> element
                                        if (root.HasAttribute(attrName))
                                            root.SetAttribute(attrName, attrValue);
                                        else
                                            root.SetAttribute(attrName, attrValue);
                                    }
                                }
                            }

                            // Save the modified SVG back to disk
                            svgDoc.Save(outputPath);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to embed metadata for shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                            // Continue processing other shapes even if metadata embedding fails
                        }
                    }
                }

                // TODO: Add version‑control repository handling (e.g., git add/commit) here if required.
                // This placeholder is intentional because external VCS tools are not part of the Aspose.Diagram API.
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
