using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Create an in‑memory DataTable to simulate a relational database table
        DataTable pageDimensionsTable = new DataTable("PageDimensions");
        pageDimensionsTable.Columns.Add("RevisionId", typeof(int));
        pageDimensionsTable.Columns.Add("PageName", typeof(string));
        pageDimensionsTable.Columns.Add("WidthInches", typeof(double));
        pageDimensionsTable.Columns.Add("HeightInches", typeof(double));
        pageDimensionsTable.Columns.Add("Timestamp", typeof(DateTime));

        // For demonstration, we use a simple revision identifier
        int revisionId = 1; // In a real scenario this would come from your versioning system

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over each page explicitly typing the iterator as Page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Create a new DataRow with the extracted information
                DataRow row = pageDimensionsTable.NewRow();
                row["RevisionId"] = revisionId;
                // Page.Name is a string, not a cell with .Value
                row["PageName"] = page.Name ?? $"Page_{page.ID}";
                row["WidthInches"] = width;
                row["HeightInches"] = height;
                row["Timestamp"] = DateTime.UtcNow;

                pageDimensionsTable.Rows.Add(row);
            }

            // Simulated database insertion
            // In a real implementation you would use ADO.NET with a proper DbConnection,
            // e.g., SqlConnection, NpgsqlConnection, etc., and execute INSERT commands.
            // The following loop demonstrates how rows would be processed.
            foreach (DataRow dr in pageDimensionsTable.Rows)
            {
                // For this example we simply output the row to the console
                Console.WriteLine($"Stored Revision {dr["RevisionId"]}, Page '{dr["PageName"]}': {dr["WidthInches"]}in x {dr["HeightInches"]}in at {dr["Timestamp"]}");
            }

            // No need to dispose Diagram as it does not implement IDisposable
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}