using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a single page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Define the original shape dimensions and position
            double startPinX = 2.0;   // X coordinate of the shape's center
            double startPinY = 2.0;   // Y coordinate of the shape's center
            double shapeWidth = 1.0;  // Width of the rectangle
            double shapeHeight = 0.5; // Height of the rectangle

            // Add the original rectangle shape
            long originalShapeId = page.DrawRectangle(startPinX, startPinY, shapeWidth, shapeHeight);
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            originalShape.Text.Value.Add(new Txt("Original"));

            // Parameters for duplication
            int duplicateCount = 5;          // Number of copies to create
            double offsetX = 1.0;            // Horizontal offset per copy
            double offsetY = 0.5;            // Vertical offset per copy

            // Create duplicates with incremental offsets
            for (int i = 1; i <= duplicateCount; i++)
            {
                double newPinX = startPinX + i * offsetX;
                double newPinY = startPinY + i * offsetY;

                // Add a new rectangle at the calculated position
                long dupShapeId = page.DrawRectangle(newPinX, newPinY, shapeWidth, shapeHeight);
                Shape dupShape = page.Shapes.GetShape(dupShapeId);
                dupShape.Text.Value.Add(new Txt($"Copy {i}"));
            }

            // Save the diagram to a VSDX file
            diagram.Save("DuplicatedShapes.vsdx", SaveFileFormat.Vsdx);
        }
    }