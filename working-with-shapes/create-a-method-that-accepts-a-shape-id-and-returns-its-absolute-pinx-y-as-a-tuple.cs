using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for diagram file path
        Console.Write("Enter the path to the Visio diagram file: ");
        string diagramPath = Console.ReadLine();

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Prompt for shape ID
        Console.Write("Enter the shape ID (numeric): ");
        string idInput = Console.ReadLine();
        if (!long.TryParse(idInput, out long shapeId))
        {
            Console.Error.WriteLine("Invalid shape ID.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve absolute PinX and PinY for the specified shape
                var (absPinX, absPinY) = GetAbsolutePin(diagram, shapeId);

                // Output the results
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"Absolute PinX: {absPinX}");
                Console.WriteLine($"Absolute PinY: {absPinY}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculates the absolute PinX and PinY coordinates of a shape,
    /// taking into account any parent group transformations.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <param name="shapeId">The ID of the shape whose absolute coordinates are required.</param>
    /// <returns>A tuple containing absolute PinX and PinY values.</returns>
    static (double PinX, double PinY) GetAbsolutePin(Diagram diagram, long shapeId)
    {
        // Start with the active page (assumes shape resides on the active page)
        Page page = diagram.ActivePage;

        // Retrieve the shape; if not found, throw an informative exception
        Shape shape = page.Shapes.GetShape(shapeId);
        if (shape == null)
            throw new Exception($"Shape with ID {shapeId} not found on the active page.");

        // Initialize absolute coordinates with the shape's own PinX/Y
        double absPinX = shape.XForm.PinX.Value;
        double absPinY = shape.XForm.PinY.Value;

        // Walk up the group hierarchy, if any
        Shape currentShape = shape;
        while (true)
        {
            // Find a parent group that contains the current shape
            Shape parentGroup = FindParentGroup(page, currentShape);
            if (parentGroup == null)
                break; // No further parent groups; absolute coordinates are resolved

            // Adjust absolute coordinates based on the parent group's transformation
            // Formula: absolute = parent.Pin - parent.LocPin + childAbsolute
            absPinX = parentGroup.XForm.PinX.Value - parentGroup.XForm.LocPinX.Value + absPinX;
            absPinY = parentGroup.XForm.PinY.Value - parentGroup.XForm.LocPinY.Value + absPinY;

            // Continue climbing up the hierarchy
            currentShape = parentGroup;
        }

        return (absPinX, absPinY);
    }

    /// <summary>
    /// Searches the page for a group shape that directly contains the specified shape.
    /// </summary>
    /// <param name="page">The page to search.</param>
    /// <param name="child">The shape whose parent group is sought.</param>
    /// <returns>The parent group shape if found; otherwise, null.</returns>
    static Shape FindParentGroup(Page page, Shape child)
    {
        foreach (Shape candidate in page.Shapes)
        {
            // Only consider group shapes
            if (candidate.Type == TypeValue.Group)
            {
                // Iterate over the group's child shapes
                foreach (Shape grpChild in candidate.Shapes)
                {
                    if (grpChild.ID == child.ID)
                        return candidate;
                }
            }
        }
        return null; // No parent group found
    }
}