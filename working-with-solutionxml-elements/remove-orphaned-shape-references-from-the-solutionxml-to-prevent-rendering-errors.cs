using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Build a set of all existing shape IDs across all pages
                // ------------------------------------------------------------
                HashSet<long> existingShapeIds = new HashSet<long>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        existingShapeIds.Add(shape.ID);
                    }
                }

                // ------------------------------------------------------------
                // Identify SolutionXML elements that become empty after cleaning
                // ------------------------------------------------------------
                List<SolutionXML> solutionXmlsToRemove = new List<SolutionXML>();

                // Iterate over each SolutionXML element stored at the document level
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // Skip if the XML content is null or whitespace
                    if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                    {
                        continue;
                    }

                    // Load the XML string into an XmlDocument for manipulation
                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.LoadXml(solXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to parse SolutionXML '{solXml.Name}': {ex.Message}");
                        continue;
                    }

                    bool modified = false;

                    // Local function to process nodes that contain a specific attribute
                    void ProcessNodesWithAttribute(string attributeName)
                    {
                        XmlNodeList nodes = xmlDoc.SelectNodes($"//*[@{attributeName}]");
                        if (nodes == null) return;

                        foreach (XmlNode node in nodes)
                        {
                            XmlAttribute attr = node.Attributes[attributeName];
                            if (attr == null) continue;

                            // Try to interpret the attribute value as a shape ID
                            if (long.TryParse(attr.Value, out long shapeId))
                            {
                                // If the shape ID does not exist in the diagram, remove the node
                                if (!existingShapeIds.Contains(shapeId))
                                {
                                    XmlNode parent = node.ParentNode;
                                    if (parent != null)
                                    {
                                        parent.RemoveChild(node);
                                        modified = true;
                                    }
                                }
                            }
                        }
                    }

                    // Process common attribute names that may hold shape references
                    ProcessNodesWithAttribute("ShapeID");
                    ProcessNodesWithAttribute("ID");

                    // If modifications were made, update or remove the SolutionXML element
                    if (modified)
                    {
                        // If the document element has no child nodes, mark the whole SolutionXML for removal
                        if (xmlDoc.DocumentElement == null || !xmlDoc.DocumentElement.HasChildNodes)
                        {
                            solutionXmlsToRemove.Add(solXml);
                        }
                        else
                        {
                            // Save the cleaned XML back into the SolutionXML object
                            solXml.XmlValue = xmlDoc.OuterXml;
                        }
                    }
                }

                // Remove any SolutionXML elements that are now empty
                foreach (SolutionXML emptySolXml in solutionXmlsToRemove)
                {
                    diagram.SolutionXMLs.Remove(emptySolXml);
                }

                // Save the cleaned diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log unexpected errors during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}