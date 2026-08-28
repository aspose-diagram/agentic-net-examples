using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        string outputPath = "output_modified.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    AttemptModifyShape(shape);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Tries to modify a shape but respects its protection settings
    static void AttemptModifyShape(Shape shape)
    {
        // Check for movement lock
        if (shape.Protection.LockMoveX.Value == BOOL.True ||
            shape.Protection.LockMoveY.Value == BOOL.True)
        {
            Console.WriteLine($"Shape ID {shape.ID} is locked from moving. Skipping move operation.");
        }
        else
        {
            // Example move: shift shape by 0.5 inches right and up
            shape.Move(0.5, 0.5);
            Console.WriteLine($"Moved shape ID {shape.ID}.");
        }

        // Check for size lock
        if (shape.Protection.LockWidth.Value == BOOL.True ||
            shape.Protection.LockHeight.Value == BOOL.True)
        {
            Console.WriteLine($"Shape ID {shape.ID} is locked from resizing. Skipping resize operation.");
        }
        else
        {
            // Example resize: increase width and height by 0.2 inches
            shape.SetWidth(shape.XForm.Width.Value + 0.2);
            shape.SetHeight(shape.XForm.Height.Value + 0.2);
            Console.WriteLine($"Resized shape ID {shape.ID}.");
        }

        // Check for rotation lock
        if (shape.Protection.LockRotate.Value == BOOL.True)
        {
            Console.WriteLine($"Shape ID {shape.ID} is locked from rotating. Skipping rotation operation.");
        }
        else
        {
            // Example rotation: rotate 15 degrees (converted to radians)
            double angleDeg = 15;
            double angleRad = (Math.PI / 180) * angleDeg;
            shape.SetAngle(angleRad);
            Console.WriteLine($"Rotated shape ID {shape.ID} by {angleDeg} degrees.");
        }

        // Check for text edit lock
        if (shape.Protection.LockTextEdit.Value == BOOL.True)
        {
            Console.WriteLine($"Shape ID {shape.ID} is locked from text editing. Skipping text update.");
        }
        else
        {
            // Example text update
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt($"Updated at {DateTime.Now}"));
            Console.WriteLine($"Updated text of shape ID {shape.ID}.");
        }

        // Check for delete lock (prevent deletion)
        if (shape.Protection.LockDelete.Value == BOOL.True)
        {
            Console.WriteLine($"Shape ID {shape.ID} is locked from deletion. Skipping delete operation.");
        }
        else
        {
            // Example delete flag (mark as deleted)
            shape.Del = BOOL.True;
            Console.WriteLine($"Marked shape ID {shape.ID} as deleted.");
        }
    }
}
