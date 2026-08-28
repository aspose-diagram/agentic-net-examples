using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ProgressStreamProvider : IStreamProvider
{
    private readonly int _total;
    private int _current;

    public ProgressStreamProvider(int total)
    {
        _total = total;
        _current = 0;
    }

    // Called by Aspose.Diagram when it needs a stream for a part of the export
    public void InitStream(StreamProviderOptions options)
    {
        // Update progress counter
        _current++;

        // Simple console progress bar
        Console.Write($"\rSaving streams: {_current}/{_total}");

        // Provide a stream for the exporter (using MemoryStream here)
        options.Stream = new MemoryStream();
    }

    // Called when the exporter finishes writing to the stream
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream to release resources
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Determine how many streams will be requested (e.g., one per page)
            int totalStreams = diagram.Pages.Count;

            // Configure HTML save options with the custom progress stream provider
            HTMLSaveOptions saveOptions = new HTMLSaveOptions
            {
                StreamProvider = new ProgressStreamProvider(totalStreams)
            };

            // Save the diagram to HTML; the progress bar updates on each stream request
            diagram.Save("output.html", saveOptions);

            // Move to the next line after progress output
            Console.WriteLine();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}