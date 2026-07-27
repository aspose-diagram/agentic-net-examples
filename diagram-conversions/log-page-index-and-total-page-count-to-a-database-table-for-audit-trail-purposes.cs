using System;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramAuditExample
{
    // Implements the page saving callback to capture page index and total page count.
    public class AuditPageSavingCallback : IPageSavingCallback
    {
        private readonly string _connectionString;
        private readonly string _auditTableName;

        public AuditPageSavingCallback(string connectionString, string auditTableName = "DiagramPageAudit")
        {
            _connectionString = connectionString;
            _auditTableName = auditTableName;
        }

        // Called when a page starts to be saved.
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log the page index (zero‑based) and total page count.
            LogPageInfo(args.PageIndex, args.PageCount);
        }

        // Called when a page finishes saving. Not used for logging in this example.
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No action required here for audit logging.
        }

        private void LogPageInfo(int pageIndex, int pageCount)
        {
            // Example INSERT statement; adjust column names/types to match your schema.
            const string insertSql = @"
                INSERT INTO {0} (DiagramId, PageIndex, PageCount, LoggedAt)
                VALUES (@DiagramId, @PageIndex, @PageCount, @LoggedAt);";

            // Replace placeholder with actual table name safely.
            string sql = string.Format(insertSql, _auditTableName);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Example parameters – you may need to supply DiagramId from your context.
                cmd.Parameters.Add("@DiagramId", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid(); // replace with real ID
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                cmd.Parameters.Add("@PageCount", SqlDbType.Int).Value = pageCount;
                cmd.Parameters.Add("@LoggedAt", SqlDbType.DateTime).Value = DateTime.UtcNow;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file.
                string diagramPath = @"C:\Diagrams\Sample.vsdx";

                // Load the diagram using Aspose.Diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Prepare save options (e.g., PDF) – adjust as needed.
                PdfSaveOptions saveOptions = new PdfSaveOptions
                {
                    // Attach the custom callback to capture page information.
                    PageSavingCallback = new AuditPageSavingCallback(
                        connectionString: @"Data Source=SERVER;Initial Catalog=AuditDb;Integrated Security=True")
                };

                // Save the diagram; the callback will be invoked for each page.
                string outputPath = @"C:\Diagrams\Sample_Output.pdf";
                diagram.Save(outputPath, saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}