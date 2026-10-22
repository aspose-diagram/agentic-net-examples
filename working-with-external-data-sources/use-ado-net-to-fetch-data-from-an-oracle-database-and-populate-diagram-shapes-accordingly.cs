using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <templateDiagramPath> <outputDiagramPath>");
            return;
        }

        string templatePath = args[0];
        string outputPath = args[1];

        // Guard against missing template file.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template diagram file not found: {templatePath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(templatePath))
            {
                // ------------------------------------------------------------
                // Simulate fetching data from an Oracle database.
                // In a real scenario you would use OracleConnection, OracleCommand, etc.
                // Here we create an in‑memory DataTable with sample rows.
                // ------------------------------------------------------------
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("PosX", typeof(double));
                dataTable.Columns.Add("PosY", typeof(double));

                // Sample data rows.
                dataTable.Rows.Add(1, "Alpha", 2.0, 2.0);
                dataTable.Rows.Add(2, "Beta", 4.0, 2.0);
                dataTable.Rows.Add(3, "Gamma", 2.0, 4.0);
                dataTable.Rows.Add(4, "Delta", 4.0, 4.0);

                // ------------------------------------------------------------
                // Add a rectangle shape for each data row onto the first page.
                // ------------------------------------------------------------
                Page page = diagram.Pages[0];

                foreach (DataRow row in dataTable.Rows)
                {
                    double pinX = Convert.ToDouble(row["PosX"]);
                    double pinY = Convert.ToDouble(row["PosY"]);
                    string shapeText = row["Name"].ToString();

                    // Add a rectangle shape. The method returns the shape ID (long).
                    long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

                    // Retrieve the Shape instance using the returned ID.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and add new text.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(shapeText));

                    // Set default size (width & height in inches).
                    shape.XForm.Width.Value = 1.5;   // 1.5 inches wide
                    shape.XForm.Height.Value = 0.8; // 0.8 inches tall

                    // Set fill color to light gray (hex string).
                    shape.Fill.FillForegnd.Value = "#D9D9D9";

                    // Example: hide shape if Id is even by setting dimensions to zero.
                    int id = Convert.ToInt32(row["Id"]);
                    if (id % 2 == 0)
                    {
                        shape.XForm.Width.Value = 0;
                        shape.XForm.Height.Value = 0;
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during diagram processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}