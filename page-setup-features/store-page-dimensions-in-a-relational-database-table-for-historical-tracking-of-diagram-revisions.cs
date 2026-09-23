using System;
using Aspose.Diagram;
using System.Data;
using System.Data.Common;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "example.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Simulated revision identifier (in a real scenario this could be a GUID or incrementing number)
                int revisionId = 1;

                // Create an in‑memory DataTable to represent the relational database table
                DataTable pageDimensionsTable = new DataTable("PageDimensions");
                pageDimensionsTable.Columns.Add("RevisionId", typeof(int));
                pageDimensionsTable.Columns.Add("PageId", typeof(int));
                pageDimensionsTable.Columns.Add("PageWidthInches", typeof(double));
                pageDimensionsTable.Columns.Add("PageHeightInches", typeof(double));
                pageDimensionsTable.Columns.Add("Timestamp", typeof(DateTime));

                // Iterate through each page and capture its dimensions
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    DataRow row = pageDimensionsTable.NewRow();
                    row["RevisionId"] = revisionId;
                    row["PageId"] = page.ID;
                    row["PageWidthInches"] = width;
                    row["PageHeightInches"] = height;
                    row["Timestamp"] = DateTime.UtcNow;
                    pageDimensionsTable.Rows.Add(row);
                }

                // ----- Real database insertion example (commented out) -----
                // The following illustrates how you would insert the captured data
                // into an actual relational database using ADO.NET.
                // Replace the connection string and provider as appropriate.
                /*
                string connectionString = "your_connection_string_here";
                using (DbConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataRow row in pageDimensionsTable.Rows)
                    {
                        using (DbCommand command = connection.CreateCommand())
                        {
                            command.CommandText = @"
                                INSERT INTO PageDimensions (RevisionId, PageId, PageWidthInches, PageHeightInches, Timestamp)
                                VALUES (@RevisionId, @PageId, @Width, @Height, @Timestamp)";
                            var pRevision = command.CreateParameter();
                            pRevision.ParameterName = "@RevisionId";
                            pRevision.Value = row["RevisionId"];
                            command.Parameters.Add(pRevision);

                            var pPageId = command.CreateParameter();
                            pPageId.ParameterName = "@PageId";
                            pPageId.Value = row["PageId"];
                            command.Parameters.Add(pPageId);

                            var pWidth = command.CreateParameter();
                            pWidth.ParameterName = "@Width";
                            pWidth.Value = row["PageWidthInches"];
                            command.Parameters.Add(pWidth);

                            var pHeight = command.CreateParameter();
                            pHeight.ParameterName = "@Height";
                            pHeight.Value = row["PageHeightInches"];
                            command.Parameters.Add(pHeight);

                            var pTimestamp = command.CreateParameter();
                            pTimestamp.ParameterName = "@Timestamp";
                            pTimestamp.Value = row["Timestamp"];
                            command.Parameters.Add(pTimestamp);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                */
                // -----------------------------------------------------------

                // For demonstration, output the captured data to the console
                Console.WriteLine("Captured page dimensions for revision {0}:", revisionId);
                foreach (DataRow row in pageDimensionsTable.Rows)
                {
                    Console.WriteLine("Page ID: {0}, Width: {1} in, Height: {2} in, Timestamp: {3}",
                        row["PageId"], row["PageWidthInches"], row["PageHeightInches"], row["Timestamp"]);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }