using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (first argument) and output XML path (second argument)
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramExport <inputVisioFile> <outputXmlFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare XML writer settings
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };

            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Diagram");

                // Export style sheets (theme related information)
                writer.WriteStartElement("StyleSheets");
                foreach (StyleSheet styleSheet in diagram.StyleSheets)
                {
                    writer.WriteStartElement("StyleSheet");
                    writer.WriteAttributeString("ID", styleSheet.ID.ToString());
                    writer.WriteAttributeString("Name", styleSheet.Name ?? string.Empty);
                    writer.WriteEndElement(); // StyleSheet
                }
                writer.WriteEndElement(); // StyleSheets

                // Iterate through pages
                foreach (Page page in diagram.Pages)
                {
                    writer.WriteStartElement("Page");
                    writer.WriteAttributeString("ID", page.ID.ToString());
                    writer.WriteAttributeString("Name", page.Name ?? string.Empty);
                    writer.WriteAttributeString("NameU", page.NameU ?? string.Empty);

                    // Iterate through shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        writer.WriteStartElement("Shape");
                        writer.WriteAttributeString("ID", shape.ID.ToString());
                        writer.WriteAttributeString("Name", shape.Name ?? string.Empty);
                        writer.WriteAttributeString("NameU", shape.NameU ?? string.Empty);
                        writer.WriteAttributeString("Type", shape.Type.ToString());

                        // Master name (if any)
                        if (shape.Master != null)
                        {
                            writer.WriteAttributeString("MasterName", shape.Master.Name ?? string.Empty);
                        }

                        // Parent shape ID (if shape is part of a group)
                        if (shape.ParentShape != null)
                        {
                            writer.WriteAttributeString("ParentID", shape.ParentShape.ID.ToString());
                        }

                        // Custom properties (Props)
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            writer.WriteStartElement("Props");
                            foreach (Prop prop in shape.Props)
                            {
                                writer.WriteStartElement("Prop");
                                writer.WriteAttributeString("Name", prop.Name ?? string.Empty);
                                writer.WriteAttributeString("Label", prop.Label?.Value ?? string.Empty);
                                writer.WriteAttributeString("Value", prop.Value?.Val ?? string.Empty);
                                writer.WriteEndElement(); // Prop
                            }
                            writer.WriteEndElement(); // Props
                        }

                        // Hyperlinks (if any)
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            writer.WriteStartElement("Hyperlinks");
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                writer.WriteStartElement("Hyperlink");
                                writer.WriteAttributeString("Address", link.Address?.Value ?? string.Empty);
                                writer.WriteAttributeString("SubAddress", link.SubAddress?.Value ?? string.Empty);
                                writer.WriteAttributeString("Description", link.Description?.Value ?? string.Empty);
                                writer.WriteEndElement(); // Hyperlink
                            }
                            writer.WriteEndElement(); // Hyperlinks
                        }

                        writer.WriteEndElement(); // Shape
                    }

                    writer.WriteEndElement(); // Page
                }

                writer.WriteEndElement(); // Diagram
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Diagram hierarchy exported to XML file: {outputPath}");
        }
    }