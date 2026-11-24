using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Prepare an in‑memory DataTable to simulate a relational database table
        DataTable pageDimensionsTable = new DataTable("PageDimensions");
        pageDimensionsTable.Columns.Add("DiagramPath", typeof(string));
        pageDimensionsTable.Columns.Add("RevisionTimestamp", typeof(DateTime));
        pageDimensionsTable.Columns.Add("PageIndex", typeof(int));
        pageDimensionsTable.Columns.Add("PageWidthInches", typeof(double));
        pageDimensionsTable.Columns.Add("PageHeightInches", typeof(double));

        // Capture the current timestamp as the revision identifier
        DateTime revisionTimestamp = DateTime.Now;

        try
        {
            // Load the Visio diagram inside a using block as required by Aspose.Diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each page in the diagram
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (values are in inches)
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a new row to the DataTable
                    DataRow row = pageDimensionsTable.NewRow();
                    row["DiagramPath"] = diagramPath;
                    row["RevisionTimestamp"] = revisionTimestamp;
                    row["PageIndex"] = pageIndex;
                    row["PageWidthInches"] = width;
                    row["PageHeightInches"] = height;
                    pageDimensionsTable.Rows.Add(row);

                    pageIndex++;
                }
            }

            // ----- Real database insertion (commented out) -----
            // The following illustrates how you would insert the collected data
            // into a relational database using ADO.NET. Replace the connection
            // string and table/column names with those matching your schema.
            /*
            using (var connection = new System.Data.SqlClient.SqlConnection("your-connection-string"))
            {
                connection.Open();
                foreach (DataRow row in pageDimensionsTable.Rows)
                {
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO PageDimensions (DiagramPath, RevisionTimestamp, PageIndex, PageWidthInches, PageHeightInches) " +
                        "VALUES (@DiagramPath, @RevisionTimestamp, @PageIndex, @PageWidthInches, @PageHeightInches)", connection))
                    {
                        command.Parameters.AddWithValue("@DiagramPath", row["DiagramPath"]);
                        command.Parameters.AddWithValue("@RevisionTimestamp", row["RevisionTimestamp"]);
                        command.Parameters.AddWithValue("@PageIndex", row["PageIndex"]);
                        command.Parameters.AddWithValue("@PageWidthInches", row["PageWidthInches"]);
                        command.Parameters.AddWithValue("@PageHeightInches", row["PageHeightInches"]);
                        command.ExecuteNonQuery();
                    }
                }
            }
            */

            // For demonstration purposes, output the collected data to the console
            Console.WriteLine("Page dimensions captured:");
            foreach (DataRow row in pageDimensionsTable.Rows)
            {
                Console.WriteLine($"Diagram: {row["DiagramPath"]}, Revision: {row["RevisionTimestamp"]}, " +
                                  $"Page {row["PageIndex"]}: {row["PageWidthInches"]}in x {row["PageHeightInches"]}in");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}