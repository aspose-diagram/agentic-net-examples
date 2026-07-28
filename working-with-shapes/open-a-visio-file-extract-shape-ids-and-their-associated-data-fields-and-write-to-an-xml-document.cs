using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // Output XML file path
                string xmlOutputPath = "shapeData.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create the root element for the XML document
                XElement root = new XElement("DiagramData");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create an XML element for the shape
                        XElement shapeElement = new XElement("Shape",
                            new XAttribute("ID", shape.ID),
                            new XAttribute("Data1", shape.Data1 ?? string.Empty),
                            new XAttribute("Data2", shape.Data2 ?? string.Empty),
                            new XAttribute("Data3", shape.Data3 ?? string.Empty)
                        );

                        // Add the shape element to the root
                        root.Add(shapeElement);
                    }
                }

                // Build the XDocument and save to file
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                doc.Save(xmlOutputPath);

                Console.WriteLine($"Shape data exported to '{xmlOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }