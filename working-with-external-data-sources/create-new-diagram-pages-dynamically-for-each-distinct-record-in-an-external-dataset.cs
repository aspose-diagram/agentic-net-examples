using System.IO;
using System;
using System.Collections.Generic;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram (lifecycle create)
        Diagram diagram = new Diagram();

        // Load external data (replace with real data source as needed)
        DataTable externalData = GetExternalData();

        // Determine distinct values for which pages will be created
        var distinctKeys = new HashSet<string>();
        foreach (DataRow row in externalData.Rows)
        {
            distinctKeys.Add(row["Category"].ToString());
        }

        // Dynamically add a page for each distinct record
        foreach (string key in distinctKeys)
        {
            // Add a new page to the diagram
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Set a meaningful name for the page
            newPage.Name = key;

            // Example: place a shape on the page (optional)
            // diagram.AddShape(4.25, 5.5, "Rectangle", 0);
        }

        // Save the diagram to a file (lifecycle save)
        diagram.Save("DynamicPagesOutput.vdx", SaveFileFormat.Vdx);
    }

    // Mock method to simulate retrieving external data
    static DataTable GetExternalData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Category", typeof(string));
        table.Columns.Add("Value", typeof(string));

        table.Rows.Add(1, "Alpha", "A1");
        table.Rows.Add(2, "Beta", "B1");
        table.Rows.Add(3, "Alpha", "A2");
        table.Rows.Add(4, "Gamma", "G1");

        return table;
    }
}
