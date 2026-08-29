using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two diagram files to compare
                string diagramPath1 = "DiagramVersion1.vsdx";
                string diagramPath2 = "DiagramVersion2.vsdx";

                // Load the diagrams
                Diagram diagram1 = new Diagram(diagramPath1);
                Diagram diagram2 = new Diagram(diagramPath2);

                // Build dictionaries of connector ID -> line jump style for each diagram
                var jumps1 = GetConnectorJumpStyles(diagram1);
                var jumps2 = GetConnectorJumpStyles(diagram2);

                bool differencesFound = false;

                // Compare connectors present in both diagrams
                foreach (var kvp in jumps1)
                {
                    long shapeId = kvp.Key;
                    ConLineJumpStyleValue style1 = kvp.Value;

                    if (jumps2.TryGetValue(shapeId, out ConLineJumpStyleValue style2))
                    {
                        if (style1 != style2)
                        {
                            differencesFound = true;
                            Console.WriteLine($"Connector ID {shapeId} has different line jump styles:");
                            Console.WriteLine($"  Diagram 1: {style1}");
                            Console.WriteLine($"  Diagram 2: {style2}");
                        }
                    }
                    else
                    {
                        differencesFound = true;
                        Console.WriteLine($"Connector ID {shapeId} exists in Diagram 1 but not in Diagram 2.");
                    }
                }

                // Find connectors that exist only in Diagram 2
                foreach (var kvp in jumps2)
                {
                    long shapeId = kvp.Key;
                    if (!jumps1.ContainsKey(shapeId))
                    {
                        differencesFound = true;
                        Console.WriteLine($"Connector ID {shapeId} exists in Diagram 2 but not in Diagram 1.");
                    }
                }

                if (!differencesFound)
                {
                    Console.WriteLine("No line jump style differences detected between the two diagrams.");
                }

                // Optionally, save a report or the diagrams (not required for comparison)

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Retrieves a mapping of connector shape IDs to their line jump style values
        private static Dictionary<long, ConLineJumpStyleValue> GetConnectorJumpStyles(Diagram diagram)
        {
            var result = new Dictionary<long, ConLineJumpStyleValue>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only consider 1-D shapes (connectors)
                    if (shape.OneD)
                    {
                        // Access the line jump style via the Layout sub-object
                        ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;
                        result[shape.ID] = jumpStyle;
                    }
                }
            }

            return result;
        }
    }