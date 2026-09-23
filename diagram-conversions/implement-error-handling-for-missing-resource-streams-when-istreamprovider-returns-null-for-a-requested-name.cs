using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    private readonly string _resourceFolder;

    public MyStreamProvider(string resourceFolder)
    {
        _resourceFolder = resourceFolder;
    }

    // Called by Aspose.Diagram when a resource stream is required during HTML export
    public void InitStream(StreamProviderOptions options)
    {
        // options.DefaultPath contains the name of the requested resource (e.g., "image1.png")
        string fullPath = Path.Combine(_resourceFolder, options.DefaultPath);

        if (File.Exists(fullPath))
        {
            // Resource exists – provide the stream to Aspose.Diagram
            options.Stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }
        else
        {
            // Resource missing – throw an informative exception
            throw new FileNotFoundException(
                $"Resource '{options.DefaultPath}' not found for HTML export.",
                fullPath);
        }
    }

    // Called after Aspose.Diagram finishes using the stream
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
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

            // Input Visio file
            string visioPath = "input.vsdx";

            // Output HTML file
            string htmlPath = "output.html";

            // Folder where external resources (images, CSS, etc.) are stored
            string resourceFolder = "Resources";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Set up HTML save options with the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new MyStreamProvider(resourceFolder);

            // Save the diagram as HTML; missing resources will trigger the error handling in MyStreamProvider
            diagram.Save(htmlPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}