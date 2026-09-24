using System;
using System.Collections.Generic;
using System.Xml;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get diagram file path
            string diagramPath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                diagramPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio diagram file: ");
                diagramPath = Console.ReadLine();
            }

            // Get keyword to search for
            string keyword;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                keyword = args[1];
            }
            else
            {
                Console.Write("Enter the keyword to search in SolutionXML: ");
                keyword = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(diagramPath) || string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Diagram path and keyword are required.");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // List to hold found shape IDs
            List<string> foundShapeIds = new List<string>();

            // Iterate through all SolutionXML elements in the diagram
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                if (string.IsNullOrWhiteSpace(solutionXml.XmlValue))
                    continue;

                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(solutionXml.XmlValue);
                }
                catch (XmlException)
                {
                    // Skip malformed XML
                    continue;
                }

                // Search every node for the keyword in its inner text
                XmlNodeList allNodes = xmlDoc.SelectNodes("//*");
                foreach (XmlNode node in allNodes)
                {
                    if (node.InnerText != null && node.InnerText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        // If the node has an attribute named "ID", capture its value
                        XmlAttribute idAttr = node.Attributes?["ID"];
                        if (idAttr != null && !string.IsNullOrWhiteSpace(idAttr.Value))
                        {
                            foundShapeIds.Add(idAttr.Value);
                        }
                    }
                }
            }

            // Output the results
            if (foundShapeIds.Count == 0)
            {
                Console.WriteLine($"No shape IDs found containing the keyword \"{keyword}\" in SolutionXML.");
            }
            else
            {
                Console.WriteLine($"Shape IDs containing the keyword \"{keyword}\":");
                foreach (string id in foundShapeIds)
                {
                    Console.WriteLine(id);
                }
            }
        }
    }