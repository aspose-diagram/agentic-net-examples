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
        if (string.IsNullOrWhiteSpace(diagramPath) || !File.Exists(diagramPath))
        {
            Console.Error.WriteLine("Diagram file not found.");
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
                var (pinX, pinY) = GetAbsolutePin(diagram, shapeId);

                // Output the results
                Console.WriteLine($"Shape ID {shapeId} absolute PinX: {pinX}");
                Console.WriteLine($"Shape ID {shapeId} absolute PinY: {pinY}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns the absolute PinX and PinY coordinates of a shape identified by its ID.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <param name="shapeId">The numeric ID of the shape.</param>
    /// <returns>Tuple containing absolute PinX and PinY.</returns>
    private static (double PinX, double PinY) GetAbsolutePin(Diagram diagram, long shapeId)
    {
        // Iterate through all pages to locate the shape
        foreach (Page page in diagram.Pages)
        {
            Shape shape = null;
            try
            {
                // Attempt to retrieve the shape directly from the page
                shape = page.Shapes.GetShape(shapeId);
            }
            catch
            {
                // If not found on this page, continue searching
            }

            if (shape != null)
            {
                // Compute absolute coordinates, taking group nesting into account
                return ComputeAbsolute(shape, page);
            }
        }

        // Shape not found in any page
        throw new Exception($"Shape with ID {shapeId} not found in the diagram.");
    }

    /// <summary>
    /// Recursively computes the absolute PinX and PinY for a shape, handling group nesting.
    /// </summary>
    /// <param name="shape">The shape whose coordinates are being calculated.</param>
    /// <param name="page">The page containing the shape.</param>
    /// <returns>Tuple of absolute PinX and PinY.</returns>
    private static (double PinX, double PinY) ComputeAbsolute(Shape shape, Page page)
    {
        // Search for a parent group that contains this shape
        foreach (Shape potentialGroup in page.Shapes)
        {
            if (potentialGroup.Type == TypeValue.Group)
            {
                foreach (Shape child in potentialGroup.Shapes)
                {
                    if (child.ID == shape.ID)
                    {
                        // Recursively compute the group's absolute position
                        var parentAbs = ComputeAbsolute(potentialGroup, page);

                        // Apply the formula to convert local to absolute coordinates
                        double absX = parentAbs.PinX + shape.XForm.PinX.Value - potentialGroup.XForm.LocPinX.Value;
                        double absY = parentAbs.PinY + shape.XForm.PinY.Value - potentialGroup.XForm.LocPinY.Value;

                        return (absX, absY);
                    }
                }
            }
        }

        // Shape is not inside any group; its PinX/Y are already absolute
        return (shape.XForm.PinX.Value, shape.XForm.PinY.Value);
    }
}