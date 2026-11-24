using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments:
        // 0 - input Visio file path
        // 1 - output Visio file path
        // 2 - target shape ID (numeric)
        // 3 - new formula string to apply to the first field of the shape
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <shapeId> <newFormula>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string newFormula = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID (Visio shape IDs are long)
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the target shape by searching all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape accepts a long ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Ensure the shape has at least one field to update
            if (targetShape.Fields == null || targetShape.Fields.Count == 0)
            {
                Console.Error.WriteLine($"Shape ID {targetShapeId} does not contain any fields.");
                return;
            }

            // Validate the formula syntax before applying it
            bool isFormulaValid = ValidateFormula(diagram, newFormula);
            if (!isFormulaValid)
            {
                Console.Error.WriteLine("The provided formula is syntactically invalid and will not be applied.");
                return;
            }

            // Apply the validated formula to the first field of the target shape
            Field fieldToUpdate = targetShape.Fields[0];
            fieldToUpdate.Value.Ufev.F = newFormula;
            fieldToUpdate.Value.Ufev.Unit = MeasureConst.Undefined;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates a Visio formula by attempting to assign it to a temporary field.
    /// Returns true if no exception is thrown, indicating the formula is syntactically correct.
    /// </summary>
    /// <param name="diagram">The diagram instance to use for temporary shape creation.</param>
    /// <param name="formula">The formula string to validate.</param>
    /// <returns>True if the formula is valid; otherwise false.</returns>
    private static bool ValidateFormula(Diagram diagram, string formula)
    {
        try
        {
            // Add a temporary rectangle shape on the first page (page index 0)
            // Using overload: AddShape(pinX, pinY, masterName, pageIndex)
            long tempShapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

            // Retrieve the shape instance
            Shape tempShape = diagram.Pages[0].Shapes.GetShape(tempShapeId);
            if (tempShape == null)
                return false; // Unexpected, treat as invalid

            // Create a new field and assign the formula
            Field tempField = new Field();
            tempField.Value.Ufev.F = formula;
            tempField.Value.Ufev.Unit = MeasureConst.Undefined;

            // Add the field to the temporary shape
            tempShape.Fields.Add(tempField);

            // If we reach this point, no exception was thrown => formula is syntactically valid
            // Mark the temporary shape for deletion
            tempShape.Del = BOOL.True;
            return true;
        }
        catch
        {
            // Any exception indicates invalid formula syntax
            return false;
        }
    }
}