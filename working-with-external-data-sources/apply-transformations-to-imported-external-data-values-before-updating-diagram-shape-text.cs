using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Input CSV data path (external data)
        string csvPath = "data.csv";
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvPath}");
            return;
        }

        // Output diagram path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load external data into a DataTable (simulated CSV)
            DataTable table = new DataTable();
            table.Columns.Add("ShapeId", typeof(long));
            table.Columns.Add("NewText", typeof(string));

            foreach (string line in File.ReadAllLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Expected format: ShapeId,NewText
                string[] parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // malformed line

                if (long.TryParse(parts[0].Trim(), out long shapeId))
                {
                    string rawText = parts[1];
                    // Apply transformation: trim and convert to upper case
                    string transformedText = rawText.Trim().ToUpperInvariant();

                    // Add to DataTable
                    DataRow row = table.NewRow();
                    row["ShapeId"] = shapeId;
                    row["NewText"] = transformedText;
                    table.Rows.Add(row);
                }
            }

            // Iterate over each data row and update corresponding shape text
            foreach (DataRow row in table.Rows)
            {
                long shapeId = (long)row["ShapeId"];
                string newText = (string)row["NewText"];

                Shape targetShape = null;

                // Search for the shape across all pages
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // GetShape throws if the ID does not exist on this page
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    catch
                    {
                        // Shape not on this page – continue searching
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in diagram.");
                    continue;
                }

                // Clear existing text
                targetShape.Text.Value.Clear();

                // Add transformed text
                targetShape.Text.Value.Add(new Txt(newText));

                // Refresh shape data to apply changes
                targetShape.RefreshData();
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}