using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, shape ID, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the shape ID (expected to be a long integer)
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the shape with the specified ID across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // GetShape accepts a long ID; returns the shape if it exists on this page
                        targetShape = page.Shapes.GetShape(shapeId);
                        if (targetShape != null)
                            break; // Shape found, exit the loop
                    }
                    catch
                    {
                        // GetShape throws when the ID is not present on this page; ignore and continue
                        targetShape = null;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                    return;
                }

                // Unlock the rotation attribute by setting the LockRotate protection flag to FALSE
                // Protection properties must be accessed via the .Value field and use BOOL enum
                targetShape.Protection.LockRotate.Value = BOOL.False;

                // Save the modified diagram to the specified output path (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape rotation unlocked and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}