using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Path to the source Visio file
    private const string InputPath = "input.vsdx";
    // Path to the output Visio file after attempted modifications
    private const string OutputPath = "output.vsdx";

    static void Main()
    {
        // Verify that the input file exists before attempting to load it
        if (!File.Exists(InputPath))
        {
            Console.Error.WriteLine($"File not found: {InputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram from the specified file
            diagram = new Diagram(InputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes to apply modifications
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Attempt to modify the shape only if it is not locked
                ModifyShapeIfUnlocked(shape);
            }
        }

        try
        {
            // Save the diagram (even if no changes were made)
            diagram.Save(OutputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Tries to modify a shape only if it is not locked.
    /// Logs any attempt to modify a locked attribute.
    /// </summary>
    /// <param name="shape">The shape to process.</param>
    private static void ModifyShapeIfUnlocked(Shape shape)
    {
        // Example modification: increase width by 1 inch
        if (shape.Protection.LockWidth.Value == BOOL.True)
        {
            Console.WriteLine($"[Lock] Shape ID {shape.ID} - Width is locked. Modification skipped.");
        }
        else
        {
            try
            {
                double originalWidth = shape.XForm.Width.Value;
                shape.XForm.Width.Value = originalWidth + 1.0;
                Console.WriteLine($"[Modify] Shape ID {shape.ID} - Width changed from {originalWidth} to {shape.XForm.Width.Value}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Shape ID {shape.ID} - Failed to modify width: {ex.Message}");
            }
        }

        // Example modification: change line color to red (no specific lock property exists for line)
        try
        {
            shape.Line.LineColor.Value = "#FF0000";
            Console.WriteLine($"[Modify] Shape ID {shape.ID} - Line color set to red.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Shape ID {shape.ID} - Failed to set line color: {ex.Message}");
        }

        // Example modification: move shape horizontally by 0.5 inch
        if (shape.Protection.LockMoveX.Value == BOOL.True)
        {
            Console.WriteLine($"[Lock] Shape ID {shape.ID} - Horizontal move is locked. Modification skipped.");
        }
        else
        {
            try
            {
                double originalPinX = shape.XForm.PinX.Value;
                shape.XForm.PinX.Value = originalPinX + 0.5;
                Console.WriteLine($"[Modify] Shape ID {shape.ID} - PinX moved from {originalPinX} to {shape.XForm.PinX.Value}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Shape ID {shape.ID} - Failed to move shape: {ex.Message}");
            }
        }
    }
}