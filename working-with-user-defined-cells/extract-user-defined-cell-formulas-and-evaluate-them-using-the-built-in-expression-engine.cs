using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path.
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        // Guard against missing file.
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.NameU}");

                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.NameU}");

                    // Iterate through all user-defined cells (Users collection).
                    foreach (User userCell in shape.Users)
                    {
                        string cellName = userCell.NameU;
                        string cellValue = userCell.Value?.Val ?? string.Empty;
                        string cellPrompt = userCell.Prompt?.Value ?? string.Empty;

                        Console.WriteLine($"    User Cell: {cellName}");
                        Console.WriteLine($"      Prompt : {cellPrompt}");
                        Console.WriteLine($"      Formula: {cellValue}");

                        // Attempt to evaluate the formula using .NET's DataTable.Compute.
                        // This serves as a fallback since Aspose.Diagram does not provide a built‑in evaluator.
                        string expression = cellValue.Trim();

                        // If the value starts with '=', treat the rest as an expression.
                        if (expression.StartsWith("="))
                        {
                            expression = expression.Substring(1);
                        }

                        // Only attempt evaluation for non‑empty expressions.
                        if (!string.IsNullOrWhiteSpace(expression))
                        {
                            try
                            {
                                // DataTable.Compute can evaluate simple arithmetic expressions.
                                var result = new DataTable().Compute(expression, null);
                                Console.WriteLine($"      Evaluated Result: {result}");
                            }
                            catch (Exception evalEx)
                            {
                                Console.WriteLine($"      Evaluation failed: {evalEx.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("      No formula to evaluate.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}