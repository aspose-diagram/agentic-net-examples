using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class LoggingStreamProvider : IStreamProvider
{
    // Called by Aspose before writing a resource (e.g., image, CSS) to a stream.
    public void InitStream(StreamProviderOptions options)
    {
        // Provide a fresh memory stream for the resource.
        options.Stream = new MemoryStream();
    }

    // Called by Aspose after the resource stream has been closed.
    public void CloseStream(StreamProviderOptions options)
    {
        Stream stream = options.Stream;
        if (stream != null)
        {
            long size = stream.Length;
            // Log the resource path and its final size for auditing.
            Console.WriteLine($"Resource '{options.DefaultPath}' closed. Size: {size} bytes.");
            stream.Dispose();
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            using (Diagram diagram = new Diagram("sample.vsdx"))
            {
                // Set up HTML export options and attach the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new LoggingStreamProvider();

                // Export the diagram to HTML; each resource stream will be logged.
                diagram.Save("output.html", htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}