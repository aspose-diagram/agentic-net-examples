using System.IO;
using System;
using Aspose.Diagram;
using System.Data;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages, shapes, and user‑defined cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        string cellName = userCell.NameU;          // Universal name of the user cell
                        string formula = userCell.Value.Val;       // The formula or value stored in the cell

                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, User Cell: {cellName}, Formula: {formula}");

                        // Try to evaluate the formula using a simple .NET expression evaluator
                        try
                        {
                            // DataTable.Compute can handle basic arithmetic expressions
                            object result = new DataTable().Compute(formula, null);
                            Console.WriteLine($"Evaluated Result: {result}");
                        }
                        catch (Exception ex)
                        {
                            // If the formula cannot be evaluated, report the error
                            Console.WriteLine($"Could not evaluate formula: {ex.Message}");
                        }

                        Console.WriteLine(); // Blank line for readability
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
