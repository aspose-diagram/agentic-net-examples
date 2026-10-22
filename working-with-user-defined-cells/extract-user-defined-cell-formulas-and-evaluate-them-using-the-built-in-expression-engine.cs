using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through all user‑defined cells (User collection) of the shape.
                    foreach (User userCell in shape.Users)
                    {
                        // Extract the cell name and the raw formula/value string.
                        string cellName = userCell.NameU ?? userCell.Name;
                        string formula = userCell.Value?.Val ?? string.Empty;

                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, User Cell: {cellName}");
                        Console.WriteLine($"  Formula/Value: {formula}");

                        // Attempt to evaluate simple numeric expressions using DataTable.Compute.
                        // This is a fallback because Aspose.Diagram does not provide a formula engine.
                        if (!string.IsNullOrWhiteSpace(formula))
                        {
                            try
                            {
                                // DataTable.Compute can evaluate basic arithmetic expressions.
                                // It will throw if the expression contains unsupported tokens.
                                object result = new DataTable().Compute(formula, null);
                                Console.WriteLine($"  Evaluated Result: {result}");
                            }
                            catch (Exception evalEx)
                            {
                                // If evaluation fails, report the issue but continue processing.
                                Console.WriteLine($"  Evaluation failed: {evalEx.Message}");
                            }
                        }

                        Console.WriteLine(); // Blank line for readability.
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during Aspose.Diagram processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}