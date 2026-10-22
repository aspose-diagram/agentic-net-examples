using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputHtmlPath = "output.html";

        // Folder where resource files (images, shapes) will be written
        string resourceFolder = "Resources";

        // Ensure the resource folder exists
        if (!Directory.Exists(resourceFolder))
        {
            Directory.CreateDirectory(resourceFolder);
        }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with a custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new MyStreamProvider(resourceFolder);

            // Save the diagram to HTML; resources will be written via the stream provider
            diagram.Save(outputHtmlPath, htmlOptions);
            Console.WriteLine("Diagram successfully saved to HTML.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
        }
    }
}

// Implements IStreamProvider to supply streams for HTML resource files with retry logic
class MyStreamProvider : IStreamProvider
{
    private readonly string _baseFolder;

    // Made public so RetryFileStream can access them
    public const int MaxRetryAttempts = 3;
    public const int RetryDelayMilliseconds = 500;

    public MyStreamProvider(string baseFolder)
    {
        _baseFolder = baseFolder;
    }

    // Called by Aspose when a resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Determine the full path for the resource based on the default path provided by Aspose
        string resourcePath = Path.Combine(_baseFolder, options.DefaultPath);

        // Ensure the directory for the resource exists
        string dir = Path.GetDirectoryName(resourcePath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Create a FileStream and wrap it with retry logic, then assign to the options
        FileStream fileStream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write, FileShare.None);
        options.Stream = new RetryFileStream(fileStream);
    }

    // Called by Aspose after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream if it was created
        options.Stream?.Dispose();
    }
}

// Wrapper stream that retries write operations on transient I/O errors
class RetryFileStream : Stream
{
    private readonly Stream _innerStream;

    public RetryFileStream(Stream innerStream)
    {
        _innerStream = innerStream;
    }

    public override bool CanRead => _innerStream.CanRead;
    public override bool CanSeek => _innerStream.CanSeek;
    public override bool CanWrite => _innerStream.CanWrite;
    public override long Length => _innerStream.Length;

    public override long Position
    {
        get => _innerStream.Position;
        set => _innerStream.Position = value;
    }

    public override void Flush()
    {
        _innerStream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return _innerStream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _innerStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _innerStream.SetLength(value);
    }

    // Write with retry on IOException
    public override void Write(byte[] buffer, int offset, int count)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                _innerStream.Write(buffer, offset, count);
                break; // Success
            }
            catch (IOException ioEx)
            {
                attempt++;
                if (attempt >= MyStreamProvider.MaxRetryAttempts)
                {
                    // Re-throw after max attempts
                    throw new IOException($"Failed to write after {attempt} attempts.", ioEx);
                }
                // Wait before retrying
                Thread.Sleep(MyStreamProvider.RetryDelayMilliseconds);
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _innerStream.Dispose();
        }
        base.Dispose(disposing);
    }
}