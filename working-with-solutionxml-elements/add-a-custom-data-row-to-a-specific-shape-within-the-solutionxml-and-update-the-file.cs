using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command line or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first shape on the first page as the target shape
                Shape targetShape = null;
                Page firstPage = diagram.Pages[0];
                foreach (Shape s in firstPage.Shapes)
                {
                    targetShape = s;
                    break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No shapes found on the first page.");
                    return;
                }

                // Store the shape's ID for later use in the XML
                long shapeId = targetShape.ID;

                // Define the name of the SolutionXML element that will hold our custom data
                const string solutionXmlName = "CustomShapeData";

                // Try to locate an existing SolutionXML element with the specified name
                SolutionXML targetSolutionXml = null;
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (solXml.Name == solutionXmlName)
                    {
                        targetSolutionXml = solXml;
                        break;
                    }
                }

                // If it does not exist, create a new one and add it to the collection
                if (targetSolutionXml == null)
                {
                    targetSolutionXml = new SolutionXML();
                    targetSolutionXml.Name = solutionXmlName;
                    // Initialise with an empty root element
                    targetSolutionXml.XmlValue = "<Root></Root>";
                    diagram.SolutionXMLs.Add(targetSolutionXml);
                }

                // Load the XML content from the SolutionXML element
                XmlDocument xmlDoc = new XmlDocument();
                if (!string.IsNullOrEmpty(targetSolutionXml.XmlValue))
                {
                    xmlDoc.LoadXml(targetSolutionXml.XmlValue);
                }
                else
                {
                    // Fallback to a basic root if the stored XML is empty
                    xmlDoc.LoadXml("<Root></Root>");
                }

                // Ensure the root element exists
                XmlElement root = xmlDoc.DocumentElement;
                if (root == null)
                {
                    root = xmlDoc.CreateElement("Root");
                    xmlDoc.AppendChild(root);
                }

                // Locate (or create) the <Shape> node that corresponds to our target shape ID
                string xpath = $"//Shape[@ID='{shapeId}']";
                XmlNode shapeNode = xmlDoc.SelectSingleNode(xpath);
                if (shapeNode == null)
                {
                    XmlElement newShapeElem = xmlDoc.CreateElement("Shape");
                    newShapeElem.SetAttribute("ID", shapeId.ToString());
                    root.AppendChild(newShapeElem);
                    shapeNode = newShapeElem;
                }

                // Create a custom data element and attach it to the shape node
                XmlElement customData = xmlDoc.CreateElement("CustomData");
                customData.SetAttribute("Name", "MyCustomRow");
                customData.InnerText = "MyValue";
                shapeNode.AppendChild(customData);

                // Write the modified XML back to the SolutionXML element
                targetSolutionXml.XmlValue = xmlDoc.OuterXml;

                // Save the updated diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}