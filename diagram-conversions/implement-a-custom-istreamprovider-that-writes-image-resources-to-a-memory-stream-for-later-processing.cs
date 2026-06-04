using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class MemoryStreamProvider : IStreamProvider
{
    // Holds the memory stream that will receive the exported data.
    private MemoryStream _memoryStream;

    // Called by Aspose.Diagram when it needs a stream to write a resource (e.g., an image).
    public void InitStream(StreamProviderOptions options)
    {
        // Create a fresh memory stream for each resource.
        _memoryStream = new MemoryStream();

        // Assign the stream to the options so Aspose can write to it.
        options.Stream = _memoryStream;
    }

    // Called after Aspose.Diagram finishes writing to the stream.
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure all data is flushed.
        options.Stream?.Flush();

        // Reset position so the consumer can read from the beginning.
        if (options.Stream != null)
            options.Stream.Position = 0;

        // Do NOT dispose the stream here if the caller needs to read it later.
        // Disposal can be handled externally after the stream is retrieved.
    }

    // Allows external code to obtain the memory stream after the export operation.
    public MemoryStream GetMemoryStream()
    {
        return _memoryStream;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new MemoryStreamProvider();
            obj.InitStream(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
