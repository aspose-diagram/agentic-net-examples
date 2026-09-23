using System;
using System.Data;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Simulate external data source: a DataTable with ShapeId and IsVisible columns
                DataTable visibilityTable = CreateVisibilityDataTable();

                // Iterate through each row and set shape visibility accordingly
                foreach (DataRow row in visibilityTable.Rows)
                {
                    // Retrieve shape ID and visibility flag from the data row
                    long shapeId = Convert.ToInt64(row["ShapeId"]);
                    bool isVisible = Convert.ToBoolean(row["IsVisible"]);

                    // Retrieve the shape from the first page (adjust if shapes are on other pages)
                    Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} not found.");
                        continue;
                    }

                    // Hide shape by setting width and height to zero, show by restoring to a default size (e.g., 1 inch)
                    if (isVisible)
                    {
                        shape.XForm.Width.Value = 1.0;   // default visible width
                        shape.XForm.Height.Value = 1.0;  // default visible height
                    }
                    else
                    {
                        shape.XForm.Width.Value = 0.0;   // hide width
                        shape.XForm.Height.Value = 0.0;  // hide height
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

        // Helper method to create a sample DataTable representing external visibility data
        private static DataTable CreateVisibilityDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ShapeId", typeof(long));
            table.Columns.Add("IsVisible", typeof(bool));

            // Sample data: adjust ShapeId values to match actual shape IDs in your diagram
            table.Rows.Add(1L, true);
            table.Rows.Add(2L, false);
            table.Rows.Add(3L, true);
            // Add more rows as needed

            return table;
        }
    }