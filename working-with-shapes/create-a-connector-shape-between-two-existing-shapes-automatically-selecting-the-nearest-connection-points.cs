using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Collect the first two non‑connector (2‑D) shapes on the page
            long shape1Id = 0, shape2Id = 0;
            int found = 0;
            foreach (Shape shp in page.Shapes)
            {
                // Skip 1‑D connector shapes
                if (shp.OneD) continue;

                if (found == 0) shape1Id = shp.ID;
                else if (found == 1) { shape2Id = shp.ID; break; }

                found++;
            }

            if (found < 2)
            {
                Console.Error.WriteLine("The page does not contain at least two non‑connector shapes.");
                return;
            }

            // Retrieve the shape objects for further calculations
            Shape shape1 = page.Shapes.GetShape(shape1Id);
            Shape shape2 = page.Shapes.GetShape(shape2Id);

            // Calculate shape centers using PinX/PinY (the shape's position)
            double x1 = shape1.XForm.PinX.Value;
            double y1 = shape1.XForm.PinY.Value;
            double x2 = shape2.XForm.PinX.Value;
            double y2 = shape2.XForm.PinY.Value;

            // Determine the nearest connection points based on relative positions
            ConnectionPointPlace connPlace1 = ConnectionPointPlace.Right;
            ConnectionPointPlace connPlace2 = ConnectionPointPlace.Left;

            // Simple heuristic: choose horizontal side that faces the other shape
            if (x2 < x1)
            {
                connPlace1 = ConnectionPointPlace.Left;
                connPlace2 = ConnectionPointPlace.Right;
            }

            // If the shapes are more vertically aligned, prefer top/bottom
            double dx = Math.Abs(x2 - x1);
            double dy = Math.Abs(y2 - y1);
            if (dy > dx)
            {
                if (y2 < y1)
                {
                    connPlace1 = ConnectionPointPlace.Top;
                    connPlace2 = ConnectionPointPlace.Bottom;
                }
                else
                {
                    connPlace1 = ConnectionPointPlace.Bottom;
                    connPlace2 = ConnectionPointPlace.Top;
                }
            }

            // Add a dynamic connector shape to the page
            // Using the overload that creates a shape with zero size at (0,0)
            long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);

            // Connect the two shapes via the newly created connector using the selected connection points
            page.ConnectShapesViaConnector(shape1Id, connPlace1, shape2Id, connPlace2, connectorId);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Connector added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}