using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Helper to log protection changes with timestamp and element identifier
    static void LogChange(string elementIdentifier, string changeDescription)
    {
        Console.WriteLine($"{DateTime.Now:O} - {elementIdentifier}: {changeDescription}");
    }

    static void Main()
    {
        // Create a new diagram (empty Visio document)
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Add a rectangle shape to the page
        // DrawRectangle(pinX, pinY, width, height) returns the shape ID (long)
        long rectShapeId = page.DrawRectangle(1.0, 1.0, 2.0, 2.0);

        // Retrieve the shape object using its ID
        Shape rectShape = page.Shapes.GetShape(rectShapeId);

        // Apply protection to the shape and log each change
        rectShape.Protection.LockMoveX.Value = BOOL.True;
        LogChange($"Shape ID {rectShapeId}", "LockMoveX set to TRUE");

        rectShape.Protection.LockMoveY.Value = BOOL.True;
        LogChange($"Shape ID {rectShapeId}", "LockMoveY set to TRUE");

        rectShape.Protection.LockWidth.Value = BOOL.True;
        LogChange($"Shape ID {rectShapeId}", "LockWidth set to TRUE");

        rectShape.Protection.LockHeight.Value = BOOL.True;
        LogChange($"Shape ID {rectShapeId}", "LockHeight set to TRUE");

        // Apply global document protection settings and log each change
        diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
        LogChange("Document", "ProtectBkgnds set to TRUE");

        diagram.DocumentSettings.ProtectMasters = BOOL.True;
        LogChange("Document", "ProtectMasters set to TRUE");

        diagram.DocumentSettings.ProtectShapes = BOOL.True;
        LogChange("Document", "ProtectShapes set to TRUE");

        diagram.DocumentSettings.ProtectStyles = BOOL.True;
        LogChange("Document", "ProtectStyles set to TRUE");

        // Save the diagram to a VSDX file
        diagram.Save("ProtectedDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
