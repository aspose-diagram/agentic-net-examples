using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape to clone.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes to clone.");
                return;
            }

            // Retrieve the first shape on the page as the source shape.
            Shape sourceShape = null;
            foreach (Shape s in page.Shapes)
            {
                sourceShape = s;
                break;
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("Failed to retrieve a source shape.");
                return;
            }

            // Clone the shape by adding a new shape with the same master.
            // Position the duplicate with an offset (e.g., +2 inches on X and Y).
            double offsetX = 2.0;
            double offsetY = 2.0;
            double newPinX = sourceShape.XForm.PinX.Value + offsetX;
            double newPinY = sourceShape.XForm.PinY.Value + offsetY;

            // Add the new shape to the same page.
            long newShapeId = page.AddShape(newPinX, newPinY, sourceShape.Master.Name);
            Shape clonedShape = page.Shapes.GetShape(newShapeId);

            // Copy size from the source shape.
            clonedShape.XForm.Width.Value = sourceShape.XForm.Width.Value;
            clonedShape.XForm.Height.Value = sourceShape.XForm.Height.Value;

            // OPTIONAL: Copy additional formatting if needed (e.g., fill, line).
            // clonedShape.Fill.FillForegnd.Value = sourceShape.Fill.FillForegnd.Value;
            // clonedShape.Line.LineColor.Value = sourceShape.Line.LineColor.Value;

            // -----------------------------------------------------------------
            // Clone the corresponding shape node inside SolutionXML (if present).
            // -----------------------------------------------------------------
            if (diagram.SolutionXMLs.Count > 0)
            {
                // For demonstration, use the first SolutionXML element.
                SolutionXML solutionXml = diagram.SolutionXMLs[0];

                // Load the XML content.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(solutionXml.XmlValue);

                // Find the first <Shape> node.
                XmlNode shapeNode = xmlDoc.SelectSingleNode("//Shape");
                if (shapeNode != null)
                {
                    // Clone the node.
                    XmlNode clonedNode = shapeNode.CloneNode(true);

                    // Adjust position attributes if they exist.
                    XmlAttribute pinXAttr = clonedNode.Attributes["PinX"];
                    XmlAttribute pinYAttr = clonedNode.Attributes["PinY"];
                    if (pinXAttr != null && double.TryParse(pinXAttr.Value, out double pinX))
                    {
                        pinXAttr.Value = (pinX + offsetX).ToString();
                    }
                    if (pinYAttr != null && double.TryParse(pinYAttr.Value, out double pinY))
                    {
                        pinYAttr.Value = (pinY + offsetY).ToString();
                    }

                    // Append the cloned node to the same parent.
                    XmlNode parentNode = shapeNode.ParentNode;
                    if (parentNode != null)
                    {
                        parentNode.AppendChild(clonedNode);
                    }

                    // Save the modified XML back to the SolutionXML element.
                    solutionXml.XmlValue = xmlDoc.OuterXml;
                }
                else
                {
                    Console.Error.WriteLine("No <Shape> node found inside SolutionXML.");
                }
            }
            else
            {
                Console.Error.WriteLine("The diagram does not contain any SolutionXML elements.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}