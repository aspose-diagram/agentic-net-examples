using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, shape name, user-defined cell name, new formula
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <shapeNameU> <userCellNameU> <newFormula>");
            return;
        }

        string inputPath = args[0];
        string targetShapeNameU = args[1];
        string targetUserCellNameU = args[2];
        string newFormula = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare output path (same folder, suffix "_updated.vdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vdx");

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool shapeFound = false;
                bool userCellFound = false;

                // Iterate through all pages and shapes to locate the target shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare shape's NameU with the provided shape name
                        if (string.Equals(shape.NameU, targetShapeNameU, StringComparison.OrdinalIgnoreCase))
                        {
                            shapeFound = true;

                            // Iterate through user-defined cells (Users collection)
                            foreach (User userCell in shape.Users)
                            {
                                // Compare user cell's NameU with the provided cell name
                                if (string.Equals(userCell.NameU, targetUserCellNameU, StringComparison.OrdinalIgnoreCase))
                                {
                                    userCellFound = true;

                                    // Update the cell's value with the new formula
                                    userCell.Value.Val = newFormula;

                                    // No need to continue searching after update
                                    break;
                                }
                            }

                            // Exit shape loop after processing the target shape
                            break;
                        }
                    }

                    if (shapeFound)
                        break;
                }

                if (!shapeFound)
                {
                    Console.Error.WriteLine($"Shape with NameU '{targetShapeNameU}' not found.");
                    return;
                }

                if (!userCellFound)
                {
                    Console.Error.WriteLine($"User-defined cell with NameU '{targetUserCellNameU}' not found in shape '{targetShapeNameU}'.");
                    return;
                }

                // Save the updated diagram in VDX format
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}