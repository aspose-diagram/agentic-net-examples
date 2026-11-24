using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputDiagramPath> <outputDiagramPath> <shapeId> <customRowName> <customRowValue>
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <shapeId> <customRowName> <customRowValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string customRowName = args[3];
        string customRowValue = args[4];

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the shape with the specified ID across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    Shape shape = page.Shapes.GetShape(targetShapeId);
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {targetShapeId} not found in the diagram.");
                    return;
                }

                // Determine the SolutionXML element to modify (by name). Use "CustomData" as default.
                const string solutionXmlName = "CustomData";
                SolutionXML solutionXml = null;
                foreach (SolutionXML sxml in diagram.SolutionXMLs)
                {
                    if (sxml.Name == solutionXmlName)
                    {
                        solutionXml = sxml;
                        break;
                    }
                }

                // If not found, create a new SolutionXML element and add it to the collection
                if (solutionXml == null)
                {
                    solutionXml = new SolutionXML();
                    solutionXml.Name = solutionXmlName;
                    // Initialize with an empty root element
                    solutionXml.XmlValue = new XDocument(new XElement("SolutionXML")).ToString();
                    diagram.SolutionXMLs.Add(solutionXml);
                }

                // Load existing XML content
                XDocument xmlDoc;
                if (string.IsNullOrWhiteSpace(solutionXml.XmlValue))
                {
                    xmlDoc = new XDocument(new XElement("SolutionXML"));
                }
                else
                {
                    xmlDoc = XDocument.Parse(solutionXml.XmlValue);
                }

                // Create a new custom data row element
                XElement customRow = new XElement("CustomDataRow",
                    new XAttribute("ShapeId", targetShapeId),
                    new XAttribute("Name", customRowName),
                    new XAttribute("Value", customRowValue));

                // Append the new row to the root element
                xmlDoc.Root.Add(customRow);

                // Update the SolutionXML's XML value
                solutionXml.XmlValue = xmlDoc.ToString();

                // Save the updated diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}