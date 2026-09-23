using System.IO;
using System;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;

class DiagramAuditLogger
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Get total number of pages in the diagram
            int totalPages = diagram.Pages.Count;

            // Connection string to the audit database (replace with actual values)
            string connectionString = "Data Source=SERVER_NAME;Initial Catalog=AuditDB;Integrated Security=True";

            // Prepare the SQL command for inserting audit records
            const string insertSql = @"
                INSERT INTO AuditLog (PageIndex, TotalPages, LoggedAt)
                VALUES (@PageIndex, @TotalPages, @LoggedAt)";

            // Open a connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Iterate through each page and log its index with the total page count
                foreach (Page page in diagram.Pages)
                {
                    using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                    {
                        // Page index (Visio pages are 1‑based)
                        cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = page.ID;
                        // Total page count (same for all pages)
                        cmd.Parameters.Add("@TotalPages", SqlDbType.Int).Value = totalPages;
                        // Timestamp of the audit entry
                        cmd.Parameters.Add("@LoggedAt", SqlDbType.DateTime).Value = DateTime.UtcNow;

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // No saving of the diagram is required for this audit operation

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
