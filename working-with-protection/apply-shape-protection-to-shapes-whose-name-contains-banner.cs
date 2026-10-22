using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape's name contains "Banner" (case‑insensitive).
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Banner", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Apply protection to the shape.
                        shape.Protection.LockAspect.Value = BOOL.True;
                        shape.Protection.LockBegin.Value = BOOL.True;
                        shape.Protection.LockCalcWH.Value = BOOL.True;
                        shape.Protection.LockCrop.Value = BOOL.True;
                        shape.Protection.LockCustProp.Value = BOOL.True;
                        shape.Protection.LockDelete.Value = BOOL.True;
                        shape.Protection.LockEnd.Value = BOOL.True;
                        shape.Protection.LockFormat.Value = BOOL.True;
                        shape.Protection.LockFromGroupFormat.Value = BOOL.True;
                        shape.Protection.LockGroup.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shape.Protection.LockRotate.Value = BOOL.True;
                        shape.Protection.LockSelect.Value = BOOL.True;
                        shape.Protection.LockTextEdit.Value = BOOL.True;
                        shape.Protection.LockThemeColors.Value = BOOL.True;
                        shape.Protection.LockThemeEffects.Value = BOOL.True;
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                        shape.Protection.LockWidth.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}