using System;
using Aspose.Diagram;
using System.Data.SqlClient;

class Program
    {
        // Entry point of the console application
        static void Main(string[] args)
        {
            // Expect two arguments: path to the Visio file and a revision identifier
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPageTracking <VisioFilePath> <RevisionId>");
                return;
            }

            string visioPath = args[0];
            if (!int.TryParse(args[1], out int revisionId))
            {
                Console.WriteLine("RevisionId must be an integer.");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Connection string to the relational database (adjust as needed)
            const string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;";

            // Iterate through each page and store its dimensions
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;
                string pageName = page.Name ?? string.Empty;
                int pageId = page.ID;

                // Insert dimensions into the database
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string insertSql = @"
                            INSERT INTO PageDimensions
                            (RevisionId, PageId, PageName, WidthInches, HeightInches, RecordedAt)
                            VALUES
                            (@RevisionId, @PageId, @PageName, @Width, @Height, @RecordedAt)";

                        using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@RevisionId", revisionId);
                            cmd.Parameters.AddWithValue("@PageId", pageId);
                            cmd.Parameters.AddWithValue("@PageName", pageName);
                            cmd.Parameters.AddWithValue("@Width", width);
                            cmd.Parameters.AddWithValue("@Height", height);
                            cmd.Parameters.AddWithValue("@RecordedAt", DateTime.Now);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine($"Recorded Page {pageId} ('{pageName}') - Width: {width} in, Height: {height} in");
                }
                catch (Exception dbEx)
                {
                    Console.WriteLine($"Database error for page {pageId}: {dbEx.Message}");
                }
            }
        }
    }