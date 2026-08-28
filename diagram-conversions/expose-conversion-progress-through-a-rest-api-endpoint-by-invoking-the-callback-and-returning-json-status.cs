using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversionService
{
    // Holds conversion progress information
    public class ConversionProgress
    {
        public int TotalPages { get; set; }
        public int ProcessedPages { get; set; }
        public bool IsCompleted { get; set; }
        public string Message { get; set; }
    }

    // Callback implementation for PDF page saving events
    public class PdfPageSavingCallback : IPageSavingCallback
    {
        private readonly ConversionProgress _progress;

        public PdfPageSavingCallback(ConversionProgress progress)
        {
            _progress = progress;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            // No action needed at start of page saving
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            // Update progress after each page is saved
            _progress.ProcessedPages = args.PageIndex + 1; // PageIndex is zero‑based
            if (_progress.ProcessedPages >= _progress.TotalPages)
            {
                _progress.IsCompleted = true;
                _progress.Message = "Conversion completed.";
            }
        }
    }

    class Program
    {
        // Shared progress instance
        private static readonly ConversionProgress progress = new ConversionProgress();

        static void Main(string[] args)
        {
            const string prefix = "http://localhost:5000/";
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(prefix);
                listener.Start();
                Console.WriteLine($"Listening on {prefix}");

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    try
                    {
                        ProcessRequest(context);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        SendJsonResponse(context.Response, new { error = ex.Message }, HttpStatusCode.InternalServerError);
                    }
                }
            }
        }

        private static void ProcessRequest(HttpListenerContext context)
        {
            string path = context.Request.Url.AbsolutePath.ToLowerInvariant();

            if (path == "/convert")
            {
                HandleConvert(context);
            }
            else if (path == "/status")
            {
                HandleStatus(context);
            }
            else
            {
                SendJsonResponse(context.Response, new { error = "Invalid endpoint." }, HttpStatusCode.NotFound);
            }
        }

        private static void HandleConvert(HttpListenerContext context)
        {
            // Reset progress
            progress.TotalPages = 0;
            progress.ProcessedPages = 0;
            progress.IsCompleted = false;
            progress.Message = "Conversion started.";

            // Path to input Visio file (adjust as needed)
            string inputPath = "sample.vsdx";
            // Path to output PDF file
            string outputPath = "output.pdf";

            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"Input file not found: {inputPath}");
            }

            // Load diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine total pages for progress reporting
                progress.TotalPages = diagram.Pages.Count;

                // Configure PDF save options with callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new PdfPageSavingCallback(progress);

                // Perform save (this will trigger the callback)
                diagram.Save(outputPath, pdfOptions);
            }

            // Return final status as JSON
            SendJsonResponse(context.Response, new
            {
                status = "success",
                totalPages = progress.TotalPages,
                processedPages = progress.ProcessedPages,
                message = progress.Message,
                version = GetDiagramVersion()
            }, HttpStatusCode.OK);
        }

        private static void HandleStatus(HttpListenerContext context)
        {
            SendJsonResponse(context.Response, new
            {
                totalPages = progress.TotalPages,
                processedPages = progress.ProcessedPages,
                isCompleted = progress.IsCompleted,
                message = progress.Message
            }, HttpStatusCode.OK);
        }

        private static void SendJsonResponse(HttpListenerResponse response, object payload, HttpStatusCode statusCode)
        {
            string json = JsonSerializer.Serialize(payload);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.StatusCode = (int)statusCode;
            response.ContentLength64 = buffer.Length;
            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }
        }

        private static string GetDiagramVersion()
        {
            // Retrieve version from a temporary diagram instance (if needed)
            using (Diagram temp = new Diagram())
            {
                return temp.Version;
            }
        }
    }
}