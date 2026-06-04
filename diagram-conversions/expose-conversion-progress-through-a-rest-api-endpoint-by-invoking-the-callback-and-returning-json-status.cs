using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PdfProgressCallback : IPageSavingCallback
{
    private readonly string _requestId;
    private static readonly ConcurrentDictionary<string, int> ProgressMap = new();

    public PdfProgressCallback(string requestId)
    {
        _requestId = requestId;
        ProgressMap[_requestId] = 0;
    }

    public void PageStartSaving(PageStartSavingArgs args)
    {
        // No action needed at start of a page.
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        int progress = (int)(((args.PageIndex + 1) / (double)args.PageCount) * 100);
        ProgressMap[_requestId] = progress;
    }

    public static int GetProgress(string requestId)
    {
        return ProgressMap.TryGetValue(requestId, out var value) ? value : 0;
    }

    public static void Remove(string requestId)
    {
        ProgressMap.TryRemove(requestId, out _);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        const string prefix = "http://localhost:5000/";
        using var listener = new HttpListener();
        listener.Prefixes.Add(prefix);
        try
        {
            listener.Start();
            Console.WriteLine($"Listening on {prefix}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to start HttpListener: {ex.Message}");
            return;
        }

        while (true)
        {
            HttpListenerContext context;
            try
            {
                context = listener.GetContext();
            }
            catch (HttpListenerException)
            {
                break; // Listener stopped.
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Listener error: {ex.Message}");
                continue;
            }

            ProcessRequest(context);
        }
    }

    private static void ProcessRequest(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;

        if (request.HttpMethod != "GET" || request.Url?.AbsolutePath != "/convert")
        {
            response.StatusCode = 404;
            response.Close();
            return;
        }

        string inputPath = request.QueryString["input"];
        string outputPath = request.QueryString["output"];
        string requestId = request.QueryString["id"];

        if (string.IsNullOrWhiteSpace(inputPath) ||
            string.IsNullOrWhiteSpace(outputPath) ||
            string.IsNullOrWhiteSpace(requestId))
        {
            response.StatusCode = 400;
            WriteResponse(response, "Missing required query parameters: input, output, id");
            return;
        }

        if (!File.Exists(inputPath))
        {
            response.StatusCode = 404;
            WriteResponse(response, $"Input file not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PdfProgressCallback(requestId);

            diagram.Save(outputPath, pdfOptions);

            int finalProgress = PdfProgressCallback.GetProgress(requestId);
            PdfProgressCallback.Remove(requestId);

            var responseObj = new
            {
                id = requestId,
                status = "Completed",
                progress = finalProgress,
                output = outputPath
            };

            response.ContentType = "application/json";
            WriteResponse(response, JsonSerializer.Serialize(responseObj));
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            var errorObj = new { id = requestId, status = "Error", message = ex.Message };
            WriteResponse(response, JsonSerializer.Serialize(errorObj));
        }
    }

    private static void WriteResponse(HttpListenerResponse response, string content)
    {
        using var writer = new StreamWriter(response.OutputStream);
        writer.Write(content);
        response.Close();
    }
}