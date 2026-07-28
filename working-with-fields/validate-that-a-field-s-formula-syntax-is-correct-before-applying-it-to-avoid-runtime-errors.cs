using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram (may throw if file is corrupted)
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram contains at least one page
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram contains no pages.");
            return;
        }

        // Get the first page
        Page page = diagram.Pages[0];

        // Ensure the page has at least one shape
        if (page.Shapes.Count == 0)
        {
            Console.Error.WriteLine("The page contains no shapes.");
            return;
        }

        // Retrieve the first shape on the page
        Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

        // Ensure the shape has at least one text insertion field
        if (shape.Fields.Count == 0)
        {
            Console.Error.WriteLine("The shape does not contain any fields.");
            return;
        }

        // Select the first field to modify
        Field field = shape.Fields[0];

        // New formula to assign to the field
        string newFormula = "Width*Height";

        // Validate the formula syntax using a simple regex (basic validation)
        bool isValid = IsFormulaValid(newFormula);
        if (!isValid)
        {
            Console.Error.WriteLine($"The formula \"{newFormula}\" is not valid.");
            return;
        }

        // Apply the validated formula to the field
        field.Value.Ufev.F = newFormula;
        // Reset unit to undefined (no specific unit)
        field.Value.Ufev.Unit = MeasureConst.Undefined;

        // Output file path
        string outputPath = "output.vsdx";

        try
        {
            // Save the modified diagram using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }

    // Simple formula validator: allows letters, numbers, spaces and basic operators
    static bool IsFormulaValid(string formula)
    {
        // Regex matches expressions like "Width*Height", "Length + 2", etc.
        return Regex.IsMatch(formula, @"^[A-Za-z0-9\*\+\/\-\(\)\s]+$");
    }
}