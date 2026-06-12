using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class VisioShapeExtractor
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string visioPath = "input.vsdx";

            // Path where the resulting XML will be saved
            string xmlOutputPath = "shapes.xml";

            // Load the Visio diagram using the Diagram constructor (load rule)
            Diagram diagram = new Diagram(visioPath);

            // Create an XmlWriter for efficient XML generation
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(xmlOutputPath, settings))
            {
                // Start the root element
                writer.WriteStartDocument();
                writer.WriteStartElement("Shapes");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Write a Shape element with its ID attribute
                        writer.WriteStartElement("Shape");
                        writer.WriteAttributeString("ID", shape.ID.ToString());

                        // Write Data1, Data2, Data3 elements if they contain values
                        if (!string.IsNullOrEmpty(shape.Data1))
                        {
                            writer.WriteElementString("Data1", shape.Data1);
                        }

                        if (!string.IsNullOrEmpty(shape.Data2))
                        {
                            writer.WriteElementString("Data2", shape.Data2);
                        }

                        if (!string.IsNullOrEmpty(shape.Data3))
                        {
                            writer.WriteElementString("Data3", shape.Data3);
                        }

                        // Close the Shape element
                        writer.WriteEndElement();
                    }
                }

                // Close the root element
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // Optional: inform the user that the process completed
            Console.WriteLine($"Shape data extracted to '{xmlOutputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
