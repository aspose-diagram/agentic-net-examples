using System.IO;
using System;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Uniform line weight to apply (in inches)
    const double UniformLineWeight = 0.02;

    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";   // TODO: replace with actual file path
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each SolutionXML element
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                    continue;

                // Parse the XML stored in the SolutionXML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(solXml.XmlValue);

                // Assume connector IDs are stored in elements named <Connector Id="123"/>
                XmlNodeList connectorNodes = xmlDoc.SelectNodes("//Connector[@Id]");
                if (connectorNodes == null)
                    continue;

                foreach (XmlNode node in connectorNodes)
                {
                    if (long.TryParse(node.Attributes["Id"]?.Value, out long connectorId))
                    {
                        Shape connectorShape = FindShapeById(diagram, connectorId);
                        if (connectorShape != null && connectorShape.OneD)
                        {
                            // Set the line weight uniformly
                            connectorShape.Line.LineWeight.Value = UniformLineWeight;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx"; // TODO: replace with desired output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to locate a shape by its ID across all pages
    private static Shape FindShapeById(Diagram diagram, long shapeId)
    {
        foreach (Page page in diagram.Pages)
        {
            try
            {
                // GetShape throws if the ID is not present on the page
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                    return shape;
            }
            catch
            {
                // Ignore and continue searching other pages
            }
        }
        return null;
    }
}
