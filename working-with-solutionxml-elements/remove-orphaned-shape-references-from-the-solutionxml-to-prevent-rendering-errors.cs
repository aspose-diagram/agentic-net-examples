using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Build a set of all existing shape IDs across all pages
            HashSet<long> existingShapeIds = new HashSet<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    existingShapeIds.Add(shape.ID);
                }
            }

            // Collect SolutionXML entries that reference missing shapes
            List<SolutionXML> toRemove = new List<SolutionXML>();
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                bool hasOrphan = false;

                // Parse the XML content; if parsing fails, treat as orphaned
                XDocument doc;
                try
                {
                    doc = XDocument.Parse(solXml.XmlValue);
                }
                catch
                {
                    hasOrphan = true;
                    doc = null;
                }

                if (doc != null)
                {
                    // Look for any attribute named "ShapeID" (common convention)
                    foreach (XElement elem in doc.Descendants())
                    {
                        XAttribute attr = elem.Attribute("ShapeID");
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

            // Save the cleaned diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
