using System;
using System.IO;
using System.Text;
using System.Net.Http;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HttpResponseStreamProvider : IStreamProvider
{
    private readonly Func<Stream> _streamFactory;

    public HttpResponseStreamProvider(Func<Stream> streamFactory)
    {
        _streamFactory = streamFactory;
    }

    // Called by Aspose.Diagram before writing data
    public void InitStream(StreamProviderOptions options)
    {
        // Provide the HTTP response stream to the save operation
        options.Stream = _streamFactory();
    }

    // Called by Aspose.Diagram after writing data
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure all data is flushed; do not close the response stream here
        options.Stream?.Flush();
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load a diagram (replace with your actual file path)
            string diagramPath = "sample.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Simulate an HTTP response body using a memory stream
            using (MemoryStream responseStream = new MemoryStream())
            {
                // Create the custom stream provider that returns the response stream
                IStreamProvider provider = new HttpResponseStreamProvider(() => responseStream);

                // Configure HTML save options and assign the stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML; the filename is ignored when using StreamProvider
                diagram.Save("ignored.html", htmlOptions);

                // Retrieve the generated HTML from the memory stream
                responseStream.Position = 0;
                string htmlContent = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();

                // Simulate sending the HTML via an HTTP response
                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(htmlContent, Encoding.UTF8, "text/html");

                // Output the result length to the console
                Console.WriteLine($"HTML content length: {htmlContent.Length}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}