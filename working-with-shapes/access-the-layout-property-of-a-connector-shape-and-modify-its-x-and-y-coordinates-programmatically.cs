using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <newPinX> <newPinY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the new X and Y coordinates.
        if (!double.TryParse(args[2], out double newPinX) || !double.TryParse(args[3], out double newPinY))
        {
            Console.Error.WriteLine("Error: Invalid coordinate values. Provide numeric values for PinX and PinY.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to find a connector shape.
            bool connectorFound = false;
            foreach (Page page in diagram.Pages)
            {
                // ShapeCollection does not support direct indexing by long ID; use GetShape.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify a dynamic connector: it is a 1‑D shape and its master name contains "Dynamic connector".
                    if (shape.OneD && shape.Master != null && shape.Master.Name != null &&
                        shape.Master.Name.IndexOf("Dynamic connector", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Access the Layout property (required by the task) – although Layout does not hold position,
                        // we demonstrate accessing it before modifying coordinates.
                        var layout = shape.Layout;

                        // Modify the connector's position via its XForm (PinX and PinY).
                        shape.XForm.PinX.Value = newPinX;
                        shape.XForm.PinY.Value = newPinY;

                        // Optionally, you could modify a Layout property here, e.g., routing style.
                        // shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                        connectorFound = true;
                        Console.WriteLine($"Connector shape (ID={shape.ID}) position updated to PinX={newPinX}, PinY={newPinY}.");
                        break; // Assuming only one connector needs to be updated.
                    }
                }

                if (connectorFound) break;
            }

            if (!connectorFound)
            {
                Console.Error.WriteLine("Warning: No dynamic connector shape was found in the diagram.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}