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
                string xmlPath = "output.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create the root XML element
                XElement root = new XElement("Diagram");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Create an XML element for the page
                    XElement pageElement = new XElement("Page",
                        new XAttribute("Name", page.NameU ?? string.Empty));

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create an XML element for the shape with its ID
                        XElement shapeElement = new XElement("Shape",
                            new XAttribute("ID", shape.ID));

                        // Add Data1 if present
                        if (!string.IsNullOrEmpty(shape.Data1))
                            shapeElement.Add(new XElement("Data1", shape.Data1));

                        // Add Data2 if present
                        if (!string.IsNullOrEmpty(shape.Data2))
                            shapeElement.Add(new XElement("Data2", shape.Data2));

                        // Add Data3 if present
                        if (!string.IsNullOrEmpty(shape.Data3))
                            shapeElement.Add(new XElement("Data3", shape.Data3));

                        // Append the shape element to the page element
                        pageElement.Add(shapeElement);
                    }

                    // Append the page element to the root
                    root.Add(pageElement);
                }

                // Build the XDocument and save to file
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                doc.Save(xmlPath);

                Console.WriteLine($"Shape data exported to '{xmlPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }