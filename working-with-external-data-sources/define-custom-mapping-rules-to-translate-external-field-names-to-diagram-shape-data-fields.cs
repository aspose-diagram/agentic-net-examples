using System.IO;
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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Simulated external data source (e.g., from a CSV or API)
            DataTable externalData = new DataTable();
            externalData.Columns.Add("ShapeId", typeof(long));
            externalData.Columns.Add("EmployeeID", typeof(string));
            externalData.Columns.Add("Name", typeof(string));
            externalData.Columns.Add("Department", typeof(string));

            // Example rows – in real scenarios this would be populated dynamically
            externalData.Rows.Add(1L, "E001", "Alice", "HR");
            externalData.Rows.Add(2L, "E002", "Bob", "IT");

            // Map external fields to shape data fields (Data1, Data2, Data3)
            foreach (DataRow row in externalData.Rows)
            {
                long shapeId = (long)row["ShapeId"];
                // Assuming shapes are on the first page; adjust if needed
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                    continue; // Skip if shape not found

                // Assign values to the shape's custom data fields
                shape.Data1 = row["EmployeeID"].ToString();
                shape.Data2 = row["Name"].ToString();
                shape.Data3 = row["Department"].ToString();
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
