using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        // Guard: ensure we can write to the directory
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

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isConnector
            long rectShapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", false);
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Lock the rectangle to prevent modifications during batch processing
            rectShape.Protection.LockMoveX.Value = BOOL.True;
            rectShape.Protection.LockMoveY.Value = BOOL.True;
            rectShape.Protection.LockWidth.Value = BOOL.True;
            rectShape.Protection.LockHeight.Value = BOOL.True;
            rectShape.Protection.LockRotate.Value = BOOL.True;
            rectShape.Protection.LockVtxEdit.Value = BOOL.True;

            // ----- Begin batch processing steps -----
            // Example: add another shape (ellipse) while the rectangle is locked
            long ellipseShapeId = page.AddShape(5.0, 2.0, 1.5, 1.5, "Ellipse", false);
            Shape ellipseShape = page.Shapes.GetShape(ellipseShapeId);
            // (Additional drawing or manipulation logic would go here)
            // ----- End batch processing steps -----

            // Unlock the rectangle after all drawing steps are completed
            rectShape.Protection.LockMoveX.Value = BOOL.False;
            rectShape.Protection.LockMoveY.Value = BOOL.False;
            rectShape.Protection.LockWidth.Value = BOOL.False;
            rectShape.Protection.LockHeight.Value = BOOL.False;
            rectShape.Protection.LockRotate.Value = BOOL.False;
            rectShape.Protection.LockVtxEdit.Value = BOOL.False;

            // Save the diagram to VSDX format
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