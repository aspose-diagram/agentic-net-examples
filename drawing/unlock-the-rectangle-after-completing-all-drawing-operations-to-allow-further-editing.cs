using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram (the first page is created by default)
            // Use the first page for drawing
            Page page = diagram.Pages[0];

            // Define rectangle parameters (position and size in inches)
            double pinX = 2.0;   // X coordinate of the rectangle's pin
            double pinY = 2.0;   // Y coordinate of the rectangle's pin
            double width = 3.0;  // Width of the rectangle
            double height = 2.0; // Height of the rectangle

            // Draw the rectangle on the page; DrawRectangle returns the shape ID (long)
            long rectShapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the shape object using the returned ID
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Unlock the rectangle by clearing all protection flags
            // Each protection property uses the BOOL enum; set to BOOL.False
            rectShape.Protection.LockAspect.Value = BOOL.False;
            rectShape.Protection.LockBegin.Value = BOOL.False;
            rectShape.Protection.LockCalcWH.Value = BOOL.False;
            rectShape.Protection.LockCrop.Value = BOOL.False;
            rectShape.Protection.LockCustProp.Value = BOOL.False;
            rectShape.Protection.LockDelete.Value = BOOL.False;
            rectShape.Protection.LockEnd.Value = BOOL.False;
            rectShape.Protection.LockFormat.Value = BOOL.False;
            rectShape.Protection.LockFromGroupFormat.Value = BOOL.False;
            rectShape.Protection.LockGroup.Value = BOOL.False;
            rectShape.Protection.LockHeight.Value = BOOL.False;
            rectShape.Protection.LockMoveX.Value = BOOL.False;
            rectShape.Protection.LockMoveY.Value = BOOL.False;
            rectShape.Protection.LockRotate.Value = BOOL.False;
            rectShape.Protection.LockSelect.Value = BOOL.False;
            rectShape.Protection.LockTextEdit.Value = BOOL.False;
            rectShape.Protection.LockThemeColors.Value = BOOL.False;
            rectShape.Protection.LockThemeEffects.Value = BOOL.False;
            rectShape.Protection.LockVtxEdit.Value = BOOL.False;
            rectShape.Protection.LockWidth.Value = BOOL.False;

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}