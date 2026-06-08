using System;
using System.Data;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load external data (replace with actual data source)
        DataTable data = LoadExternalData();

        // Create a new empty Visio diagram
        Diagram diagram = new Diagram();

        // Determine distinct records based on a key column (e.g., "Category")
        DataTable distinctTable = data.DefaultView.ToTable(true, "Category");

        // Create a new page for each distinct record
        foreach (DataRow row in distinctTable.Rows)
        {
            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Optionally set a meaningful page name
            page.Name = row["Category"].ToString();

            // Example: add a shape to the page using a master shape (optional)
            // int masterId = diagram.AddMaster("Basic_U.vssx", "Rectangle");
            // diagram.AddShape(5.0, 5.0, masterId);
        }

        // Save the diagram to a file (using VDX format as an example)
        diagram.Save("DynamicPagesDiagram.vdx", SaveFileFormat.Vdx);
    }

    // Placeholder method to simulate loading data from an external source
    static DataTable LoadExternalData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Category", typeof(string));
        table.Columns.Add("Value", typeof(int));

        // Sample data rows
        table.Rows.Add("Finance", 100);
        table.Rows.Add("HR", 200);
        table.Rows.Add("Finance", 150);
        table.Rows.Add("IT", 300);
        table.Rows.Add("HR", 250);

        return table;
    }
}
