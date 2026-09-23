using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Set and log global document protection settings
            SetDocumentProtection(diagram, "ProtectBkgnds", BOOL.True);
            SetDocumentProtection(diagram, "ProtectMasters", BOOL.False);
            SetDocumentProtection(diagram, "ProtectShapes", BOOL.True);
            SetDocumentProtection(diagram, "ProtectStyles", BOOL.False);

            // Add a shape to demonstrate shape-level protection
            Page page = diagram.Pages[0];
            long shapeId = page.AddShape(1, 1, 2, 1, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set and log shape protection properties
            SetShapeProtection(shape, "LockMoveX", BOOL.True);
            SetShapeProtection(shape, "LockMoveY", BOOL.False);
            SetShapeProtection(shape, "LockWidth", BOOL.True);
            SetShapeProtection(shape, "LockHeight", BOOL.True);
            SetShapeProtection(shape, "LockRotate", BOOL.True);
            SetShapeProtection(shape, "LockVtxEdit", BOOL.False);

            // Save the diagram
            diagram.Save("ProtectedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    static void SetDocumentProtection(Diagram diagram, string propertyName, BOOL value)
    {
        switch (propertyName)
        {
            case "ProtectBkgnds":
                diagram.DocumentSettings.ProtectBkgnds = value;
                break;
            case "ProtectMasters":
                diagram.DocumentSettings.ProtectMasters = value;
                break;
            case "ProtectShapes":
                diagram.DocumentSettings.ProtectShapes = value;
                break;
            case "ProtectStyles":
                diagram.DocumentSettings.ProtectStyles = value;
                break;
            default:
                throw new Exception($"Unknown document protection property: {propertyName}");
        }
        LogChange("Document", propertyName, value);
    }

    static void SetShapeProtection(Shape shape, string propertyName, BOOL value)
    {
        switch (propertyName)
        {
            case "LockMoveX":
                shape.Protection.LockMoveX.Value = value;
                break;
            case "LockMoveY":
                shape.Protection.LockMoveY.Value = value;
                break;
            case "LockWidth":
                shape.Protection.LockWidth.Value = value;
                break;
            case "LockHeight":
                shape.Protection.LockHeight.Value = value;
                break;
            case "LockRotate":
                shape.Protection.LockRotate.Value = value;
                break;
            case "LockVtxEdit":
                shape.Protection.LockVtxEdit.Value = value;
                break;
            default:
                throw new Exception($"Unknown shape protection property: {propertyName}");
        }
        LogChange($"Shape ID {shape.ID}", propertyName, value);
    }

    static void LogChange(string elementIdentifier, string propertyName, BOOL value)
    {
        string timestamp = DateTime.Now.ToString("o");
        Console.WriteLine($"{timestamp} - {elementIdentifier} - {propertyName} set to {value}");
    }
}
