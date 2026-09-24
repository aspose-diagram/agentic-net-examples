using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Assume the group shape is on the first page and we know its ID
                // Replace this ID with the actual group shape ID you want to process
                long groupShapeId = 1; // example ID

                // Retrieve the group shape
                Shape groupShape = diagram.Pages[0].Shapes.GetShape(groupShapeId);

                if (groupShape == null)
                {
                    Console.WriteLine($"Group shape with ID {groupShapeId} not found.");
                    return;
                }

                // Verify that the shape is a group
                if (groupShape.Type != TypeValue.Group)
                {
                    Console.WriteLine($"Shape ID {groupShapeId} is not a group shape.");
                    return;
                }

                // Get group's absolute center position
                double groupPinX = groupShape.XForm.PinX.Value;
                double groupPinY = groupShape.XForm.PinY.Value;

                // Initialize bounding box extremes
                double minX = double.MaxValue;
                double minY = double.MaxValue;
                double maxX = double.MinValue;
                double maxY = double.MinValue;

                // Iterate over child shapes within the group
                foreach (Shape child in groupShape.Shapes)
                {
                    // Child's position is relative to the group's center
                    double childPinX = child.XForm.PinX.Value;
                    double childPinY = child.XForm.PinY.Value;
                    double childWidth = child.XForm.Width.Value;
                    double childHeight = child.XForm.Height.Value;

                    // Compute absolute center coordinates
                    double absPinX = groupPinX + childPinX;
                    double absPinY = groupPinY + childPinY;

                    // Compute child's bounding edges
                    double left   = absPinX - (childWidth / 2.0);
                    double right  = absPinX + (childWidth / 2.0);
                    double top    = absPinY - (childHeight / 2.0);
                    double bottom = absPinY + (childHeight / 2.0);

                    // Update overall bounding box
                    if (left   < minX) minX = left;
                    if (right  > maxX) maxX = right;
                    if (top    < minY) minY = top;
                    if (bottom > maxY) maxY = bottom;
                }

                // Output the calculated bounding box
                Console.WriteLine("Bounding Box of Group Shape:");
                Console.WriteLine($"Left   (Min X): {minX}");
                Console.WriteLine($"Right  (Max X): {maxX}");
                Console.WriteLine($"Top    (Min Y): {minY}");
                Console.WriteLine($"Bottom (Max Y): {maxY}");
                Console.WriteLine($"Width  : {maxX - minX}");
                Console.WriteLine($"Height : {maxY - minY}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }