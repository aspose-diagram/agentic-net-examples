using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // ------------------------------------------------------------
            // Locate the two shapes to be connected.
            // Attempt to find shapes named "Shape1" and "Shape2".
            // If not found, fall back to the first two shapes on the page.
            // ------------------------------------------------------------
            long shape1Id = -1;
            long shape2Id = -1;

            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == "Shape1")
                    shape1Id = s.ID;
                else if (s.NameU == "Shape2")
                    shape2Id = s.ID;
            }

            // Fallback: use the first two shapes if specific names were not found.
            if (shape1Id == -1 || shape2Id == -1)
            {
                int found = 0;
                foreach (Shape s in page.Shapes)
                {
                    if (found == 0) { shape1Id = s.ID; found++; }
                    else if (found == 1) { shape2Id = s.ID; found++; break; }
                }
            }

            // Validate that we have two distinct shape IDs.
            if (shape1Id == -1 || shape2Id == -1 || shape1Id == shape2Id)
            {
                Console.Error.WriteLine("Unable to locate two distinct shapes to connect.");
                return;
            }

            // Retrieve the shape objects for geometry calculations.
            Shape shape1 = page.Shapes.GetShape(shape1Id);
            Shape shape2 = page.Shapes.GetShape(shape2Id);

            // ------------------------------------------------------------
            // Determine the nearest connection points based on shape centers.
            // Use ConnectionPointPlace enum values: Right, Left, Top, Bottom.
            // ------------------------------------------------------------
            double pinX1 = shape1.XForm.PinX.Value;
            double pinY1 = shape1.XForm.PinY.Value;
            double pinX2 = shape2.XForm.PinX.Value;
            double pinY2 = shape2.XForm.PinY.Value;

            double deltaX = pinX2 - pinX1;
            double deltaY = pinY2 - pinY1;

            ConnectionPointPlace fromPlace;
            ConnectionPointPlace toPlace;

            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                // Horizontal dominance.
                if (deltaX > 0)
                {
                    fromPlace = ConnectionPointPlace.Right;
                    toPlace = ConnectionPointPlace.Left;
                }
                else
                {
                    fromPlace = ConnectionPointPlace.Left;
                    toPlace = ConnectionPointPlace.Right;
                }
            }
            else
            {
                // Vertical dominance.
                if (deltaY > 0)
                {
                    fromPlace = ConnectionPointPlace.Bottom;
                    toPlace = ConnectionPointPlace.Top;
                }
                else
                {
                    fromPlace = ConnectionPointPlace.Top;
                    toPlace = ConnectionPointPlace.Bottom;
                }
            }

            // ------------------------------------------------------------
            // Create a dynamic connector shape.
            // The AddShape overload expects (pinX, pinY, width, height, masterName, isCalculate).
            // For a connector, dimensions are not required; use zeros and set isCalculate to false.
            // ------------------------------------------------------------
            long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);

            // ------------------------------------------------------------
            // Connect the two shapes via the newly created connector,
            // using the automatically selected nearest connection points.
            // ------------------------------------------------------------
            page.ConnectShapesViaConnector(shape1Id, fromPlace, shape2Id, toPlace, connectorId);

            // ------------------------------------------------------------
            // Save the modified diagram to the specified output path.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}