using System;
using System.Data;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // NOTE: Oracle data access requires external libraries not available in this project.
        // The following mock data simulates rows that would be retrieved from an Oracle database.
        DataTable mockTable = new DataTable();
        mockTable.Columns.Add("POSX", typeof(double));
        mockTable.Columns.Add("POSY", typeof(double));
        mockTable.Columns.Add("WIDTH", typeof(double));
        mockTable.Columns.Add("HEIGHT", typeof(double));
        mockTable.Columns.Add("MASTER_NAME", typeof(string));
        mockTable.Columns.Add("SHAPE_TEXT", typeof(string));

        // Add sample rows – replace this block with actual Oracle data retrieval logic.
        mockTable.Rows.Add(1.0, 2.0, 1.5, 1.0, "Rectangle", "First Shape");
        mockTable.Rows.Add(3.0, 4.0, 2.0, 1.5, "Ellipse", "Second Shape");

        // Create an empty diagram and ensure it has at least one page.
        Diagram diagram = new Diagram();
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }
        Page page = diagram.Pages[0];

        try
        {
            // Iterate over the mock data (replace with OracleDataReader loop in real scenario).
            foreach (DataRow row in mockTable.Rows)
            {
                // Retrieve values from the data row.
                double posX = Convert.ToDouble(row["POSX"]);
                double posY = Convert.ToDouble(row["POSY"]);
                double width = Convert.ToDouble(row["WIDTH"]);
                double height = Convert.ToDouble(row["HEIGHT"]);
                string masterName = row["MASTER_NAME"].ToString();
                string shapeText = row["SHAPE_TEXT"].ToString();

                // Add a shape based on the master name and dimensions.
                long shapeId = page.AddShape(posX, posY, width, height, masterName);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Populate the shape's text.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(shapeText));
            }

            // Save the diagram to VSDX format.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}