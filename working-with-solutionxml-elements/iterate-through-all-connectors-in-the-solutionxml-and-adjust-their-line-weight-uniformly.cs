using System;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Desired uniform line weight (in inches)
                double uniformWeight = 0.02;

                // Iterate through each SolutionXML element stored in the diagram
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // Parse the XML content of the SolutionXML
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                    // Example: assume connector definitions are stored in <Connector Id="123"/> elements
                    foreach (XElement connectorElem in xDoc.Descendants("Connector"))
                    {
                        // Try to read the connector shape ID
                        XAttribute idAttr = connectorElem.Attribute("Id");
                        if (idAttr == null) continue;

                        if (!long.TryParse(idAttr.Value, out long shapeId)) continue;

                        // Find the shape with this ID across all pages
                        Shape connectorShape = FindShapeById(diagram, shapeId);
                        if (connectorShape == null) continue;

                        // Ensure the shape is a connector (1‑D shape)
                        if (!connectorShape.OneD) continue;

                        // Set the line weight uniformly
                        connectorShape.Line.LineWeight.Value = uniformWeight;
                    }
                }

                // Save the modified diagram
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
                // GetShape returns null if the ID is not present on this page
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                    return shape;
            }
            return null;
        }
    }