using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // This method is called by Aspose.Diagram when it needs to create a stream for a resource (e.g., image) during HTML export.
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Extract the original file name from the default path.
            string originalFileName = Path.GetFileName(options.DefaultPath);

            // Create a timestamp prefix to guarantee uniqueness.
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Build a new file name with the timestamp.
            string newFileName = $"{timestamp}_{originalFileName}";

            // Determine the directory where the resource should be saved.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (string.IsNullOrEmpty(directory))
            {
                directory = Directory.GetCurrentDirectory();
            }

            // Ensure the directory exists.
            Directory.CreateDirectory(directory);

            // Combine directory and new file name.
            string fullPath = Path.Combine(directory, newFileName);

            // Assign a writable file stream to the options.
            options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing stream for resource '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }

    // This method is called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            options.Stream?.Close();
            options.Stream = null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error closing stream for resource '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path.
        string outputHtmlPath = "output.html";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new CustomStreamProvider();

            // Save the diagram as HTML. Resources (images, etc.) will be written using the stream provider.
            diagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to HTML: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}