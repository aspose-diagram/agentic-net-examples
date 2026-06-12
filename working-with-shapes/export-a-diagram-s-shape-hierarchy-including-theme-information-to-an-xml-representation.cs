using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class DiagramShapeHierarchyExporter
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputVisioPath = "input.vsdx";

            // Output XML file path
            string outputXmlPath = "shape_hierarchy.xml";

            // Load the Visio diagram using the provided constructor
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Configure XML writer for pretty output
                XmlWriterSettings xmlSettings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  "
                };

                // Create the XML file and start writing
                using (XmlWriter writer = XmlWriter.Create(outputXmlPath, xmlSettings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Diagram");

                    // Export theme information (StyleSheets collection)
                    writer.WriteStartElement("StyleSheets");
                    foreach (StyleSheet styleSheet in diagram.StyleSheets)
                    {
                        writer.WriteStartElement("StyleSheet");
                        writer.WriteAttributeString("Name", styleSheet.Name);
                        writer.WriteEndElement(); // StyleSheet
                    }
                    writer.WriteEndElement(); // StyleSheets

                    // Export each page and its shape hierarchy
                    writer.WriteStartElement("Pages");
                    foreach (Page page in diagram.Pages)
                    {
                        writer.WriteStartElement("Page");
                        writer.WriteAttributeString("ID", page.ID.ToString());
                        writer.WriteAttributeString("Name", page.Name);

                        writer.WriteStartElement("Shapes");
                        foreach (Shape shape in page.Shapes)
                        {
                            WriteShapeRecursive(writer, shape);
                        }
                        writer.WriteEndElement(); // Shapes

                        writer.WriteEndElement(); // Page
                    }
                    writer.WriteEndElement(); // Pages

                    writer.WriteEndElement(); // Diagram
                    writer.WriteEndDocument();
                }
            }

            Console.WriteLine("Shape hierarchy exported to: " + Path.GetFullPath(outputXmlPath));

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Recursively writes a shape and its sub‑shapes (if any) to the XML writer
    private static void WriteShapeRecursive(XmlWriter writer, Shape shape)
    {
        writer.WriteStartElement("Shape");
        writer.WriteAttributeString("ID", shape.ID.ToString());
        writer.WriteAttributeString("Name", shape.Name ?? string.Empty);

        // Master (template) information, if the shape is based on a master
        if (shape.Master != null)
        {
            writer.WriteAttributeString("MasterName", shape.Master.Name ?? string.Empty);
        }

        // Example of additional properties you might want to include
        writer.WriteAttributeString("PinX", shape.XForm.PinX.ToString());
        writer.WriteAttributeString("PinY", shape.XForm.PinY.ToString());
        writer.WriteAttributeString("Width", shape.XForm.Width.ToString());
        writer.WriteAttributeString("Height", shape.XForm.Height.ToString());

        // If the shape is a group, it contains sub‑shapes
        if (shape.Shapes != null && shape.Shapes.Count > 0)
        {
            writer.WriteStartElement("SubShapes");
            foreach (Shape subShape in shape.Shapes)
            {
                WriteShapeRecursive(writer, subShape);
            }
            writer.WriteEndElement(); // SubShapes
        }

        writer.WriteEndElement(); // Shape
    }
}
