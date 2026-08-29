using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output XML file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.xml";

        // Load the Visio diagram inside a try/catch to capture any Aspose errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Create the root XML element for the diagram
        XElement root = new XElement("Diagram");

        // Iterate over each page in the diagram (explicit type, no var)
        foreach (Page page in diagram.Pages)
        {
            // Create an XML element for the page, using its universal name if available
            string pageName = !string.IsNullOrEmpty(page.NameU) ? page.NameU : page.Name;
            XElement pageElement = new XElement("Page", new XAttribute("Name", pageName));

            // Iterate over each shape on the page (explicit type)
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True) continue;

                // Create an XML element for the shape with its ID attribute
                XElement shapeElement = new XElement("Shape", new XAttribute("ID", shape.ID));

                // Iterate over each geometry section of the shape
                foreach (Geom geom in shape.Geoms)
                {
                    // Iterate over each coordinate command within the geometry (object because collection is non‑generic)
                    foreach (object coord in geom.CoordinateCol)
                    {
                        // Determine the command type and extract X/Y values
                        if (coord is MoveTo moveTo)
                        {
                            // MoveTo command: starting point of a path
                            XElement moveElem = new XElement("MoveTo",
                                new XAttribute("X", moveTo.X.Value),
                                new XAttribute("Y", moveTo.Y.Value));
                            shapeElement.Add(moveElem);
                        }
                        else if (coord is LineTo lineTo)
                        {
                            // LineTo command: straight line segment
                            XElement lineElem = new XElement("LineTo",
                                new XAttribute("X", lineTo.X.Value),
                                new XAttribute("Y", lineTo.Y.Value));
                            shapeElement.Add(lineElem);
                        }
                        else if (coord is ArcTo arcTo)
                        {
                            // ArcTo command: elliptical arc segment
                            XElement arcElem = new XElement("ArcTo",
                                new XAttribute("X", arcTo.X.Value),
                                new XAttribute("Y", arcTo.Y.Value));
                            shapeElement.Add(arcElem);
                        }
                        else if (coord is EllipticalArcTo ellArc)
                        {
                            // EllipticalArcTo command: more detailed arc
                            XElement ellArcElem = new XElement("EllipticalArcTo",
                                new XAttribute("X", ellArc.X.Value),
                                new XAttribute("Y", ellArc.Y.Value));
                            shapeElement.Add(ellArcElem);
                        }
                        else if (coord is SplineKnot splineKnot)
                        {
                            // SplineKnot command: spline control point
                            XElement splineElem = new XElement("SplineKnot",
                                new XAttribute("X", splineKnot.X.Value),
                                new XAttribute("Y", splineKnot.Y.Value));
                            shapeElement.Add(splineElem);
                        }
                        // Additional geometry types can be added here following the same pattern
                    }
                }

                // Add the completed shape element to the page element
                pageElement.Add(shapeElement);
            }

            // Add the completed page element to the root diagram element
            root.Add(pageElement);
        }

        // Build the XDocument and attempt to save it, handling any I/O errors
        XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        try
        {
            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the XML document to the specified path
            xmlDoc.Save(outputPath);
            Console.WriteLine($"Geometry data successfully exported to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing XML file: {ex.Message}");
        }
    }
}