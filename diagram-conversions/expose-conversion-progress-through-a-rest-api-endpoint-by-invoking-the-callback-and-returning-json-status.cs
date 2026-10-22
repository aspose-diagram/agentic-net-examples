using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Initialize and start a simple HTTP listener on localhost:5000
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
        try
        {
            listener.Start();
            Console.WriteLine("Listening on http://localhost:5000/ ...");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to start HttpListener: {ex.Message}");
            return;
        }

        // Continuously handle incoming requests
        while (true)
        {
            HttpListenerContext context = null;
            try
            {
                context = listener.GetContext();
            }
            catch (HttpListenerException)
            {
                // Listener was stopped; exit loop
                break;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error receiving request: {ex.Message}");
                continue;
            }

            // Process each request in a separate method for clarity
            ProcessRequest(context);
        }

        // Clean up
        listener.Stop();
    }

    /// <summary>
    /// Handles an incoming HTTP request, performs Visio to PDF conversion,
    /// tracks progress via IPageSavingCallback, and returns JSON status.
    /// </summary>
    private static void ProcessRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        response.ContentType = "application/json";
        response.ContentEncoding = Encoding.UTF8;

        // Only support GET method for conversion endpoint
        if (request.HttpMethod != "GET")
        {
            WriteResponse(response, JsonSerializer.Serialize(new { error = "Only GET method is supported." }), 405);
            return;
        }

        // Expect path "/convert"
        if (!request.Url.AbsolutePath.Equals("/convert", StringComparison.OrdinalIgnoreCase))
        {
            WriteResponse(response, JsonSerializer.Serialize(new { error = "Invalid endpoint. Use /convert." }), 404);
            return;
        }

        // Parse query parameters: input (Visio file) and output (PDF file)
        string inputPath = request.QueryString["input"];
        string outputPath = request.QueryString["output"];

        if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
        {
            WriteResponse(response, JsonSerializer.Serialize(new { error = "Missing 'input' or 'output' query parameters." }), 400);
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            WriteResponse(response, JsonSerializer.Serialize(new { error = "Input file does not exist." }), 400);
            return;
        }

        // Prepare progress tracking object
        ConversionProgress progress = new ConversionProgress
        {
            CurrentPage = 0,
            TotalPages = 0,
            Status = "NotStarted"
        };

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options with the custom page saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PdfPageSavingCallback(progress);

            // Perform the conversion (this may take time; progress is updated via callback)
            diagram.Save(outputPath, pdfOptions);

            // After successful save, update status
            progress.Status = "Completed";
        }
        catch (Exception ex)
        {
            // Log error and return JSON error response
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
            WriteResponse(response, JsonSerializer.Serialize(new { error = "Conversion failed.", details = ex.Message }), 500);
            return;
        }

        // Return final progress as JSON
        string jsonResult = JsonSerializer.Serialize(new
        {
            input = inputPath,
            output = outputPath,
            progress.CurrentPage,
            progress.TotalPages,
            progress.Status
        });

        WriteResponse(response, jsonResult, 200);
    }

    /// <summary>
    /// Writes a JSON string to the HTTP response with the specified status code.
    /// </summary>
    private static void WriteResponse(HttpListenerResponse response, string content, int statusCode)
    {
        response.StatusCode = statusCode;
        byte[] buffer = Encoding.UTF8.GetBytes(content);
        response.ContentLength64 = buffer.Length;
        using (Stream output = response.OutputStream)
        {
            output.Write(buffer, 0, buffer.Length);
        }
    }
}

/// <summary>
/// Simple POCO to hold conversion progress information.
/// </summary>
class ConversionProgress
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string Status { get; set; }
}

/// <summary>
/// Implementation of IPageSavingCallback to receive per-page save events during PDF export.
/// Updates the shared ConversionProgress instance.
/// </summary>
class PdfPageSavingCallback : IPageSavingCallback
{
    private readonly ConversionProgress _progress;

    public PdfPageSavingCallback(ConversionProgress progress)
    {
        _progress = progress;
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // PageIndex is zero‑based; convert to 1‑based for user friendliness
        _progress.CurrentPage = args.PageIndex + 1;
        _progress.TotalPages = args.PageCount;
        _progress.Status = "Saving";
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        // If this is the last page, mark as completed
        if (args.PageIndex == args.PageCount - 1)
        {
            _progress.Status = "Completed";
        }
        else
        {
            // Otherwise, keep status as Saving for next page
            _progress.Status = "Saving";
        }

        // No need to modify args.HasMorePages; let the process continue normally
    }
}