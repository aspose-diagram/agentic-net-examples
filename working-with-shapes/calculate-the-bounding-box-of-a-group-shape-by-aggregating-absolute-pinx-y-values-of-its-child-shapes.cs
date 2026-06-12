using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the group shape (replace "Group1" with the actual group name or ID)
            Shape groupShape = diagram.Pages[0].Shapes.GetShapeIncludingChild("Group1");

            // Collection of child shapes inside the group
            ShapeCollection childShapes = groupShape.Shapes;

            // Initialize bounding box extremes
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            // Iterate through each child shape to compute absolute extents
            foreach (Shape child in childShapes)
            {
                // Center coordinates of the child shape
                double pinX = child.XForm.PinX.Value;
                double pinY = child.XForm.PinY.Value;

                // Dimensions of the child shape
                double width = child.XForm.Width.Value;
                double height = child.XForm.Height.Value;

                // Calculate left, right, top, and bottom edges
                double left   = pinX - width / 2.0;
                double right  = pinX + width / 2.0;
                double bottom = pinY - height / 2.0;
                double top    = pinY + height / 2.0;

                // Update bounding box extremes
                if (left   < minX) minX = left;
                if (right  > maxX) maxX = right;
                if (bottom < minY) minY = bottom;
                if (top    > maxY) maxY = top;
            }

            // Derive the group's new bounding box from the extremes
            double groupPinX = (minX + maxX) / 2.0;
            double groupPinY = (minY + maxY) / 2.0;
            double groupWidth = maxX - minX;
            double groupHeight = maxY - minY;

            // Apply the calculated bounding box to the group shape
            groupShape.XForm.PinX.Value = groupPinX;
            groupShape.XForm.PinY.Value = groupPinY;
            groupShape.XForm.Width.Value = groupWidth;
            groupShape.XForm.Height.Value = groupHeight;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
