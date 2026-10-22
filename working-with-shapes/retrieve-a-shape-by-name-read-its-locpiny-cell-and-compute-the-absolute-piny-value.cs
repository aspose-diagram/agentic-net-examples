using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and shape name (NameU)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the shape by its universal name (NameU)
            Shape targetShape = null;
            Page targetPage = null;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeName)
                    {
                        targetShape = shape;
                        targetPage = page;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with NameU '{targetShapeName}' not found.");
                return;
            }

            // Read the LocPinY cell value
            double locPinY = targetShape.XForm.LocPinY.Value;

            // Compute the absolute PinY value, taking group hierarchy into account
            double absolutePinY = ComputeAbsolutePinY(targetShape, targetPage);

            // Output the results
            Console.WriteLine($"Shape NameU: {targetShapeName}");
            Console.WriteLine($"LocPinY (local): {locPinY}");
            Console.WriteLine($"Absolute PinY: {absolutePinY}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively computes the absolute PinY coordinate of a shape,
    /// accounting for any parent group transformations.
    /// </summary>
    /// <param name="shape">The shape whose absolute PinY is required.</param>
    /// <param name="page">The page containing the shape.</param>
    /// <returns>Absolute PinY value.</returns>
    private static double ComputeAbsolutePinY(Shape shape, Page page)
    {
        // Base case: shape is not inside any group
        Shape parentGroup = FindParentGroup(shape, page);
        if (parentGroup == null)
        {
            // No parent group, absolute PinY is the shape's own PinY
            return shape.XForm.PinY.Value;
        }

        // Recursively compute the parent's absolute PinY
        double parentAbsPinY = ComputeAbsolutePinY(parentGroup, page);

        // Apply the formula: absolute = parentAbsPinY + child.PinY - parent.LocPinY
        double absolute = parentAbsPinY + shape.XForm.PinY.Value - parentGroup.XForm.LocPinY.Value;
        return absolute;
    }

    /// <summary>
    /// Searches for a group shape that directly contains the specified shape.
    /// Returns null if the shape is not a child of any group.
    /// </summary>
    /// <param name="shape">The shape to find a parent for.</param>
    /// <param name="page">The page to search within.</param>
    /// <returns>The immediate parent group shape, or null.</returns>
    private static Shape FindParentGroup(Shape shape, Page page)
    {
        foreach (Shape possibleGroup in page.Shapes)
        {
            // Identify group shapes by their TypeValue
            if (possibleGroup.Type == TypeValue.Group)
            {
                // Iterate over child shapes of the group
                foreach (Shape child in possibleGroup.Shapes)
                {
                    if (child.ID == shape.ID)
                    {
                        return possibleGroup;
                    }
                }
            }
        }
        // No containing group found
        return null;
    }
}