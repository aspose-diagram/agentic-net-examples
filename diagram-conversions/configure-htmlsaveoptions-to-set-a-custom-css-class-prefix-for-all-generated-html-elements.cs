using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputHtmlPath = "output.html";

        // Directory where HTML resources (images, CSS, etc.) will be written
        string resourceDir = "html_resources";

        // Ensure the resource directory exists
        if (!Directory.Exists(resourceDir))
        {
            Directory.CreateDirectory(resourceDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Export only visible pages (do not export hidden pages)
                ExportHiddenPage = false,

                // Assign a custom stream provider to control where resource files are saved
                StreamProvider = new CustomStreamProvider(resourceDir)
            };

            // Save the diagram as HTML using the configured options
            diagram.Save(outputHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Custom implementation of IStreamProvider to direct HTML resources to a specific folder
class CustomStreamProvider : IStreamProvider
{
    private readonly string _basePath;

    public CustomStreamProvider(string basePath)
    {
        _basePath = basePath;
    }

    // Called by Aspose.Diagram when a resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base path with the default resource file name
        string filePath = Path.Combine(_basePath, options.DefaultPath);

        // Ensure the directory for the file exists
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Create a writable file stream for the resource
        options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        // Close and dispose the stream if it was created
        options.Stream?.Close();
        options.Stream?.Dispose();
    }
}
