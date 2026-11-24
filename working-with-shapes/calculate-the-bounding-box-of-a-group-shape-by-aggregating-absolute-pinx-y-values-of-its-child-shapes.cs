using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <groupShapeId>");
            return;
        }

        string diagramPath = args[0];
        string groupIdArg = args[1];

        // Guard: check diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse group shape ID
        if (!long.TryParse(groupIdArg, out long groupShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid group shape ID '{groupIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by API)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Use the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the group shape by its ID
                Shape groupShape = page.Shapes.GetShape(groupShapeId);
                if (groupShape == null)
                {
                    Console.Error.WriteLine($"Error: No shape found with ID {groupShapeId}.");
                    return;
                }

                // Verify that the shape is a group (TypeValue.Group)
                if (groupShape.Type != TypeValue.Group)
                {
                    Console.Error.WriteLine($"Error: Shape ID {groupShapeId} is not a group shape.");
                    return;
                }

                // Initialize bounding box extremes
                double minX = double.MaxValue;
                double minY = double.MaxValue;
                double maxX = double.MinValue;
                double maxY = double.MinValue;

                // Iterate through each child shape within the group
                foreach (Shape child in groupShape.Shapes)
                {
                    // Skip logically deleted shapes
                    if (child.Del == BOOL.True)
                        continue;

                    // Calculate absolute PinX/Y for the child shape
                    double absolutePinX = groupShape.XForm.PinX.Value
                                          + child.XForm.PinX.Value
                                          - groupShape.XForm.LocPinX.Value;

                    double absolutePinY = groupShape.XForm.PinY.Value
                                          + child.XForm.PinY.Value
                                          - groupShape.XForm.LocPinY.Value;

                    // Retrieve half dimensions (width/height are in inches)
                    double halfWidth = child.XForm.Width.Value / 2.0;
                    double halfHeight = child.XForm.Height.Value / 2.0;

                    // Determine the child's bounding extents
                    double left   = absolutePinX - halfWidth;
                    double right  = absolutePinX + halfWidth;
                    double top    = absolutePinY - halfHeight;
                    double bottom = absolutePinY + halfHeight;

                    // Update overall bounding box
                    if (left   < minX) minX = left;
                    if (right  > maxX) maxX = right;
                    if (top    < minY) minY = top;
                    if (bottom > maxY) maxY = bottom;
                }

                // Check if any child shapes were processed
                if (minX == double.MaxValue)
                {
                    Console.WriteLine("The group shape contains no visible child shapes.");
                    return;
                }

                // Compute final width and height of the bounding box
                double boundingWidth  = maxX - minX;
                double boundingHeight = maxY - minY;

                // Output the results
                Console.WriteLine($"Bounding Box of Group Shape (ID {groupShapeId}):");
                Console.WriteLine($"Left   : {minX}");
                Console.WriteLine($"Top    : {minY}");
                Console.WriteLine($"Right  : {maxX}");
                Console.WriteLine($"Bottom : {maxY}");
                Console.WriteLine($"Width  : {boundingWidth}");
                Console.WriteLine($"Height : {boundingHeight}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or runtime errors
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}
