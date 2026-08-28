using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the active page where we will draw the rectangle
                Page page = diagram.ActivePage;

                // Draw a rectangle at position (2,2) with width 4 and height 2 inches
                // DrawRectangle returns the shape ID (long)
                long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

                // Retrieve the shape object using the returned ID
                Shape rectangle = page.Shapes.GetShape((int)rectId);

                // Unlock the rectangle by clearing all protection flags
                // Each lock property is a BOOL enum; set to BOOL.False
                rectangle.Protection.LockMoveX.Value = BOOL.False;
                rectangle.Protection.LockMoveY.Value = BOOL.False;
                rectangle.Protection.LockWidth.Value = BOOL.False;
                rectangle.Protection.LockHeight.Value = BOOL.False;
                rectangle.Protection.LockRotate.Value = BOOL.False;
                rectangle.Protection.LockVtxEdit.Value = BOOL.False;
                rectangle.Protection.LockAspect.Value = BOOL.False;
                rectangle.Protection.LockSelect.Value = BOOL.False;
                rectangle.Protection.LockDelete.Value = BOOL.False;
                rectangle.Protection.LockTextEdit.Value = BOOL.False;
                rectangle.Protection.LockThemeColors.Value = BOOL.False;
                rectangle.Protection.LockThemeEffects.Value = BOOL.False;
                rectangle.Protection.LockBegin.Value = BOOL.False;
                rectangle.Protection.LockEnd.Value = BOOL.False;
                rectangle.Protection.LockCrop.Value = BOOL.False;
                rectangle.Protection.LockCustProp.Value = BOOL.False;
                rectangle.Protection.LockFormat.Value = BOOL.False;
                rectangle.Protection.LockFromGroupFormat.Value = BOOL.False;
                rectangle.Protection.LockGroup.Value = BOOL.False;

                // Save the diagram to a VSDX file
                diagram.Save("UnlockedRectangle.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }