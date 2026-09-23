using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AuditingStreamProvider : IStreamProvider
{
    // Called when Aspose.Diagram needs a stream for a resource (e.g., an image) during HTML export.
    public void InitStream(StreamProviderOptions options)
    {
        // If Aspose did not supply a stream, create one using the default path.
        if (options.Stream == null && !string.IsNullOrEmpty(options.DefaultPath))
        {
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }
    }

    // Called after the resource stream has been written and closed.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            // Capture the size before disposing the stream.
            long size = options.Stream.Length;
            Console.WriteLine($"Closed resource '{options.DefaultPath}'. Size = {size} bytes.");
            options.Stream.Dispose();
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
            string inputPath = "sample.vsdx"; // replace with your file path
            Diagram diagram = new Diagram(inputPath);

            // Set up HTML export options and attach the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new AuditingStreamProvider();

            // Export the diagram to HTML. All resource streams will be logged when closed.
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}