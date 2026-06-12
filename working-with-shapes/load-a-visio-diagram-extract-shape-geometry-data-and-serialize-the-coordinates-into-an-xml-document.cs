using System;
using System.IO;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string visioPath = "input.vsdx";

            // Path where the resulting XML will be saved
            string xmlOutputPath = "shapes.xml";

            // Load the Visio diagram using the Diagram(string) constructor (load rule)
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Create the root element for the XML document
                XElement root = new XElement("Shapes");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Extract basic geometry information from the shape's XForm
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Build an XML element for the shape
                        XElement shapeElement = new XElement("Shape",
                            new XAttribute("ID", shape.ID),
                            new XAttribute("Name", shape.NameU ?? shape.Name),
                            new XElement("Geometry",
                                new XElement("PinX", pinX),
                                new XElement("PinY", pinY),
                                new XElement("Width", width),
                                new XElement("Height", height)
                            )
                        );

                        // Attempt to extract detailed Geom coordinates if they exist
                        if (shape.Geoms != null && shape.Geoms.Count > 0)
                        {
                            XElement geomsElement = new XElement("Geoms");

                            foreach (Geom geom in shape.Geoms)
                            {
                                // Some Geom objects expose an XY collection; use reflection to stay safe
                                var xyProp = geom.GetType().GetProperty("XY");
                                if (xyProp != null)
                                {
                                    var xyCollection = xyProp.GetValue(geom) as System.Collections.IEnumerable;
                                    if (xyCollection != null)
                                    {
                                        XElement geomElement = new XElement("Geom");

                                        foreach (var xy in xyCollection)
                                        {
                                            // Assume each XY item has X and Y properties
                                            var xProp = xy.GetType().GetProperty("X");
                                            var yProp = xy.GetType().GetProperty("Y");
                                            if (xProp != null && yProp != null)
                                            {
                                                double x = Convert.ToDouble(xProp.GetValue(xy));
                                                double y = Convert.ToDouble(yProp.GetValue(xy));
                                                geomElement.Add(new XElement("Point",
                                                    new XAttribute("X", x),
                                                    new XAttribute("Y", y)));
                                            }
                                        }

                                        geomsElement.Add(geomElement);
                                    }
                                }
                            }

                            shapeElement.Add(geomsElement);
                        }

                        root.Add(shapeElement);
                    }
                }

                // Create the final XML document and save it
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xmlDoc.Save(xmlOutputPath);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
