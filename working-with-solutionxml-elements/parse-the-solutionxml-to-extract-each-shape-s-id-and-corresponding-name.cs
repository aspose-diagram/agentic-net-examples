using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all SolutionXML elements stored at the document level.
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Ensure the XML content is not empty.
                    if (string.IsNullOrWhiteSpace(solutionXml.XmlValue))
                        continue;

                    // Parse the XML string into an XDocument for querying.
                    XDocument doc;
                    try
                    {
                        doc = XDocument.Parse(solutionXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Warning: Unable to parse SolutionXML '{solutionXml.Name}'. {ex.Message}");
                        continue;
                    }

                    // Look for elements named "Shape" that contain ID and Name attributes.
                    foreach (XElement shapeElement in doc.Descendants("Shape"))
                    {
                        XAttribute idAttr = shapeElement.Attribute("ID");
                        XAttribute nameAttr = shapeElement.Attribute("Name");

                        if (idAttr == null || nameAttr == null)
                            continue; // Skip elements missing required attributes.

                        // Parse the ID as a long (Visio shape IDs are long).
                        if (!long.TryParse(idAttr.Value, out long shapeId))
                        {
                            Console.Error.WriteLine($"Warning: Invalid shape ID '{idAttr.Value}' in SolutionXML '{solutionXml.Name}'.");
                            continue;
                        }

                        string shapeName = nameAttr.Value;

                        // Output the extracted shape information.
                        Console.WriteLine($"Shape ID: {shapeId}, Name: {shapeName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}