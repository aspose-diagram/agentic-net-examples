using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file (replace with actual path)
        string filePath = "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Log global document protection settings (BOOL values are compared directly)
            Console.WriteLine("=== Global Document Protection ===");
            Console.WriteLine($"Protect Backgrounds: {diagram.DocumentSettings.ProtectBkgnds == BOOL.True}");
            Console.WriteLine($"Protect Masters:      {diagram.DocumentSettings.ProtectMasters == BOOL.True}");
            Console.WriteLine($"Protect Shapes:      {diagram.DocumentSettings.ProtectShapes == BOOL.True}");
            Console.WriteLine($"Protect Styles:      {diagram.DocumentSettings.ProtectStyles == BOOL.True}");
            Console.WriteLine();

            // Iterate through all pages and shapes to find locked elements
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Accumulate names of lock properties that are set to TRUE
                    var lockedProps = "";

                    if (shape.Protection.LockAspect.Value == BOOL.True)          lockedProps += "LockAspect, ";
                    if (shape.Protection.LockBegin.Value == BOOL.True)           lockedProps += "LockBegin, ";
                    if (shape.Protection.LockCalcWH.Value == BOOL.True)          lockedProps += "LockCalcWH, ";
                    if (shape.Protection.LockCrop.Value == BOOL.True)            lockedProps += "LockCrop, ";
                    if (shape.Protection.LockCustProp.Value == BOOL.True)        lockedProps += "LockCustProp, ";
                    if (shape.Protection.LockDelete.Value == BOOL.True)          lockedProps += "LockDelete, ";
                    if (shape.Protection.LockEnd.Value == BOOL.True)             lockedProps += "LockEnd, ";
                    if (shape.Protection.LockFormat.Value == BOOL.True)          lockedProps += "LockFormat, ";
                    if (shape.Protection.LockFromGroupFormat.Value == BOOL.True)lockedProps += "LockFromGroupFormat, ";
                    if (shape.Protection.LockGroup.Value == BOOL.True)           lockedProps += "LockGroup, ";
                    if (shape.Protection.LockHeight.Value == BOOL.True)          lockedProps += "LockHeight, ";
                    if (shape.Protection.LockMoveX.Value == BOOL.True)           lockedProps += "LockMoveX, ";
                    if (shape.Protection.LockMoveY.Value == BOOL.True)           lockedProps += "LockMoveY, ";
                    if (shape.Protection.LockRotate.Value == BOOL.True)          lockedProps += "LockRotate, ";
                    if (shape.Protection.LockSelect.Value == BOOL.True)          lockedProps += "LockSelect, ";
                    if (shape.Protection.LockTextEdit.Value == BOOL.True)        lockedProps += "LockTextEdit, ";
                    if (shape.Protection.LockThemeColors.Value == BOOL.True)     lockedProps += "LockThemeColors, ";
                    if (shape.Protection.LockThemeEffects.Value == BOOL.True)    lockedProps += "LockThemeEffects, ";
                    if (shape.Protection.LockVtxEdit.Value == BOOL.True)         lockedProps += "LockVtxEdit, ";
                    if (shape.Protection.LockWidth.Value == BOOL.True)           lockedProps += "LockWidth, ";

                    if (!string.IsNullOrEmpty(lockedProps))
                    {
                        // Trim trailing comma and space
                        lockedProps = lockedProps.TrimEnd(' ', ',');

                        // Output locked shape information
                        Console.WriteLine($"Page: {page.NameU} | Shape ID: {shape.ID} | Name: {shape.NameU} | Locked: {lockedProps}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}