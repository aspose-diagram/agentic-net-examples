using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Example invocation
        string diagramPath = "sample.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Simulate an HTTP response using a MemoryStream for demonstration.
        using var responseStream = new MemoryStream();
        var exporter = new DiagramExporter();
        exporter.ExportDiagramAsHtml(diagramPath, responseStream);
        // The responseStream now contains the HTML output.
    }
}

// Custom IStreamProvider that writes diagram resources directly to a provided Stream.
public class StreamProvider : IStreamProvider
{
    private readonly Stream _outputStream;

    public StreamProvider(Stream outputStream)
    {
        _outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
    }

    public void InitStream(StreamProviderOptions options)
    {
        // Assign the provided stream to the options.
        options.Stream = _outputStream;
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // No special cleanup required.
    }
}

// Service class handling diagram export.
public class DiagramExporter
{
    public void ExportDiagramAsHtml(string diagramFilePath, Stream outputStream)
    {
        if (!File.Exists(diagramFilePath))
        {
            Console.Error.WriteLine($"File not found: {diagramFilePath}");
            return;
        }

        try
        {
            var diagram = new Diagram(diagramFilePath);

            var htmlOptions = new HTMLSaveOptions
            {
                StreamProvider = new StreamProvider(outputStream)
            };

            // Save the diagram; Aspose will stream the HTML directly to the provided stream.
            diagram.Save(outputStream, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}