using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and keyword.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <keyword>");
            return;
        }

        string diagramPath = args[0];
        string keyword = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // HashSet to store unique shape IDs found.
            HashSet<long> shapeIds = new HashSet<long>();

            // Iterate over all SolutionXML elements in the document.
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                // The XML content is stored in the XmlValue property.
                string xmlContent = solutionXml.XmlValue;
                if (string.IsNullOrEmpty(xmlContent))
                {
                    continue;
                }

                // Parse the XML content.
                XDocument xDoc;
                try
                {
                    xDoc = XDocument.Parse(xmlContent);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Warning: Failed to parse SolutionXML '{solutionXml.Name}': {ex.Message}");
                    continue;
                }

                // Search for elements or attributes that contain the keyword.
                // If an element named "ShapeID" (or attribute containing "ID") is found,
                // attempt to parse its value as a long and add to the result set.
                foreach (XElement element in xDoc.Descendants())
                {
                    // Check element value for keyword.
                    if (element.Value != null && element.Value.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // If the element name suggests it holds an ID, try to parse.
                        if (element.Name.LocalName.Equals("ShapeID", StringComparison.OrdinalIgnoreCase))
                        {
                            if (long.TryParse(element.Value, out long id))
                            {
                                shapeIds.Add(id);
                            }
                        }
                    }

                    // Check each attribute of the element.
                    foreach (XAttribute attr in element.Attributes())
                    {
                        if (attr.Value != null && attr.Value.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // If attribute name suggests it holds an ID, try to parse.
                            if (attr.Name.LocalName.IndexOf("ID", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                if (long.TryParse(attr.Value, out long id))
                                {
                                    shapeIds.Add(id);
                                }
                            }
                        }
                    }
                }
            }

            // Output the collected shape IDs.
            if (shapeIds.Count == 0)
            {
                Console.WriteLine($"No shape IDs containing the keyword \"{keyword}\" were found in SolutionXML.");
            }
            else
            {
                Console.WriteLine($"Shape IDs containing the keyword \"{keyword}\":");
                foreach (long id in shapeIds)
                {
                    Console.WriteLine(id);
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related or general exceptions.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}