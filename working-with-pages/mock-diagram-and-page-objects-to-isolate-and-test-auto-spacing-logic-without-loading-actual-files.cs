using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        // Create an empty diagram (no file loading)
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Add three rectangle shapes to the page
        // Parameters: pinX, pinY, width, height (all in inches)
        long id1 = page.DrawRectangle(1, 1, 2, 1);
        long id2 = page.DrawRectangle(4, 1, 2, 1);
        long id3 = page.DrawRectangle(7, 1, 2, 1);

        // Retrieve the shape objects by their IDs
        Shape shape1 = page.Shapes.GetShape(id1);
        Shape shape2 = page.Shapes.GetShape(id2);
        Shape shape3 = page.Shapes.GetShape(id3);

        // Display initial positions
        Console.WriteLine("Before AutoSpace:");
        PrintShapeInfo(shape1);
        PrintShapeInfo(shape2);
        PrintShapeInfo(shape3);

        // Configure auto‑spacing options
        AutoSpaceOptions options = new AutoSpaceOptions
        {
            DistanceInHorizontal = 2, // horizontal gap in inches
            DistanceInVertical = 1    // vertical gap in inches
        };

        // Apply auto‑spacing to all shapes on the page
        page.AutoSpaceShapes(page.Shapes, options);

        // Display positions after auto‑spacing
        Console.WriteLine("\nAfter AutoSpace:");
        PrintShapeInfo(shape1);
        PrintShapeInfo(shape2);
        PrintShapeInfo(shape3);
    }

    // Helper method to output shape ID and its PinX/PinY coordinates
    static void PrintShapeInfo(Shape shape)
    {
        Console.WriteLine($"Shape ID: {shape.ID}, PinX: {shape.XForm.PinX.Value}, PinY: {shape.XForm.PinY.Value}");
    }
}
