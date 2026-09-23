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
            Console.WriteLine("Usage: DiagramHiddenInfoCleaner <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Count hidden shapes before removal.
            int hiddenShapesBefore = 0;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shape.Del is a BOOL enum; compare with BOOL.True.
                    if (shape.Del == BOOL.True)
                    {
                        hiddenShapesBefore++;
                    }
                }
            }

            Console.WriteLine($"Hidden shapes before removal: {hiddenShapesBefore}");

            // Remove hidden information (shapes and masters). Pages flag is omitted because it does not exist in the enum.
            int flags = (int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters);
            diagram.RemoveHiddenInformation(flags);

            // Count hidden shapes after removal to determine how many were actually removed.
            int hiddenShapesAfter = 0;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                    {
                        hiddenShapesAfter++;
                    }
                }
            }

            int removedShapes = hiddenShapesBefore - hiddenShapesAfter;
            Console.WriteLine($"Hidden shapes removed: {removedShapes}");

            // Save the cleaned diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Cleaned diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}