using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputXmlPath>");
            return;
        }

        string visioPath = args[0];
        string xmlPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Prepare XML writer settings.
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineOnAttributes = false
                };

                // Create the XML document.
                using (XmlWriter writer = XmlWriter.Create(xmlPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Diagram");

                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        writer.WriteStartElement("Page");

                        // Write page name if available.
                        if (!string.IsNullOrEmpty(page.NameU))
                            writer.WriteAttributeString("name", page.NameU);

                        // Iterate through each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            writer.WriteStartElement("Shape");
                            writer.WriteAttributeString("id", shape.ID.ToString());

                            // Iterate through each custom property (Prop) of the shape.
                            foreach (Prop prop in shape.Props)
                            {
                                writer.WriteStartElement("Prop");
                                writer.WriteAttributeString("name", prop.Name ?? string.Empty);
                                writer.WriteAttributeString("value", prop.Value?.Val ?? string.Empty);
                                writer.WriteEndElement(); // </Prop>
                            }

                            writer.WriteEndElement(); // </Shape>
                        }

                        writer.WriteEndElement(); // </Page>
                    }

                    writer.WriteEndElement(); // </Diagram>
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Successfully exported shape data to XML file: {xmlPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram or I/O operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}