using System;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Simulated external database: a DataTable with ShapeId -> Tooltip mapping
                DataTable tooltipTable = new DataTable();
                tooltipTable.Columns.Add("ShapeId", typeof(int));
                tooltipTable.Columns.Add("Tooltip", typeof(string));

                // Sample data rows (in a real scenario, fill this table from a database)
                tooltipTable.Rows.Add(1, "Start process");
                tooltipTable.Rows.Add(2, "Decision point");
                tooltipTable.Rows.Add(3, "End process");

                // Iterate through all pages and shapes to assign tooltips
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Convert shape ID to int for lookup
                        int shapeId = (int)shape.ID;

                        // Find matching tooltip text
                        DataRow[] rows = tooltipTable.Select($"ShapeId = {shapeId}");
                        if (rows.Length == 0)
                            continue; // No tooltip defined for this shape

                        string tooltipText = rows[0]["Tooltip"].ToString();

                        // Create a new hyperlink to hold the tooltip (address can be empty)
                        Hyperlink link = new Hyperlink();
                        link.Name = $"Tooltip_{shapeId}";
                        link.Address.Value = ""; // No navigation target
                        link.Description.Value = tooltipText;

                        // Add the hyperlink to the shape's collection
                        shape.Hyperlinks.Add(link);
                    }
                }

                // Save the updated diagram with tooltips
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }