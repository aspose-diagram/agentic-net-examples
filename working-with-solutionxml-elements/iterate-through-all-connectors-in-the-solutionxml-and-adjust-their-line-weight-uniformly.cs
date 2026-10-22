using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Uniform line weight to apply (in inches)
                const double uniformLineWeight = 0.02;

                // Iterate through all SolutionXML elements in the diagram
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrEmpty(solXml.XmlValue))
                        continue; // Skip empty XML

                    // Load the XML content of the SolutionXML element
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(solXml.XmlValue);

                    // Find all connector nodes (assumed to be named "Connector")
                    XmlNodeList connectorNodes = xmlDoc.GetElementsByTagName("Connector");
                    foreach (XmlNode connectorNode in connectorNodes)
                    {
                        // Set or update the LineWeight attribute to the uniform value
                        XmlAttribute lineWeightAttr = connectorNode.Attributes["LineWeight"];
                        if (lineWeightAttr == null)
                        {
                            // Create the attribute if it does not exist
                            lineWeightAttr = xmlDoc.CreateAttribute("LineWeight");
                            connectorNode.Attributes.Append(lineWeightAttr);
                        }
                        lineWeightAttr.Value = uniformLineWeight.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }

                    // Save the modified XML back to the SolutionXML element
                    solXml.XmlValue = xmlDoc.OuterXml;
                }

                // Save the modified diagram to the specified output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}