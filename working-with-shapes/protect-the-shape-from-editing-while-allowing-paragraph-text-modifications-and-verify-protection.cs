using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (pinX, pinY, width, height)
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);
            // Retrieve the shape instance (GetShape expects an int)
            Shape shape = page.Shapes.GetShape((int)rectId);

            // Set protection flags to lock most editing actions
            shape.Protection.LockMoveX.Value = BOOL.True;      // Prevent horizontal move
            shape.Protection.LockMoveY.Value = BOOL.True;      // Prevent vertical move
            shape.Protection.LockWidth.Value = BOOL.True;      // Prevent width change
            shape.Protection.LockHeight.Value = BOOL.True;     // Prevent height change
            shape.Protection.LockRotate.Value = BOOL.True;     // Prevent rotation
            shape.Protection.LockDelete.Value = BOOL.True;     // Prevent deletion
            shape.Protection.LockFormat.Value = BOOL.True;     // Prevent format changes
            shape.Protection.LockVtxEdit.Value = BOOL.True;    // Prevent vertex editing

            // Allow paragraph text modifications by ensuring text edit lock is FALSE
            shape.Protection.LockTextEdit.Value = BOOL.False;

            // Verify that the protection settings have been applied correctly
            if (shape.Protection.LockMoveX.Value != BOOL.True ||
                shape.Protection.LockMoveY.Value != BOOL.True ||
                shape.Protection.LockWidth.Value != BOOL.True ||
                shape.Protection.LockHeight.Value != BOOL.True ||
                shape.Protection.LockRotate.Value != BOOL.True ||
                shape.Protection.LockDelete.Value != BOOL.True ||
                shape.Protection.LockFormat.Value != BOOL.True ||
                shape.Protection.LockVtxEdit.Value != BOOL.True)
            {
                throw new Exception("One or more shape lock properties were not set to TRUE as expected.");
            }

            if (shape.Protection.LockTextEdit.Value != BOOL.False)
            {
                throw new Exception("LockTextEdit property is not FALSE; paragraph text editing is not allowed.");
            }

            Console.WriteLine("Shape protection configured successfully. Text editing remains enabled.");

            // Save the diagram to verify the protection persists
            diagram.Save("ProtectedShape.vsdx", SaveFileFormat.Vsdx);
        }
    }