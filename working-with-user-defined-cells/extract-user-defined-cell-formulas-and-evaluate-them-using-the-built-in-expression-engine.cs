using System.IO;
using System;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with your actual file path)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages, shapes, and user‑defined cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        string cellName = userCell.NameU;
                        string cellValue = userCell.Value.Val;

                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, User Cell: {cellName}, Value: {cellValue}");

                        // If the cell contains a formula (starts with '=') evaluate it
                        if (!string.IsNullOrEmpty(cellValue) && cellValue.StartsWith("="))
                        {
                            string expression = cellValue.TrimStart('=');

                            try
                            {
                                // Simple arithmetic evaluation using DataTable.Compute
                                object result = new DataTable().Compute(expression, null);
                                Console.WriteLine($"  Evaluated Result: {result}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"  Evaluation failed: {ex.Message}");
                            }
                        }
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
