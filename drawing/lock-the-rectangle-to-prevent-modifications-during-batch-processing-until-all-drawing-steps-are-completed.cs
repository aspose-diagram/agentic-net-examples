using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new diagram
            Diagram diagram = new Diagram();

            // Access the first page (default page)
            Page page = diagram.Pages[0];

            // Define rectangle parameters (position and size in inches)
            double pinX = 5.0;   // X coordinate of the rectangle's center
            double pinY = 5.0;   // Y coordinate of the rectangle's center
            double width = 3.0;  // Width of the rectangle
            double height = 2.0; // Height of the rectangle

            // Draw the rectangle on the page; returns the shape ID (long)
            long rectShapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape((int)rectShapeId);

            // -------------------------------------------------
            // Lock the rectangle to prevent any modifications
            // during batch processing.
            // -------------------------------------------------
            rectShape.Protection.LockMoveX.Value = BOOL.True;
            rectShape.Protection.LockMoveY.Value = BOOL.True;
            rectShape.Protection.LockWidth.Value = BOOL.True;
            rectShape.Protection.LockHeight.Value = BOOL.True;
            rectShape.Protection.LockRotate.Value = BOOL.True;
            rectShape.Protection.LockVtxEdit.Value = BOOL.True;

            // -------------------------------------------------
            // ----- Begin batch processing (placeholder) -----
            // -------------------------------------------------
            // Perform any drawing steps, shape additions, etc.
            // The rectangle above will remain locked throughout.
            // Example: add another shape (a circle) without affecting the rectangle.
            double circlePinX = 8.0;
            double circlePinY = 5.0;
            double circleRadius = 1.0;
            page.DrawEllipse(circlePinX, circlePinY, circleRadius * 2, circleRadius * 2);
            // -------------------------------------------------
            // ----- End batch processing --------------------
            // -------------------------------------------------

            // Unlock the rectangle now that all drawing steps are completed
            rectShape.Protection.LockMoveX.Value = BOOL.False;
            rectShape.Protection.LockMoveY.Value = BOOL.False;
            rectShape.Protection.LockWidth.Value = BOOL.False;
            rectShape.Protection.LockHeight.Value = BOOL.False;
            rectShape.Protection.LockRotate.Value = BOOL.False;
            rectShape.Protection.LockVtxEdit.Value = BOOL.False;

            // Save the diagram to a VSDX file
            string outputPath = "LockedRectangleDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }