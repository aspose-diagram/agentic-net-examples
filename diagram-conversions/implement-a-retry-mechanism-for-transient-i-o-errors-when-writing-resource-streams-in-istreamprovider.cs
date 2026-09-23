using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RetryStreamProvider : IStreamProvider
{
    private const int MaxRetries = 3;
    private const int BaseDelayMs = 500;

    public void InitStream(StreamProviderOptions options)
    {
        // Path where the resource should be written.
        string path = options.DefaultPath;

        // Ensure the target directory exists.
        string directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        int attempt = 0;
        while (true)
        {
            try
            {
                // Create a writable file stream.
                FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                options.Stream = stream;
                break; // Success.
            }
            catch (IOException ex) when (IsTransient(ex) && attempt < MaxRetries)
            {
                // Transient I/O error – wait and retry.
                attempt++;
                Thread.Sleep(BaseDelayMs * attempt);
            }
        }
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream if it was created.
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }

    // Simple check: treat all IOExceptions as transient for this example.
    private bool IsTransient(IOException ex)
    {
        return true;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file).
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Set up HTML export with the retry-enabled stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new RetryStreamProvider();

            // Export the diagram to HTML.
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}