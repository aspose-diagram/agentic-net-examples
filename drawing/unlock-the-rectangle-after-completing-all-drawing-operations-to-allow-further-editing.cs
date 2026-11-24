using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "UnlockedRectangle.vsdx";

        // Guard: ensure the directory for the output exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Draw a rectangle on the page (pinX, pinY, width, height)
                // This method returns the shape ID of the newly created rectangle
                long rectShapeId = page.DrawRectangle(1.0, 1.0, 2.0, 2.0);

                // Retrieve the shape object using the returned ID
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Ensure the shape is not marked as deleted before modifying
                if (rectShape.Del == BOOL.False)
                {
                    // Unlock the rectangle by clearing protection flags.
                    // Set each relevant protection property to FALSE.
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
                }
                else
                {
                    Console.Error.WriteLine("The rectangle shape was marked as deleted and cannot be unlocked.");
                }

                // Save the diagram to the specified file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}