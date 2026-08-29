using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram that contains a group shape.
            // Replace the path with the actual file location.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume the diagram has at least one page.
            Page page = diagram.Pages[0];

            // Find the first group shape on the page.
            Shape groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupShape = shape;
                    break;
                }
            }

            if (groupShape == null)
            {
                Console.WriteLine("No group shape found on the first page.");
                return;
            }

            // Ensure the group has child shapes.
            if (groupShape.Shapes == null || groupShape.Shapes.Count == 0)
            {
                Console.WriteLine("The group shape does not contain any child shapes.");
                return;
            }

            // Initialize bounding box extremes.
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            // Iterate over each child shape within the group.
            foreach (Shape child in groupShape.Shapes)
            {
                // Retrieve absolute PinX and PinY values.
                double pinX = child.XForm.PinX.Value;
                double pinY = child.XForm.PinY.Value;

                // Update bounding box limits.
                if (pinX < minX) minX = pinX;
                if (pinY < minY) minY = pinY;
                if (pinX > maxX) maxX = pinX;
                if (pinY > maxY) maxY = pinY;
            }

            // Calculate width and height of the bounding box.
            double boundingWidth = maxX - minX;
            double boundingHeight = maxY - minY;

            // Output the results.
            Console.WriteLine($"Bounding Box for Group Shape (ID: {groupShape.ID}):");
            Console.WriteLine($"Min PinX: {minX}");
            Console.WriteLine($"Min PinY: {minY}");
            Console.WriteLine($"Max PinX: {maxX}");
            Console.WriteLine($"Max PinY: {maxY}");
            Console.WriteLine($"Width : {boundingWidth}");
            Console.WriteLine($"Height: {boundingHeight}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
