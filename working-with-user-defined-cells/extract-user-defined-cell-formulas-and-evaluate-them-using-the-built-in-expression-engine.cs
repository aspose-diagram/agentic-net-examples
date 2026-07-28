using System.IO;
using System;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output Visio file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages, shapes, and user‑defined cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        string formula = userCell.Value.Val; // The formula or value stored in the user cell
                        double evalResult;
                        bool success = TryEvaluateFormula(formula, out evalResult);

                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, User Cell: {userCell.Name}, Formula: \"{formula}\", Result: {(success ? evalResult.ToString() : "Evaluation failed")}");

                        // Optionally store the evaluated result back into the cell
                        if (success)
                        {
                            userCell.Value.Val = evalResult.ToString();
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Simple evaluator for arithmetic expressions using DataTable.Compute
    static bool TryEvaluateFormula(string expression, out double result)
    {
        result = 0;
        if (string.IsNullOrWhiteSpace(expression))
            return false;

        try
        {
            // DataTable.Compute can evaluate basic arithmetic expressions
            object eval = new DataTable().Compute(expression, null);
            result = Convert.ToDouble(eval);
            return true;
        }
        catch
        {
            // Evaluation failed (unsupported functions, syntax errors, etc.)
            return false;
        }
    }
}
