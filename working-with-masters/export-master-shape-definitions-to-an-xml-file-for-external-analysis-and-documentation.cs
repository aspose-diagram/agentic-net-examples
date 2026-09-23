using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Path for the exported XML file
                string outputPath = "masters.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(sourcePath);

                // Create an XmlWriter with indentation for readability
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineOnAttributes = false
                };

                using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Masters");

                    // Iterate through all masters in the diagram
                    foreach (Master master in diagram.Masters)
                    {
                        writer.WriteStartElement("Master");

                        // Basic master metadata
                        writer.WriteElementString("ID", master.ID.ToString());
                        writer.WriteElementString("Name", master.Name ?? string.Empty);
                        writer.WriteElementString("UniqueID", master.UniqueID.ToString());
                        writer.WriteElementString("Hidden", master.Hidden == BOOL.True ? "True" : "False");

                        // Optional additional properties (if needed)
                        writer.WriteElementString("BaseID", master.BaseID.ToString());
                        writer.WriteElementString("IconSize", master.IconSize.ToString());

                        // Export shapes contained in the master (only count here)
                        writer.WriteStartElement("Shapes");
                        writer.WriteAttributeString("Count", master.Shapes.Count.ToString());
                        writer.WriteEndElement(); // Shapes

                        writer.WriteEndElement(); // Master
                    }

                    writer.WriteEndElement(); // Masters
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Master definitions exported to '{Path.GetFullPath(outputPath)}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }