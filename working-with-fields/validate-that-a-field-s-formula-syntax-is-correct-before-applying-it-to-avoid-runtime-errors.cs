using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, new formula string
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <newFormula>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string newFormula = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Simple formula validation using a regular expression.
        // This is a placeholder; replace with Aspose.Diagram's native validation if available.
        bool IsFormulaValid(string formula)
        {
            // Allow letters, digits, basic operators, parentheses, and asterisk for multiplication.
            // Adjust the pattern as needed for more complex Visio formulas.
            return Regex.IsMatch(formula, @"^[A-Za-z0-9\+\-\*/\(\)\s]+$");
        }

        if (!IsFormulaValid(newFormula))
        {
            Console.Error.WriteLine("The provided formula appears to be invalid.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to apply the formula to each field
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has at least one field
                    if (shape.Fields == null || shape.Fields.Count == 0)
                        continue;

                    // Iterate over all fields in the shape
                    for (int i = 0; i < shape.Fields.Count; i++)
                    {
                        Field field = shape.Fields[i];

                        // Example: only modify fields of type formula (TypeFieldValue.Undefined is a safe default)
                        // Since specific type enums are not reliably known, we apply the formula to all fields.
                        // Set the formula string
                        field.Value.Ufev.F = newFormula;

                        // Set unit to undefined to avoid unit-related errors
                        field.Value.Ufev.Unit = MeasureConst.Undefined;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}