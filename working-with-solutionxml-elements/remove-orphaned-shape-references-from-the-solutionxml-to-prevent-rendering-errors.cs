using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path. If not supplied, create a new file name.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Build a set of all existing shape IDs across all pages.
                // -----------------------------------------------------------------
                HashSet<long> existingShapeIds = new HashSet<long>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        existingShapeIds.Add(shape.ID);
                    }
                }

                // -----------------------------------------------------------------
                // Identify SolutionXML elements that reference missing shape IDs.
                // -----------------------------------------------------------------
                List<SolutionXML> toRemove = new List<SolutionXML>();

                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // The XML content is stored in the XmlValue property.
                    string xmlContent = solXml.XmlValue;

                    if (string.IsNullOrWhiteSpace(xmlContent))
                    {
                        // Empty XML cannot contain references; keep it.
                        continue;
                    }

                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.LoadXml(xmlContent);
                    }
                    catch (XmlException)
                    {
                        // Malformed XML – consider it orphaned and schedule for removal.
                        toRemove.Add(solXml);
                        continue;
                    }

                    // Find any element that has a "ShapeID" attribute (case‑sensitive as per XML).
                    XmlNodeList nodesWithShapeId = doc.SelectNodes("//*[@ShapeID]");
                    bool hasOrphan = false;

                    if (nodesWithShapeId != null)
                    {
                        foreach (XmlNode node in nodesWithShapeId)
                        {
                            XmlAttribute attr = node.Attributes["ShapeID"];
                            if (attr == null) continue;

                            if (long.TryParse(attr.Value, out long shapeId))
                            {
                                if (!existingShapeIds.Contains(shapeId))
                                {
                                    // Reference to a non‑existent shape found.
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

                // -----------------------------------------------------------------
                // Remove the identified orphaned SolutionXML elements.
                // -----------------------------------------------------------------
                foreach (SolutionXML solXml in toRemove)
                {
                    diagram.SolutionXMLs.Remove(solXml);
                }

                // -----------------------------------------------------------------
                // Save the cleaned diagram.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Cleaned diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}