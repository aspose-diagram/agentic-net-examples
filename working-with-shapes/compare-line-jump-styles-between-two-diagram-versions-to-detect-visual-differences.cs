using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the two Visio diagram files to compare
            string diagramPath1 = "DiagramVersion1.vsdx";
            string diagramPath2 = "DiagramVersion2.vsdx";

            // Load the diagrams
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Collect connector line jump styles from the first diagram
            var connectorStyles1 = new Dictionary<(int pageId, long shapeId), ConLineJumpStyleValue>();
            foreach (Page page in diagram1.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only 1‑D shapes are connectors
                    if (shape.OneD)
                    {
                        // Retrieve the line jump style; default to PageDefault if not set
                        ConLineJumpStyleValue style = shape.Layout.ConLineJumpStyle.Value;
                        connectorStyles1[(page.ID, shape.ID)] = style;
                    }
                }
            }

            // Collect connector line jump styles from the second diagram
            var connectorStyles2 = new Dictionary<(int pageId, long shapeId), ConLineJumpStyleValue>();
            foreach (Page page in diagram2.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD)
                    {
                        ConLineJumpStyleValue style = shape.Layout.ConLineJumpStyle.Value;
                        connectorStyles2[(page.ID, shape.ID)] = style;
                    }
                }
            }

            bool differencesFound = false;

            // Compare connectors present in the first diagram against the second
            foreach (var kvp in connectorStyles1)
            {
                var key = kvp.Key;
                ConLineJumpStyleValue style1 = kvp.Value;

                if (connectorStyles2.TryGetValue(key, out ConLineJumpStyleValue style2))
                {
                    if (style1 != style2)
                    {
                        differencesFound = true;
                        Console.WriteLine($"Difference in connector (Page ID: {key.pageId}, Shape ID: {key.shapeId}):");
                        Console.WriteLine($"  Diagram 1 style: {style1}");
                        Console.WriteLine($"  Diagram 2 style: {style2}");
                    }
                }
                else
                {
                    differencesFound = true;
                    Console.WriteLine($"Connector (Page ID: {key.pageId}, Shape ID: {key.shapeId}) exists in Diagram 1 but not in Diagram 2.");
                }
            }

            // Check for connectors that exist only in the second diagram
            foreach (var kvp in connectorStyles2)
            {
                var key = kvp.Key;
                if (!connectorStyles1.ContainsKey(key))
                {
                    differencesFound = true;
                    Console.WriteLine($"Connector (Page ID: {key.pageId}, Shape ID: {key.shapeId}) exists in Diagram 2 but not in Diagram 1.");
                }
            }

            if (!differencesFound)
            {
                Console.WriteLine("No line jump style differences detected between the two diagrams.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
