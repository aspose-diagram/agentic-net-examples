using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ConsoleProgressStreamProvider : IStreamProvider
{
    private int _currentCount = 0;
    private readonly int _expectedCount;

    public ConsoleProgressStreamProvider(int expectedCount)
    {
        _expectedCount = expectedCount > 0 ? expectedCount : 1;
    }

    // Called by Aspose.Diagram when a new stream is required (e.g., for an image file during HTML export)
    public void InitStream(StreamProviderOptions options)
    {
        _currentCount++;
        DrawProgress(_currentCount, _expectedCount);

        // Provide a writable stream for the resource. Using MemoryStream as a placeholder.
        options.Stream = new MemoryStream();
    }

    // Called after the stream has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure the stream is properly disposed.
        options.Stream?.Dispose();
    }

    private void DrawProgress(int current, int total)
    {
        const int barWidth = 50;
        double ratio = (double)current / total;
        int filled = (int)(barWidth * ratio);

        Console.Write("\r[");
        Console.Write(new string('#', filled));
        Console.Write(new string('-', barWidth - filled));
        Console.Write($"] {current}/{total}");

        if (current >= total)
        {
            Console.WriteLine();
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
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare HTML export options and attach the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // Estimate the number of resources (images, CSS files, etc.) that will be generated.
            // Adjust this number based on the actual diagram complexity.
            htmlOptions.StreamProvider = new ConsoleProgressStreamProvider(expectedCount: 10);

            // Export the diagram to HTML. The progress bar updates each time a stream is requested.
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}