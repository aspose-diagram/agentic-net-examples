using System;
using System.IO;
using Aspose.Diagram;

public class ShapeGrouper
{
    /// <summary>
    /// Ensures that the shape with the specified ID on the given page is part of a group.
    /// If the shape is not already grouped, it creates a new group containing only this shape.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram Diagram instance.</param>
    /// <param name="pageIndex">Zero‑based index of the page containing the shape.</param>
    /// <param name="shapeId">The unique ID of the shape to check.</param>
    public void EnsureShapeIsGrouped(Diagram diagram, int pageIndex, long shapeId)
    {
        // Get the target page.
        Page page = diagram.Pages[pageIndex];

        // Retrieve the shape by its ID.
        Shape targetShape = page.Shapes.GetShape(shapeId);

        // If the shape is already in a group, nothing to do.
        if (targetShape.IsInGroup())
        {
            // Shape is already grouped.
            return;
        }

        // The shape is not grouped; create a new group containing this shape.
        // ShapeCollection.Group expects an array of Shape objects.
        Shape[] shapesToGroup = new Shape[] { targetShape };

        // Group the shapes. The method returns the newly created group shape.
        Shape groupShape = page.Shapes.Group(shapesToGroup);

        // Optional: configure group properties (e.g., SelectMode) if needed.
        // groupShape.Group.SelectMode = SelectModeValue.GroupShapeOnly;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new ShapeGrouper();
            obj.EnsureShapeIsGrouped(null, 0, 0);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
