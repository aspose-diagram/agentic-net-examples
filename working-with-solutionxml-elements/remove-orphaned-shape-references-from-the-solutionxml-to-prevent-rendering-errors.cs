using System;
using System.Collections.Generic;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_cleaned.vsdx";

                // Load the diagram
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

                // Process each SolutionXML entry
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                        continue;

                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.LoadXml(solXml.XmlValue);
                    }
                    catch (XmlException)
                    {
                        // Skip malformed XML
                        continue;
                    }

                    // Find all nodes that have a "ShapeID" attribute
                    XmlNodeList nodesWithShapeId = xmlDoc.SelectNodes("//*[@ShapeID]");
                    if (nodesWithShapeId != null)
                    {
                        foreach (XmlNode node in nodesWithShapeId)
                        {
                            string idStr = node.Attributes["ShapeID"]?.Value;
                            if (long.TryParse(idStr, out long shapeId))
                            {
                                // If the shape ID does not exist in the diagram, remove the node
                                if (!existingShapeIds.Contains(shapeId))
                                {
                                    XmlNode parent = node.ParentNode;
                                    if (parent != null)
                                    {
                                        parent.RemoveChild(node);
                                    }
                                }
                            }
                        }
                    }

                    // Update the SolutionXML with the cleaned XML
                    solXml.XmlValue = xmlDoc.OuterXml;
                }

                // Save the cleaned diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Orphaned shape references removed and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }