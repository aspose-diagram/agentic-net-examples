using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramBatchProcessor <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Build a quick lookup of shape ID → Shape.
                var shapeLookup = new Dictionary<long, Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    shapeLookup[shape.ID] = shape;
                }

                // Examine each connection definition on the page.
                foreach (Connect connect in page.Connects)
                {
                    // We are interested in the beginning point of a connector.
                    // The BeginX/BeginY cells indicate the source shape.
                    if (connect.FromCell != null && connect.FromCell.StartsWith("Begin"))
                    {
                        long connectorId = connect.FromSheet;   // The connector shape.
                        long sourceShapeId = connect.ToSheet;   // Shape that the connector starts from.

                        // Retrieve the connector and its source shape.
                        if (shapeLookup.TryGetValue(connectorId, out Shape connector) &&
                            shapeLookup.TryGetValue(sourceShapeId, out Shape sourceShape))
                        {
                            // Identify decision nodes by their master name.
                            if (sourceShape.Master != null && sourceShape.Master.Name == "Decision")
                            {
                                // Set the line jump style of the connector to Arc.
                                connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to '{outputPath}'.");
        }
    }