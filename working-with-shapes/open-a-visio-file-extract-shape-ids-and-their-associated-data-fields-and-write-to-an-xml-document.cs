using System;
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
                string xmlOutputPath = "shapes.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create the root XML element
                XElement root = new XElement("Diagram");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create an XML element for each shape
                        XElement shapeElement = new XElement("Shape",
                            new XAttribute("ID", shape.ID),
                            new XAttribute("Data1", shape.Data1 ?? string.Empty),
                            new XAttribute("Data2", shape.Data2 ?? string.Empty),
                            new XAttribute("Data3", shape.Data3 ?? string.Empty)
                        );

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