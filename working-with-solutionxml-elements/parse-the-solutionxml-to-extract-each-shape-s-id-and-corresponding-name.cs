using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all SolutionXML elements stored in the document
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // The XML content is stored in the XmlValue property
                    string xmlContent = solutionXml.XmlValue;

                    if (string.IsNullOrWhiteSpace(xmlContent))
                    {
                        continue; // Skip empty entries
                    }

                    // Parse the XML content
                    XDocument doc;
                    try
                    {
                        doc = XDocument.Parse(xmlContent);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to parse SolutionXML '{solutionXml.Name}': {ex.Message}");
                        continue;
                    }

                    // Look for <Shape> elements with ID and Name attributes
                    foreach (XElement shapeElem in doc.Descendants("Shape"))
                    {
                        XAttribute idAttr = shapeElem.Attribute("ID");
                        XAttribute nameAttr = shapeElem.Attribute("Name");

                        if (idAttr != null && nameAttr != null)
                        {
                            Console.WriteLine($"Shape ID: {idAttr.Value}, Name: {nameAttr.Value}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}