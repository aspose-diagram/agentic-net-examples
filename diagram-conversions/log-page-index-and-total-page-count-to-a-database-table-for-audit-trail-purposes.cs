using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare the output PDF path in the same directory.
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + ".pdf");

        // Create an in‑memory DataTable to simulate the audit log.
        DataTable auditLog = new DataTable("AuditLog");
        auditLog.Columns.Add("PageIndex", typeof(int));
        auditLog.Columns.Add("PageCount", typeof(int));
        auditLog.Columns.Add("Timestamp", typeof(DateTime));

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options and assign the custom page‑saving callback.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new AuditCallback(auditLog);

            // Save the diagram to PDF using the configured options.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // After saving, display the audit log entries.
        Console.WriteLine("Audit Log:");
        foreach (DataRow row in auditLog.Rows)
        {
            int pageIndex = (int)row["PageIndex"];
            int pageCount = (int)row["PageCount"];
            DateTime timestamp = (DateTime)row["Timestamp"];
            Console.WriteLine($"Page {pageIndex + 1} of {pageCount} saved at {timestamp:O}");
        }
    }
}

// Custom callback to log page start/end events during PDF export.
class AuditCallback : IPageSavingCallback
{
    private readonly DataTable _logTable;

    public AuditCallback(DataTable logTable)
    {
        _logTable = logTable;
    }

    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page {args.PageIndex + 1} of {args.PageCount}");
        AddLogEntry(args.PageIndex, args.PageCount);
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"End saving page {args.PageIndex + 1} of {args.PageCount}");
        // No additional action needed; the start event already logged the entry.
    }

    // Helper to insert a row into the audit DataTable.
    private void AddLogEntry(int pageIndex, int pageCount)
    {
        DataRow row = _logTable.NewRow();
        row["PageIndex"] = pageIndex;
        row["PageCount"] = pageCount;
        row["Timestamp"] = DateTime.Now;
        _logTable.Rows.Add(row);
    }
}