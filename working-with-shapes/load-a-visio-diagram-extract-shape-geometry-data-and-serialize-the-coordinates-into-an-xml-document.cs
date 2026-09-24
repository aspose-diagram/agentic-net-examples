using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (change as needed or pass via command line)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            // Output XML file path
            string outputPath = args.Length > 1 ? args[1] : "geometry.xml";

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare XML document
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Diagram");
            xmlDoc.AppendChild(root);

            // Iterate through pages
            foreach (Page page in diagram.Pages)
            {
                XmlElement pageElem = xmlDoc.CreateElement("Page");
                pageElem.SetAttribute("Name", page.NameU ?? string.Empty);
                root.AppendChild(pageElem);

                // Iterate through shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    XmlElement shapeElem = xmlDoc.CreateElement("Shape");
                    shapeElem.SetAttribute("ID", shape.ID.ToString());
                    shapeElem.SetAttribute("Name", shape.NameU ?? string.Empty);
                    pageElem.AppendChild(shapeElem);

                    XmlElement geomElem = xmlDoc.CreateElement("Geometry");
                    shapeElem.AppendChild(geomElem);

                    // Collect all coordinate points from the shape's geometry sections
                    List<(double X, double Y)> points = new List<(double, double)>();

                    foreach (Geom geom in shape.Geoms)
                    {
                        foreach (var row in geom.CoordinateCol)
                        {
                            double x = 0, y = 0;
                            switch (row)
                            {
                                case MoveTo move:
                                    x = move.X.Value;
                                    y = move.Y.Value;
                                    break;
                                case LineTo line:
                                    x = line.X.Value;
                                    y = line.Y.Value;
                                    break;
                                case ArcTo arc:
                                    x = arc.X.Value;
                                    y = arc.Y.Value;
                                    break;
                                case EllipticalArcTo ell:
                                    x = ell.X.Value;
                                    y = ell.Y.Value;
                                    break;
                                case SplineKnot knot:
                                    x = knot.X.Value;
                                    y = knot.Y.Value;
                                    break;
                                // Add other geometry row types if needed
                            }
                            points.Add((x, y));
                        }
                    }

                    // Serialize points to XML
                    foreach (var pt in points)
                    {
                        XmlElement pointElem = xmlDoc.CreateElement("Point");
                        pointElem.SetAttribute("X", pt.X.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
                        pointElem.SetAttribute("Y", pt.Y.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
                        geomElem.AppendChild(pointElem);
                    }
                }
            }

            // Save the XML document
            xmlDoc.Save(outputPath);
            Console.WriteLine($"Geometry data exported to: {outputPath}");
        }
    }