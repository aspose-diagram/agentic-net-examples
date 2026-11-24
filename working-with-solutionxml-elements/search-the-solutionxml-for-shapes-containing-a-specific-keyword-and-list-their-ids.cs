using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <Keyword>");
            return;
        }

        string visioPath = args[0];
        string keyword = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // List to collect shape IDs that match the keyword.
            List<long> matchingShapeIds = new List<long>();

            // Iterate over all SolutionXML elements in the document.
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Quick check: does the raw XML contain the keyword (case‑insensitive)?
                if (solXml.XmlValue != null &&
                    solXml.XmlValue.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Load the XML fragment into an XmlDocument for parsing.
                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.LoadXml(solXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        // If the XML is malformed, report and continue with next SolutionXML.
                        Console.Error.WriteLine($"Warning: Unable to parse SolutionXML '{solXml.Name}'. {ex.Message}");
                        continue;
                    }

                    // Attempt to find shape identifiers.
                    // Common patterns: <Shape ID="123"> or <Shape ShapeID="123">
                    XmlNodeList shapeNodes = xmlDoc.SelectNodes("//Shape[@ID] | //Shape[@ShapeID]");
                    foreach (XmlNode node in shapeNodes)
                    {
                        string idStr = node.Attributes["ID"]?.Value ?? node.Attributes["ShapeID"]?.Value;
                        if (long.TryParse(idStr, out long shapeId))
                        {
                            // Verify that the shape actually exists in the diagram before adding.
                            // Shapes are stored per page; we need to search all pages.
                            bool exists = false;
                            foreach (Page page in diagram.Pages)
                            {
                                // Shapes.GetShape returns null if not found.
                                Shape shape = page.Shapes.GetShape(shapeId);
                                if (shape != null)
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            if (exists && !matchingShapeIds.Contains(shapeId))
                            {
                                matchingShapeIds.Add(shapeId);
                            }
                        }
                    }
                }
            }

            // Output the results.
            if (matchingShapeIds.Count == 0)
            {
                Console.WriteLine($"No shapes containing the keyword \"{keyword}\" were found in any SolutionXML.");
            }
            else
            {
                Console.WriteLine($"Shapes containing the keyword \"{keyword}\":");
                foreach (long id in matchingShapeIds)
                {
                    Console.WriteLine($"- Shape ID: {id}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related exceptions and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}