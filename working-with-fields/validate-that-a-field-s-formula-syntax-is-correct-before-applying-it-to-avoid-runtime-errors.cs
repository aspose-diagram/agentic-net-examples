using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Simple formula validator:
    // - Not null or whitespace
    // - Parentheses are balanced
    // - Contains only allowed characters (letters, digits, operators, underscores, dot, and parentheses)
    static bool IsFormulaValid(string formula)
    {
        if (string.IsNullOrWhiteSpace(formula))
            return false;

        int balance = 0;
        foreach (char c in formula)
        {
            if (c == '(') balance++;
            else if (c == ')')
            {
                balance--;
                if (balance < 0) return false; // closing before opening
            }
            else if (!char.IsLetterOrDigit(c) && c != '_' && c != '.' && c != '+' && c != '-' && c != '*' && c != '/' && c != ' ')
            {
                // Invalid character detected
                return false;
            }
        }
        return balance == 0;
    }

    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes
        foreach (Aspose.Diagram.Page page in diagram.Pages)
        {
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                // Ensure the shape has fields before accessing
                if (shape.Fields == null || shape.Fields.Count == 0)
                    continue;

                // Iterate over each field in the shape
                foreach (Field field in shape.Fields)
                {
                    string formula = field.Value.Ufev.F;

                    // Validate the formula syntax
                    if (!IsFormulaValid(formula))
                    {
                        Console.WriteLine($"Invalid formula detected in Shape ID {shape.ID}: \"{formula}\"");
                        // Skip applying invalid formula
                        continue;
                    }

                    // Example of assigning a new formula after validation
                    // field.Value.Ufev.F = "Width*Height";
                }
            }
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}