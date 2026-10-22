using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as an argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate over all SolutionXML elements stored in the diagram.
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                // The XML content is stored in the XmlValue property as a string.
                string xmlContent = solutionXml.XmlValue;

                if (string.IsNullOrWhiteSpace(xmlContent))
                {
                    continue; // Skip empty entries.
                }

                // Parse the XML string using LINQ to XML.
                XDocument xDoc = XDocument.Parse(xmlContent);

                // Example query: find all <Shape> elements that have an attribute named "type"
                // with the value "process". Adjust the element/attribute names as needed for your schema.
                var matchingShapes = xDoc.Descendants("Shape")
                                         .Where(e => (string)e.Attribute("type") == "process")
                                         .Select(e => new
                                         {
                                             Id = (string)e.Attribute("id"),
                                             Name = (string)e.Attribute("name")
                                         });

                // Output the results.
                foreach (var shapeInfo in matchingShapes)
                {
                    Console.WriteLine($"Found shape - ID: {shapeInfo.Id}, Name: {shapeInfo.Name}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}