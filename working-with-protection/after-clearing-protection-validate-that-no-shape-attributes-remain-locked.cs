using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file (replace with actual path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Clear protection on all shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        ClearShapeProtection(shape);
                    }
                }

                // Validate that no protection flags remain set
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        ValidateShapeProtection(shape);
                    }
                }

                Console.WriteLine("All shape protection flags have been cleared successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Sets all lock properties of a shape to FALSE
        private static void ClearShapeProtection(Shape shape)
        {
            if (shape.Protection == null) return;

            shape.Protection.LockAspect.Value = BOOL.False;
            shape.Protection.LockBegin.Value = BOOL.False;
            shape.Protection.LockCalcWH.Value = BOOL.False;
            shape.Protection.LockCrop.Value = BOOL.False;
            shape.Protection.LockCustProp.Value = BOOL.False;
            shape.Protection.LockDelete.Value = BOOL.False;
            shape.Protection.LockEnd.Value = BOOL.False;
            shape.Protection.LockFormat.Value = BOOL.False;
            shape.Protection.LockFromGroupFormat.Value = BOOL.False;
            shape.Protection.LockGroup.Value = BOOL.False;
            shape.Protection.LockHeight.Value = BOOL.False;
            shape.Protection.LockMoveX.Value = BOOL.False;
            shape.Protection.LockMoveY.Value = BOOL.False;
            shape.Protection.LockRotate.Value = BOOL.False;
            shape.Protection.LockSelect.Value = BOOL.False;
            shape.Protection.LockTextEdit.Value = BOOL.False;
            shape.Protection.LockThemeColors.Value = BOOL.False;
            shape.Protection.LockThemeEffects.Value = BOOL.False;
            shape.Protection.LockVtxEdit.Value = BOOL.False;
            shape.Protection.LockWidth.Value = BOOL.False;
        }

        // Checks that all lock properties of a shape are FALSE; throws if any are TRUE
        private static void ValidateShapeProtection(Shape shape)
        {
            if (shape.Protection == null) return;

            if (shape.Protection.LockAspect.Value == BOOL.True) ThrowLockError(shape, "LockAspect");
            if (shape.Protection.LockBegin.Value == BOOL.True) ThrowLockError(shape, "LockBegin");
            if (shape.Protection.LockCalcWH.Value == BOOL.True) ThrowLockError(shape, "LockCalcWH");
            if (shape.Protection.LockCrop.Value == BOOL.True) ThrowLockError(shape, "LockCrop");
            if (shape.Protection.LockCustProp.Value == BOOL.True) ThrowLockError(shape, "LockCustProp");
            if (shape.Protection.LockDelete.Value == BOOL.True) ThrowLockError(shape, "LockDelete");
            if (shape.Protection.LockEnd.Value == BOOL.True) ThrowLockError(shape, "LockEnd");
            if (shape.Protection.LockFormat.Value == BOOL.True) ThrowLockError(shape, "LockFormat");
            if (shape.Protection.LockFromGroupFormat.Value == BOOL.True) ThrowLockError(shape, "LockFromGroupFormat");
            if (shape.Protection.LockGroup.Value == BOOL.True) ThrowLockError(shape, "LockGroup");
            if (shape.Protection.LockHeight.Value == BOOL.True) ThrowLockError(shape, "LockHeight");
            if (shape.Protection.LockMoveX.Value == BOOL.True) ThrowLockError(shape, "LockMoveX");
            if (shape.Protection.LockMoveY.Value == BOOL.True) ThrowLockError(shape, "LockMoveY");
            if (shape.Protection.LockRotate.Value == BOOL.True) ThrowLockError(shape, "LockRotate");
            if (shape.Protection.LockSelect.Value == BOOL.True) ThrowLockError(shape, "LockSelect");
            if (shape.Protection.LockTextEdit.Value == BOOL.True) ThrowLockError(shape, "LockTextEdit");
            if (shape.Protection.LockThemeColors.Value == BOOL.True) ThrowLockError(shape, "LockThemeColors");
            if (shape.Protection.LockThemeEffects.Value == BOOL.True) ThrowLockError(shape, "LockThemeEffects");
            if (shape.Protection.LockVtxEdit.Value == BOOL.True) ThrowLockError(shape, "LockVtxEdit");
            if (shape.Protection.LockWidth.Value == BOOL.True) ThrowLockError(shape, "LockWidth");
        }

        // Helper to throw an exception with shape ID and property name
        private static void ThrowLockError(Shape shape, string propertyName)
        {
            throw new Exception($"Shape ID {shape.ID} still has locked property: {propertyName}");
        }
    }