using System.IO;
using System;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // -------------------------------------------------
        // 1. Load external data (example uses a DataTable)
        // -------------------------------------------------
        DataTable externalData = GetExternalData();

        // -------------------------------------------------
        // 2. Create a new empty Visio diagram
        // -------------------------------------------------
        Diagram diagram = new Diagram(); // uses the default constructor

        // -------------------------------------------------
        // 3. For each distinct record, add a new page
        // -------------------------------------------------
        foreach (DataRow row in externalData.Rows)
        {
            // Create a new page
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Optionally set a meaningful name for the page
            // (e.g., using a column called "Title" from the data row)
            if (externalData.Columns.Contains("Title") && row["Title"] != DBNull.Value)
            {
                newPage.Name = row["Title"].ToString();
            }

            // Additional page customization can be done here,
            // such as setting background, size, etc.
        }

        // -------------------------------------------------
        // 4. Save the diagram to a file
        // -------------------------------------------------
        // Save as VDX (Visio 2003-2007 XML format)
        diagram.Save("DynamicPagesOutput.vdx", SaveFileFormat.Vdx);
    }

    // Mock method to simulate retrieving external data.
    // Replace this with actual data access logic (e.g., database query, CSV read, etc.).
    static DataTable GetExternalData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Title", typeof(string));

        // Sample distinct records
        table.Rows.Add(1, "Page One");
        table.Rows.Add(2, "Page Two");
        table.Rows.Add(3, "Page Three");

        return table;
    }
}
