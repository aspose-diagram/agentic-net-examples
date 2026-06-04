using System;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                const string diagramPath = "input.vsdx";

                // Connection string to the audit database
                const string connectionString = "Data Source=SERVER;Initial Catalog=AuditDb;Integrated Security=True";

                // Name of the audit table (must have columns: PageIndex (int), TotalPages (int), LoggedAt (datetime))
                const string auditTable = "DiagramPageAudit";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Total number of pages in the diagram
                    int totalPages = diagram.Pages.Count;

                    // Iterate through each page by index
                    for (int i = 0; i < totalPages; i++)
                    {
                        // Log the current page index and total page count to the database
                        LogPageInfo(i, totalPages, connectionString, auditTable);
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Inserts a record into the audit table with the page index, total page count, and timestamp.
        /// </summary>
        static void LogPageInfo(int pageIndex, int totalPages, string connectionString, string tableName)
        {
            // Build the INSERT statement with parameters to avoid SQL injection
            string sql = $"INSERT INTO {tableName} (PageIndex, TotalPages, LoggedAt) VALUES (@PageIndex, @TotalPages, @LoggedAt)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Define parameters and assign values
                cmd.Parameters.Add("@PageIndex", System.Data.SqlDbType.Int).Value = pageIndex;
                cmd.Parameters.Add("@TotalPages", System.Data.SqlDbType.Int).Value = totalPages;
                cmd.Parameters.Add("@LoggedAt", System.Data.SqlDbType.DateTime).Value = DateTime.UtcNow;

                // Open the connection, execute the command, and close the connection automatically via using
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                // Simple verification – throw if the insert failed
                if (rowsAffected != 1)
                {
                    throw new Exception($"Failed to insert audit record for page {pageIndex}.");
                }
            }
        }
    }