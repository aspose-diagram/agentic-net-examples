using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output Visio file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Collect all existing shape IDs across all pages
            HashSet<long> existingShapeIds = new HashSet<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    existingShapeIds.Add(shape.ID);
                }
            }

            // Identify SolutionXML elements that reference non‑existent shapes
            List<SolutionXML> toRemove = new List<SolutionXML>();
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                bool hasOrphan = false;

                // Parse the XML content; if malformed, treat as orphaned
                XDocument? doc = null;
                try
                {
                    doc = XDocument.Parse(solXml.XmlValue);
                }
                catch
                {
                    hasOrphan = true;
                    // No need to continue parsing; mark for removal
                }

                if (!hasOrphan && doc != null)
                {
                    // Look for any attribute named "ShapeID" (common convention)
                    foreach (XElement elem in doc.Descendants())
                    {
                        XAttribute? attr = elem.Attribute("ShapeID");
                        if (attr != null && long.TryParse(attr.Value, out long shapeId))
                        {
                            if (!existingShapeIds.Contains(shapeId))
                            {
                                hasOrphan = true;
                                break;
                            }
                        }
                    }
                }

                if (hasOrphan)
                {
                    toRemove.Add(solXml);
                }
            }

            // Remove the identified orphaned SolutionXML entries
            foreach (SolutionXML orphan in toRemove)
            {
                diagram.SolutionXMLs.Remove(orphan);
            }

            // Save the cleaned diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}