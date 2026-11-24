using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with actual path or pass as argument)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Simulate external database with an in‑memory DataTable
            DataTable tooltipTable = CreateTooltipDataTable();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the shape's NameU as the lookup key
                    string shapeName = shape.NameU ?? string.Empty;

                    // Find matching tooltip text in the simulated table
                    DataRow[] rows = tooltipTable.Select($"ShapeName = '{shapeName.Replace("'", "''")}'");
                    if (rows.Length > 0)
                    {
                        string tooltip = rows[0]["Tooltip"] as string ?? string.Empty;

                        // Ensure the Misc and Comment cells are available before assigning
                        if (shape.Misc != null && shape.Misc.Comment != null)
                        {
                            shape.Misc.Comment.Value = tooltip;
                            // Refresh shape data so the tooltip becomes effective
                            shape.RefreshData();
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with tooltips to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Creates a DataTable that mimics an external database containing shape tooltips
    private static DataTable CreateTooltipDataTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("ShapeName", typeof(string));
        table.Columns.Add("Tooltip", typeof(string));

        // Sample data – in a real scenario this would come from a database query
        table.Rows.Add("Process", "This shape represents a business process.");
        table.Rows.Add("Decision", "Decision point where a choice is made.");
        table.Rows.Add("Start", "Entry point of the workflow.");
        table.Rows.Add("End", "Termination point of the workflow.");

        return table;
    }
}