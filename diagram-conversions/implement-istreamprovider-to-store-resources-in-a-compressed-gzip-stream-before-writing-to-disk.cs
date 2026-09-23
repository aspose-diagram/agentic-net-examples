using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class GzipStreamProvider : IStreamProvider
{
    // Called before a resource stream is created.
    public void InitStream(StreamProviderOptions options)
    {
        // Ensure the directory for the target file exists.
        string directory = Path.GetDirectoryName(options.DefaultPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream for the resource and wrap it with GZip compression.
        FileStream fileStream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
        options.Stream = gzipStream;
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream (which also disposes the underlying file stream).
        options.Stream?.Dispose();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio diagram.
            string inputDiagramPath = "input.vsdx";

            // Path where the HTML file will be created.
            string outputHtmlPath = "output.html";

            // Load the diagram.
            Diagram diagram = new Diagram(inputDiagramPath);

            // Configure HTML save options and assign the custom GZIP stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new GzipStreamProvider();

            // Save the diagram as HTML; resources will be written through the GZIP provider.
            diagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine("Diagram exported to HTML with compressed resources.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}