using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output SVG file path
        string outputPath = "output.svg";

        // ---------- Collect user-defined cells per shape ----------
        // Map shape ID to a list of its user-defined cells (must be accessible after diagram disposal)
        Dictionary<long, List<User>> shapeUserCells = new Dictionary<long, List<User>>();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------- Auto-space shapes before export ----------
                // Create auto-space options with default distances
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                // Apply auto-spacing to each page
                foreach (Page page in diagram.Pages)
                {
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // ---------- Gather user-defined cells ----------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users != null && shape.Users.Count > 0)
                        {
                            List<User> users = new List<User>();
                            foreach (User userCell in shape.Users)
                            {
                                users.Add(userCell);
                            }
                            if (users.Count > 0)
                            {
                                shapeUserCells[shape.ID] = users;
                            }
                        }
                    }
                }

                // ---------- Export diagram to SVG ----------
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    SVGFitToViewPort = true
                };
                // Save the diagram as SVG using the options
                diagram.Save(outputPath, svgOptions);
            }

            // ---------- Post-process SVG to embed user-defined cells as custom attributes ----------
            // Load the generated SVG XML
            XDocument svgDoc = XDocument.Load(outputPath);
            // Namespace of the SVG root element
            XNamespace svgNs = svgDoc.Root.Name.Namespace;

            // Iterate over all elements that have an 'id' attribute matching a shape ID
            foreach (XElement element in svgDoc.Descendants())
            {
                XAttribute idAttr = element.Attribute("id");
                if (idAttr != null && long.TryParse(idAttr.Value, out long shapeId))
                {
                    if (shapeUserCells.TryGetValue(shapeId, out List<User> users))
                    {
                        // Add each user-defined cell as a data-* attribute
                        foreach (User user in users)
                        {
                            // Determine a safe attribute name (use universal name if available)
                            string rawName = user.NameU ?? user.Name ?? "unknown";
                            string safeName = rawName.Replace(' ', '_');
                            string attrName = $"data-{safeName}";
                            string attrValue = user.Value?.Val ?? string.Empty;
                            element.SetAttributeValue(attrName, attrValue);
                        }
                    }
                }
            }

            // Save the modified SVG back to the same file
            svgDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
