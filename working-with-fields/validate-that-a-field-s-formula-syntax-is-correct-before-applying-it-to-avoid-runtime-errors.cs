using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    // Simple Visio formula validation using a regular expression.
    // This checks for allowed characters, basic function names and balanced parentheses.
    static bool IsFormulaValid(string formula)
    {
        if (string.IsNullOrWhiteSpace(formula))
            return false;

        // Allow letters, digits, underscores, operators, parentheses, commas, periods, and spaces.
        const string pattern = @"^[A-Za-z0-9_\.\+\-\*/\^\(\),\s]+$";
        if (!Regex.IsMatch(formula, pattern))
            return false;

        // Basic parentheses balance check.
        int balance = 0;
        foreach (char c in formula)
        {
            if (c == '(') balance++;
            else if (c == ')') balance--;
            if (balance < 0) return false;
        }
        return balance == 0;
    }

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has at least one field.
                    if (shape.Fields.Count == 0)
                        continue;

                    // Process each field in the shape.
                    foreach (Field field in shape.Fields)
                    {
                        // Example: we want to set a new formula for the field.
                        string newFormula = "Width*Height";

                        // Validate the formula before applying.
                        if (IsFormulaValid(newFormula))
                        {
                            // Assign the formula to the field's UFEV (universal formula expression value).
                            field.Value.Ufev.F = newFormula;
                            Console.WriteLine($"Applied formula to shape ID {shape.ID}, field IX {field.IX}.");
                        }
                        else
                        {
                            Console.WriteLine($"Invalid formula '{newFormula}' for shape ID {shape.ID}, field IX {field.IX}. Skipping assignment.");
                        }
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
