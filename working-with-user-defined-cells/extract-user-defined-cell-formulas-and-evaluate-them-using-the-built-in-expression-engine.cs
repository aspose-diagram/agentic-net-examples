using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file; can be passed as a command‑line argument.
            string filePath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages, shapes, and user‑defined cells.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        // User‑defined cell name (universal name) and its formula/value.
                        string cellName = userCell.NameU;
                        string formula = userCell.Value.Val;

                        Console.WriteLine($"Page: {page.NameU}, Shape: {shape.NameU}, UserCell: {cellName}, Formula: {formula}");

                        // NOTE: Aspose.Diagram does not provide a built‑in formula evaluation engine.
                        // If evaluation is required, implement a custom parser/evaluator here.
                    }
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
