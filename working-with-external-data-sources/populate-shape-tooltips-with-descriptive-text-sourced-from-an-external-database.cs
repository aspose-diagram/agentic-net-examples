using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Simulated external database: a DataTable with shape IDs and tooltips
            // In a real scenario this data would come from an actual database.
            // ------------------------------------------------------------
            DataTable tooltipTable = new DataTable();
            tooltipTable.Columns.Add("ShapeId", typeof(long));
            tooltipTable.Columns.Add("Tooltip", typeof(string));

            // Sample data rows (populate as needed)
            tooltipTable.Rows.Add(1L, "Start of the process");
            tooltipTable.Rows.Add(2L, "Decision point");
            tooltipTable.Rows.Add(3L, "End of the process");
            // Add more rows corresponding to actual shape IDs in the diagram

            // ------------------------------------------------------------
            // Iterate through all pages and shapes, assigning tooltips
            // ------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Look up a tooltip for the current shape ID
                    DataRow[] rows = tooltipTable.Select($"ShapeId = {shape.ID}");
                    if (rows.Length > 0)
                    {
                        string tooltip = rows[0]["Tooltip"]?.ToString() ?? string.Empty;

                        // Ensure the Misc and Comment objects are available before setting
                        if (shape.Misc != null && shape.Misc.Comment != null)
                        {
                            shape.Misc.Comment.Value = tooltip;
                        }
                    }
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with tooltips to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}