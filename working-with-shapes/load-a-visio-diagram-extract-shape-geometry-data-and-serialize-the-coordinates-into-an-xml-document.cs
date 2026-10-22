using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputXmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure XML writer for pretty‑printed output
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineOnAttributes = false
            };

            // Create the XML document
            using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Diagram");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    writer.WriteStartElement("Page");
                    writer.WriteAttributeString("ID", page.ID.ToString());
                    writer.WriteAttributeString("NameU", page.NameU ?? string.Empty);

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted (hidden) shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        writer.WriteStartElement("Shape");
                        writer.WriteAttributeString("ID", shape.ID.ToString());
                        writer.WriteAttributeString("NameU", shape.NameU ?? string.Empty);
                        writer.WriteAttributeString("Type", shape.Type.ToString());

                        // Ensure the shape has geometry (Geoms) data
                        if (shape.Geoms != null && shape.Geoms.Count > 0)
                        {
                            // Iterate through each geometry (Geom) of the shape
                            for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                            {
                                Geom geom = shape.Geoms[geomIdx];
                                writer.WriteStartElement("Geom");
                                writer.WriteAttributeString("Index", geomIdx.ToString());

                                // Iterate through each coordinate within the geometry
                                if (geom.CoordinateCol != null && geom.CoordinateCol.Count > 0)
                                {
                                    for (int coordIdx = 0; coordIdx < geom.CoordinateCol.Count; coordIdx++)
                                    {
                                        // Use dynamic to access X and Y of the coordinate (MoveTo, LineTo, etc.)
                                        dynamic coord = geom.CoordinateCol[coordIdx];
                                        writer.WriteStartElement("Coordinate");
                                        writer.WriteAttributeString("Index", coordIdx.ToString());

                                        // Write X and Y values
                                        writer.WriteElementString("X", coord.X.Value.ToString());
                                        writer.WriteElementString("Y", coord.Y.Value.ToString());

                                        writer.WriteEndElement(); // Coordinate
                                    }
                                }

                                writer.WriteEndElement(); // Geom
                            }
                        }

                        writer.WriteEndElement(); // Shape
                    }

                    writer.WriteEndElement(); // Page
                }

                writer.WriteEndElement(); // Diagram
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Geometry data successfully exported to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}