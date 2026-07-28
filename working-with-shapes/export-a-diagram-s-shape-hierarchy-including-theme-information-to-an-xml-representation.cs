using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output XML file path
                string outputPath = "diagram_hierarchy.xml";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Prepare XML writer settings for readability
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true,
                        IndentChars = "  ",
                        NewLineOnAttributes = false
                    };

                    using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Diagram");

                        // Export pages and their shapes
                        writer.WriteStartElement("Pages");
                        foreach (Page page in diagram.Pages)
                        {
                            writer.WriteStartElement("Page");
                            writer.WriteAttributeString("ID", page.ID.ToString());
                            writer.WriteAttributeString("Name", page.Name ?? string.Empty);
                            writer.WriteAttributeString("NameU", page.NameU ?? string.Empty);

                            // Theme information (if set previously, cannot be read back; placeholder)
                            // Since PresetTheme properties are write‑only, we include a placeholder node.
                            writer.WriteStartElement("ThemeInfo");
                            writer.WriteAttributeString("PresetTheme", "NotReadable");
                            writer.WriteAttributeString("PresetThemeVariant", "NotReadable");
                            writer.WriteEndElement(); // ThemeInfo

                            // Shapes within the page
                            writer.WriteStartElement("Shapes");
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                writer.WriteStartElement("Shape");
                                writer.WriteAttributeString("ID", shape.ID.ToString());
                                writer.WriteAttributeString("Name", shape.Name ?? string.Empty);
                                writer.WriteAttributeString("NameU", shape.NameU ?? string.Empty);
                                writer.WriteAttributeString("MasterName", shape.Master?.Name ?? string.Empty);
                                writer.WriteAttributeString("Type", shape.Type.ToString());

                                // Position and size
                                writer.WriteStartElement("Geometry");
                                writer.WriteElementString("PinX", shape.XForm.PinX.Value.ToString());
                                writer.WriteElementString("PinY", shape.XForm.PinY.Value.ToString());
                                writer.WriteElementString("Width", shape.XForm.Width.Value.ToString());
                                writer.WriteElementString("Height", shape.XForm.Height.Value.ToString());
                                writer.WriteEndElement(); // Geometry

                                // Custom properties (Props)
                                if (shape.Props != null && shape.Props.Count > 0)
                                {
                                    writer.WriteStartElement("CustomProperties");
                                    foreach (Prop prop in shape.Props)
                                    {
                                        writer.WriteStartElement("Property");
                                        writer.WriteAttributeString("Name", prop.Name ?? string.Empty);
                                        writer.WriteAttributeString("Label", prop.Label?.Value ?? string.Empty);
                                        writer.WriteAttributeString("Value", prop.Value?.Val ?? string.Empty);
                                        writer.WriteEndElement(); // Property
                                    }
                                    writer.WriteEndElement(); // CustomProperties
                                }

                                // Hyperlinks
                                if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                                {
                                    writer.WriteStartElement("Hyperlinks");
                                    foreach (Hyperlink link in shape.Hyperlinks)
                                    {
                                        writer.WriteStartElement("Hyperlink");
                                        writer.WriteElementString("Address", link.Address?.Value ?? string.Empty);
                                        writer.WriteElementString("SubAddress", link.SubAddress?.Value ?? string.Empty);
                                        writer.WriteElementString("Description", link.Description?.Value ?? string.Empty);
                                        writer.WriteEndElement(); // Hyperlink
                                    }
                                    writer.WriteEndElement(); // Hyperlinks
                                }

                                writer.WriteEndElement(); // Shape
                            }
                            writer.WriteEndElement(); // Shapes
                            writer.WriteEndElement(); // Page
                        }
                        writer.WriteEndElement(); // Pages

                        // Export style sheets (theme related information)
                        writer.WriteStartElement("StyleSheets");
                        foreach (StyleSheet ss in diagram.StyleSheets)
                        {
                            writer.WriteStartElement("StyleSheet");
                            writer.WriteAttributeString("ID", ss.ID.ToString());
                            writer.WriteAttributeString("Name", ss.Name ?? string.Empty);
                            writer.WriteEndElement(); // StyleSheet
                        }
                        writer.WriteEndElement(); // StyleSheets

                        writer.WriteEndElement(); // Diagram
                        writer.WriteEndDocument();
                    }
                }

                Console.WriteLine($"Diagram hierarchy exported to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }