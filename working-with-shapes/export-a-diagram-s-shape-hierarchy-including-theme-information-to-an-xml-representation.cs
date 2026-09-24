using System;
using Aspose.Diagram;
using System.Xml;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output XML file path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramExport <inputVisioFile> <outputXmlFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure XML writer for pretty output
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };

            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Diagram");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    writer.WriteStartElement("Page");
                    writer.WriteAttributeString("ID", page.ID.ToString());
                    writer.WriteAttributeString("Name", page.Name ?? string.Empty);

                    // Theme information is write‑only in the API; we cannot read it.
                    // Export a placeholder to indicate theme data is unavailable.
                    writer.WriteAttributeString("Theme", "Unavailable");

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        writer.WriteStartElement("Shape");
                        writer.WriteAttributeString("ID", shape.ID.ToString());
                        writer.WriteAttributeString("Name", shape.Name ?? string.Empty);
                        writer.WriteAttributeString("MasterName", shape.Master?.Name ?? "None");
                        writer.WriteAttributeString("Type", shape.Type.ToString());

                        // Parent shape relationship (for grouped shapes)
                        if (shape.ParentShape != null)
                        {
                            writer.WriteAttributeString("ParentID", shape.ParentShape.ID.ToString());
                        }

                        // Export custom properties (Props) as additional information
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            writer.WriteStartElement("CustomProperties");
                            foreach (Prop prop in shape.Props)
                            {
                                writer.WriteStartElement("Property");
                                writer.WriteAttributeString("Name", prop.Name ?? string.Empty);
                                writer.WriteAttributeString("Value", prop.Value?.Val ?? string.Empty);
                                writer.WriteEndElement(); // Property
                            }
                            writer.WriteEndElement(); // CustomProperties
                        }

                        writer.WriteEndElement(); // Shape
                    }

                    writer.WriteEndElement(); // Page
                }

                writer.WriteEndElement(); // Diagram
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Diagram hierarchy exported to '{outputPath}'.");
        }
    }