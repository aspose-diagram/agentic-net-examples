using System;
using System.IO;
using System.Net;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HttpResponseStreamProvider : IStreamProvider
{
    private readonly HttpListenerResponse _response;

    public HttpResponseStreamProvider(HttpListenerResponse response)
    {
        _response = response;
    }

    // Assign the HTTP response output stream to the options
    public void InitStream(StreamProviderOptions options)
    {
        options.Stream = _response.OutputStream;
    }

    // Flush the stream after writing; do not close the response here
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Flush();
    }
}

class Program
{
    static void Main()
    {
        // Simple HTTP listener that serves the HTML export of a Visio diagram
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();
        Console.WriteLine("Listening on http://localhost:8080/ ...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext(); // wait for request
            try
            {
                // Load the diagram (adjust the path as needed)
                string diagramPath = "sample.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML save options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new HttpResponseStreamProvider(context.Response);
                htmlOptions.Title = "Exported Diagram";

                // Set response headers for HTML content
                context.Response.ContentType = "text/html";

                // Save the diagram directly to the HTTP response stream
                diagram.Save(context.Response.OutputStream, htmlOptions);

                // Close the response stream
                context.Response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                // Return error information
                context.Response.StatusCode = 500;
                using (StreamWriter writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write($"Error: {ex.Message}");
                }
                context.Response.OutputStream.Close();
            }
        }

        // listener.Stop(); // Unreachable in this example
    }
}