using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Initialize the stream for a given resource during HTML export.
    public void InitStream(StreamProviderOptions options)
    {
        // Determine a folder to store the resources (images, shapes, etc.).
        string resourcesFolder = "Resources";

        // Ensure the folder exists.
        if (!Directory.Exists(resourcesFolder))
        {
            Directory.CreateDirectory(resourcesFolder);
        }

        // Use the default path (resource name) to create a file name.
        string fileName = Path.GetFileName(options.DefaultPath);

        // Combine folder and file name to get the full path.
        string fullPath = Path.Combine(resourcesFolder, fileName);

        // Create a file stream for writing the resource.
        FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

        // Assign the created stream to the options so Aspose can write into it.
        options.Stream = fileStream;
    }

    // Close the stream after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Safely close and dispose the stream if it was created.
        if (options.Stream != null)
        {
            options.Stream.Close();
            options.Stream.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with a custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new MyStreamProvider();

            // Define the output HTML file path.
            string outputHtml = "output.html";

            // Save the diagram to HTML; resources will be streamed via MyStreamProvider.
            diagram.Save(outputHtml, htmlOptions);

            Console.WriteLine($"Diagram successfully exported to '{outputHtml}'.");
            Console.WriteLine("Associated resources are stored in the 'Resources' folder.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}