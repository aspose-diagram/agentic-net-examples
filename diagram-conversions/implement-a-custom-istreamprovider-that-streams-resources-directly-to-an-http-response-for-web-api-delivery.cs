using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HttpResponseStreamProvider : IStreamProvider
{
    private readonly Stream _responseStream;

    public HttpResponseStreamProvider(Stream responseStream)
    {
        _responseStream = responseStream ?? throw new ArgumentNullException(nameof(responseStream));
    }

    // Called by Aspose.Diagram when it needs to write a resource (e.g., images, CSS) during HTML export.
    public void InitStream(StreamProviderOptions options)
    {
        // Direct the output to the HTTP response stream.
        options.Stream = _responseStream;
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // No additional cleanup required; the framework will handle flushing/closing the response.
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            var diagram = new Diagram("sample.vsdx");

            // Configure HTML export options.
            var htmlOptions = new HTMLSaveOptions();

            // In a real ASP.NET Core controller you would pass HttpContext.Response.Body.
            // For this console example we stream to a file that represents the HTTP response body.
            using var responseStream = new FileStream("output.html", FileMode.Create, FileAccess.Write);
            htmlOptions.StreamProvider = new HttpResponseStreamProvider(responseStream);

            // Export the diagram to HTML; resources are streamed via the custom provider.
            diagram.Save("placeholder.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}