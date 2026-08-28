using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class VerifyEllipseInSvg
{
    static void Main()
    {
        // Define ellipse (circle) parameters
        double pinX = 5.0;   // X coordinate of the shape's pin (center of rotation)
        double pinY = 5.0;   // Y coordinate of the shape's pin (center of rotation)
        double diameter = 4.0; // Width and height (circle)

        // Create a new diagram
        Diagram diagram = new Diagram();

        // Draw an ellipse (circle) on the first page
        // DrawEllipse returns the shape ID, which we ignore here
        diagram.Pages[0].DrawEllipse(pinX, pinY, diameter, diameter);

        // Export the diagram to SVG
        string svgPath = "circle.svg";
        diagram.Save(svgPath, SaveFileFormat.Svg);

        // Load the generated SVG for verification
        XmlDocument svgDoc = new XmlDocument();
        svgDoc.Load(svgPath);

        // Find all <ellipse> elements
        XmlNodeList ellipseNodes = svgDoc.GetElementsByTagName("ellipse");

        if (ellipseNodes.Count == 0)
        {
            Console.WriteLine("Verification failed: No <ellipse> element found in the SVG.");
            return;
        }

        // Verify that at least one ellipse has equal radii (i.e., a circle)
        const double tolerance = 0.0001;
        bool circleFound = false;

        foreach (XmlNode node in ellipseNodes)
        {
            // Extract radius attributes (rx, ry)
            double rx = double.Parse(node.Attributes["rx"].Value);
            double ry = double.Parse(node.Attributes["ry"].Value);

            // Check if radii are effectively equal
            if (Math.Abs(rx - ry) <= tolerance)
            {
                // Optionally verify position matches the drawn parameters
                double cx = double.Parse(node.Attributes["cx"].Value);
                double cy = double.Parse(node.Attributes["cy"].Value);

                // The SVG coordinates may differ due to internal scaling,
                // but we can at least ensure the center is close to the pin point.
                if (Math.Abs(cx - pinX) <= tolerance && Math.Abs(cy - pinY) <= tolerance)
                {
                    circleFound = true;
                    break;
                }
            }
        }

        if (circleFound)
            Console.WriteLine("Verification succeeded: SVG contains a correct <ellipse> element representing the drawn circle.");
        else
            Console.WriteLine("Verification failed: No matching <ellipse> element with equal radii found.");
    }
}
