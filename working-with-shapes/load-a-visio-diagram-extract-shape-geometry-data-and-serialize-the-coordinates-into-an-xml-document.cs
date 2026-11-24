using System;
using System.IO;
using System.Xml.Linq;
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

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create the root XML element.
                XElement root = new XElement("Diagram");

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create an XML element for the shape, using its ID.
                        XElement shapeElement = new XElement("Shape",
                            new XAttribute("Id", shape.ID));

                        // Iterate through each geometry (Geom) of the shape.
                        foreach (Geom geom in shape.Geoms)
                        {
                            XElement geomElement = new XElement("Geometry");

                            // Iterate through each coordinate row in the geometry.
                            foreach (object coord in geom.CoordinateCol)
                            {
                                double? x = null;
                                double? y = null;

                                // Extract X and Y values from known coordinate types.
                                if (coord is MoveTo moveTo)
                                {
                                    x = moveTo.X.Value;
                                    y = moveTo.Y.Value;
                                }
                                else if (coord is LineTo lineTo)
                                {
                                    x = lineTo.X.Value;
                                    y = lineTo.Y.Value;
                                }
                                else if (coord is ArcTo arcTo)
                                {
                                    x = arcTo.X.Value;
                                    y = arcTo.Y.Value;
                                }
                                else if (coord is EllipticalArcTo ellArc)
                                {
                                    x = ellArc.X.Value;
                                    y = ellArc.Y.Value;
                                }
                                // Additional geometry types can be handled here if needed.

                                // If coordinates were found, add a Point element.
                                if (x.HasValue && y.HasValue)
                                {
                                    XElement point = new XElement("Point",
                                        new XAttribute("X", x.Value),
                                        new XAttribute("Y", y.Value));
                                    geomElement.Add(point);
                                }
                            }

                            shapeElement.Add(geomElement);
                        }

                        root.Add(shapeElement);
                    }
                }

                // Save the XML document to the specified output path.
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                doc.Save(outputPath);
                Console.WriteLine($"Geometry data exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}