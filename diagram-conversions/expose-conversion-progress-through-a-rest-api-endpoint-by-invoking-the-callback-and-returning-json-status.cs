using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class ConversionProgressCallback : IPageSavingCallback
{
    // Total number of pages in the document
    public int TotalPages { get; private set; }

    // Number of pages that have been saved so far
    public int SavedPages { get; private set; }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Capture total page count on first call
        if (TotalPages == 0)
        {
            TotalPages = args.PageCount;
        }
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        SavedPages = args.PageIndex + 1; // PageIndex is zero‑based
        // If all pages are processed, we can stop further processing (optional)
        if (SavedPages >= TotalPages)
        {
            args.HasMorePages = false;
        }
    }
}

public static class ProgressStore
{
    // Holds the latest callback instance for status reporting
    public static ConversionProgressCallback CurrentCallback { get; set; }
}

public class Program
{
    private const string Prefix = "http://localhost:5000/";

    public static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(Prefix);
        listener.Start();
        Console.WriteLine($"Listening on {Prefix}");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            ThreadPool.QueueUserWorkItem(_ => HandleRequest(context));
        }
    }

    private static void HandleRequest(HttpListenerContext context)
    {
        try
        {
            string path = context.Request.Url.AbsolutePath.ToLowerInvariant();

            if (path == "/convert")
            {
                var query = ParseQuery(context.Request.Url.Query);
                if (!query.TryGetValue("input", out string inputPath) ||
                    !query.TryGetValue("output", out string outputPath))
                {
                    WriteJson(context.Response, new { status = "error", message = "Missing input or output parameters." });
                    return;
                }

                // Start conversion on a background thread
                Thread conversionThread = new Thread(() => ConvertDiagramToPdf(inputPath, outputPath));
                conversionThread.IsBackground = true;
                conversionThread.Start();

                WriteJson(context.Response, new { status = "started" });
            }
            else if (path == "/status")
            {
                var callback = ProgressStore.CurrentCallback;
                if (callback == null)
                {
                    WriteJson(context.Response, new { status = "idle" });
                }
                else
                {
                    WriteJson(context.Response, new
                    {
                        status = "running",
                        totalPages = callback.TotalPages,
                        savedPages = callback.SavedPages
                    });
                }
            }
            else
            {
                WriteJson(context.Response, new { status = "error", message = "Unknown endpoint." });
            }
        }
        catch (Exception ex)
        {
            WriteJson(context.Response, new { status = "error", message = ex.Message });
        }
        finally
        {
            context.Response.OutputStream.Close();
        }
    }

    private static void ConvertDiagramToPdf(string inputFile, string outputFile)
    {
        // Load the diagram
        Diagram diagram = new Diagram(inputFile);

        // Prepare PDF save options and attach the progress callback
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        var progressCallback = new ConversionProgressCallback();
        pdfOptions.PageSavingCallback = progressCallback;

        // Store the callback for status queries
        ProgressStore.CurrentCallback = progressCallback;

        // Perform the save operation
        diagram.Save(outputFile, pdfOptions);

        // Reset the stored callback after completion
        ProgressStore.CurrentCallback = null;
    }

    private static void WriteJson(HttpListenerResponse response, object data)
    {
        response.ContentType = "application/json";
        string json = JsonSerializer.Serialize(data);
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
    }

    private static System.Collections.Generic.Dictionary<string, string> ParseQuery(string query)
    {
        var result = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrEmpty(query))
            return result;

        // Trim leading '?'
        if (query.StartsWith("?"))
            query = query.Substring(1);

        string[] pairs = query.Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach (string pair in pairs)
        {
            string[] kv = pair.Split('=', 2);
            if (kv.Length == 2)
            {
                string key = WebUtility.UrlDecode(kv[0]);
                string value = WebUtility.UrlDecode(kv[1]);
                result[key] = value;
            }
        }
        return result;
    }
}