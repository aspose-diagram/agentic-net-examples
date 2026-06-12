using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the specific SolutionXML element by its Name
            SolutionXML targetSolutionXml = null;
            foreach (SolutionXML s in diagram.SolutionXMLs)
            {
                if (s.Name == "MySolutionXML")
                {
                    targetSolutionXml = s;
                    break;
                }
            }

            if (targetSolutionXml == null)
            {
                Console.WriteLine("SolutionXML with the specified name was not found.");
                return;
            }

            // Parse the XML stored in the SolutionXML element
            XDocument xmlDoc = XDocument.Parse(targetSolutionXml.XmlValue);

            // Find the first <Shape> node (adjust the query as needed for your XML structure)
            XElement originalShapeNode = xmlDoc.Descendants("Shape").FirstOrDefault();
            if (originalShapeNode == null)
            {
                Console.WriteLine("No <Shape> element found in the SolutionXML.");
                return;
            }

            // Clone the shape node
            XElement clonedShapeNode = new XElement(originalShapeNode);

            // Adjust the position attributes (PinX and PinY) of the cloned node
            double originalPinX = double.Parse(originalShapeNode.Attribute("PinX")?.Value ?? "0");
            double originalPinY = double.Parse(originalShapeNode.Attribute("PinY")?.Value ?? "0");

            // Example offset: move the duplicate 1 inch to the right and 1 inch down
            clonedShapeNode.SetAttributeValue("PinX", originalPinX + 1.0);
            clonedShapeNode.SetAttributeValue("PinY", originalPinY + 1.0);

            // Insert the cloned node after the original node in the XML hierarchy
            originalShapeNode.AddAfterSelf(clonedShapeNode);

            // Write the modified XML back to the SolutionXML element
            targetSolutionXml.XmlValue = xmlDoc.ToString();

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Shape node cloned and repositioned successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
