using System;
using System.IO;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string visioFilePath = "input.vsdx";

            // Load the Visio diagram using the constructor that accepts a file path
            Diagram diagram = new Diagram(visioFilePath);

            // Create the root element for the resulting XML
            XElement rootElement = new XElement("Shapes");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve basic geometry data from the shape's XForm element
                    double pinX = shape.XForm?.PinX?.Value ?? 0;
                    double pinY = shape.XForm?.PinY?.Value ?? 0;
                    double width = shape.XForm?.Width?.Value ?? 0;
                    double height = shape.XForm?.Height?.Value ?? 0;

                    // Build an XML element representing the shape and its coordinates
                    XElement shapeElement = new XElement("Shape",
                        new XAttribute("ID", shape.ID),
                        new XAttribute("Name", shape.NameU ?? shape.Name ?? string.Empty),
                        new XElement("PinX", pinX),
                        new XElement("PinY", pinY),
                        new XElement("Width", width),
                        new XElement("Height", height)
                    );

                    // Add the shape element to the root
                    rootElement.Add(shapeElement);
                }
            }

            // Assemble the final XML document
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                rootElement
            );

            // Save the XML document to a file
            string outputXmlPath = "shapes.xml";
            xmlDoc.Save(outputXmlPath);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
