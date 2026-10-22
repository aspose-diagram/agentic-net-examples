using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <groupShapeId>");
            return;
        }

        string diagramPath = args[0];
        string groupIdArg = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the group shape ID
        if (!long.TryParse(groupIdArg, out long groupShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid group shape ID - {groupIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Use the first page of the diagram (you can change this as needed)
                Page page = diagram.Pages[0];

                // Retrieve the shape that is supposed to be a group
                Shape groupShape = page.Shapes.GetShape(groupShapeId);

                // Verify that the retrieved shape is a group
                // A shape is a group if its Type is Group or it contains child shapes
                if (groupShape.Type != TypeValue.Group && groupShape.Shapes.Count == 0)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {groupShapeId} is not a group.");
                    return;
                }

                // Initialize bounding‑box extremes
                double minX = double.MaxValue;
                double minY = double.MaxValue;
                double maxX = double.MinValue;
                double maxY = double.MinValue;

                // Iterate through each child shape inside the group
                foreach (Shape child in groupShape.Shapes)
                {
                    // Skip logically deleted shapes
                    if (child.Del == BOOL.True)
                        continue;

                    // Calculate the absolute PinX/Y of the child shape
                    double absolutePinX = groupShape.XForm.PinX.Value
                                          + child.XForm.PinX.Value
                                          - groupShape.XForm.LocPinX.Value;

                    double absolutePinY = groupShape.XForm.PinY.Value
                                          + child.XForm.PinY.Value
                                          - groupShape.XForm.LocPinY.Value;

                    // Retrieve child dimensions
                    double width = child.XForm.Width.Value;
                    double height = child.XForm.Height.Value;

                    // Compute the child's extents
                    double left   = absolutePinX - width / 2.0;
                    double right  = absolutePinX + width / 2.0;
                    double bottom = absolutePinY - height / 2.0;
                    double top    = absolutePinY + height / 2.0;

                    // Update the overall bounding box
                    if (left   < minX) minX = left;
                    if (right  > maxX) maxX = right;
                    if (bottom < minY) minY = bottom;
                    if (top    > maxY) maxY = top;
                }

                // If no child shapes were processed, report and exit
                if (minX == double.MaxValue)
                {
                    Console.WriteLine("The group contains no visible child shapes.");
                    return;
                }

                // Output the calculated bounding box
                Console.WriteLine("Bounding Box of Group Shape:");
                Console.WriteLine($"Left   (Min X): {minX}");
                Console.WriteLine($"Bottom (Min Y): {minY}");
                Console.WriteLine($"Right  (Max X): {maxX}");
                Console.WriteLine($"Top    (Max Y): {maxY}");
                Console.WriteLine($"Width  : {maxX - minX}");
                Console.WriteLine($"Height : {maxY - minY}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
