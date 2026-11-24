using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Simulated audit logger. Replace with real ADO.NET code as needed.
    private static class AuditLogger
    {
        private static readonly DataTable _logTable = CreateLogTable();

        private static DataTable CreateLogTable()
        {
            var table = new DataTable("PageAudit");
            table.Columns.Add("PageIndex", typeof(int));
            table.Columns.Add("PageCount", typeof(int));
            table.Columns.Add("Timestamp", typeof(DateTime));
            return table;
        }

        public static void Log(int pageIndex, int pageCount)
        {
            // Add a new row to the in‑memory table.
            var row = _logTable.NewRow();
            row["PageIndex"] = pageIndex;
            row["PageCount"] = pageCount;
            row["Timestamp"] = DateTime.UtcNow;
            _logTable.Rows.Add(row);

            // Real database insertion example (commented out):
            // using (var connection = new SqlConnection("your-connection-string"))
            // {
            //     connection.Open();
            //     using (var command = new SqlCommand(
            //         "INSERT INTO PageAudit (PageIndex, PageCount, Timestamp) VALUES (@PageIndex, @PageCount, @Timestamp)", connection))
            //     {
            //         command.Parameters.AddWithValue("@PageIndex", pageIndex);
            //         command.Parameters.AddWithValue("@PageCount", pageCount);
            //         command.Parameters.AddWithValue("@Timestamp", DateTime.UtcNow);
            //         command.ExecuteNonQuery();
            //     }
            // }
        }

        // For demonstration purposes: display the logged rows.
        public static void PrintLog()
        {
            Console.WriteLine("Audit Log:");
            foreach (DataRow row in _logTable.Rows)
            {
                Console.WriteLine($"Page {row["PageIndex"]} of {row["PageCount"]} logged at {row["Timestamp"]}");
            }
        }
    }

    // Custom callback to capture page saving events.
    private class PageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Log the start of a page save.
            Console.WriteLine($"Start saving page index {args.PageIndex} of pages {args.PageCount}");
            // Optionally, log to the audit table here as well.
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Log the end of a page save.
            Console.WriteLine($"End saving page index {args.PageIndex} of pages {args.PageCount}");
            // Record the page index and total page count to the audit log.
            AuditLogger.Log(args.PageIndex, args.PageCount);
        }
    }

    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];

        // Guard against missing file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(visioPath);

            // Prepare PDF save options with the custom callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageSavingCallback();

            // Define output PDF path.
            string outputPdf = Path.ChangeExtension(visioPath, ".pdf");

            // Save the diagram to PDF, triggering the callback for each page.
            diagram.Save(outputPdf, pdfOptions);

            // After saving, display the audit log.
            AuditLogger.PrintLog();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}