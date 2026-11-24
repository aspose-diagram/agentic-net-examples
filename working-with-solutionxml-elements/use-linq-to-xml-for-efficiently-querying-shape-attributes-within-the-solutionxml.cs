using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at path '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all SolutionXML elements in the diagram
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Retrieve the raw XML string stored in the SolutionXML element
                    string xmlContent = solutionXml.XmlValue;

                    // Parse the XML content using LINQ to XML
                    XDocument xDoc = XDocument.Parse(xmlContent);

                    // Query: find all <Shape> elements that have a "Name" attribute
                    var shapeElements = xDoc.Descendants("Shape")
                                            .Where(e => e.Attribute("Name") != null);

                    // Output the queried shape attributes
                    foreach (XElement shapeElem in shapeElements)
                    {
                        string shapeName = shapeElem.Attribute("Name")?.Value ?? "(no name)";
                        Console.WriteLine($"Found Shape - Name: {shapeName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}