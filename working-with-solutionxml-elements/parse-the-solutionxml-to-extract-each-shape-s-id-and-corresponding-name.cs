using System;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through all SolutionXML entries in the document
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Parse the XML content stored in the SolutionXML element
                    XDocument xDoc;
                    try
                    {
                        xDoc = XDocument.Parse(solutionXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to parse SolutionXML '{solutionXml.Name}': {ex.Message}");
                        continue;
                    }

                    // Find all shape elements (assumed to be named "Shape") and extract ID and Name attributes
                    foreach (XElement shapeElem in xDoc.Descendants("Shape"))
                    {
                        // Attempt to read the ID attribute as a long
                        long id = 0;
                        XAttribute idAttr = shapeElem.Attribute("ID");
                        if (idAttr != null && long.TryParse(idAttr.Value, out long parsedId))
                        {
                            id = parsedId;
                        }

                        // Read the Name attribute (if present)
                        string name = shapeElem.Attribute("Name")?.Value ?? string.Empty;

                        Console.WriteLine($"Shape ID: {id}, Name: {name}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }