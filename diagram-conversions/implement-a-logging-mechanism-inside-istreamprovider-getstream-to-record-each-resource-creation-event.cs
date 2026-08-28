using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class LoggingStreamProvider : IStreamProvider
{
    // Simple logger – replace with any logging framework if desired
    private readonly Action<string> _log;

    public LoggingStreamProvider(Action<string> logger = null)
    {
        _log = logger ?? Console.WriteLine;
    }

    // Called by Aspose.Diagram when a new resource stream is required
    public void InitStream(StreamProviderOptions options)
    {
        // Log the creation of a new resource stream
        _log($"[IStreamProvider] InitStream invoked – creating new stream for resource.");

        // Provide a fresh stream (MemoryStream used here; switch to FileStream if needed)
        options.Stream = new MemoryStream();
    }

    // Called by Aspose.Diagram when the resource stream is no longer needed
    public void CloseStream(StreamProviderOptions options)
    {
        // Log the disposal of the resource stream
        _log($"[IStreamProvider] CloseStream invoked – disposing stream for resource.");

        // Properly dispose and clear the stream reference
        options.Stream?.Dispose();
        options.Stream = null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new LoggingStreamProvider();
            obj.InitStream(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
