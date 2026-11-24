using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TempFolderStreamProvider : IStreamProvider
{
    private readonly string _tempFolder;
    private readonly List<string> _createdFiles = new List<string>();

    public TempFolderStreamProvider(string tempFolder)
    {
        _tempFolder = tempFolder;
        if (!Directory.Exists(_tempFolder))
            Directory.CreateDirectory(_tempFolder);
    }

    // Called by Aspose.Diagram when a resource stream needs to be created.
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the temporary folder with the default relative path provided by Aspose.
        string filePath = Path.Combine(_tempFolder, options.DefaultPath);
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create a file stream for writing the resource.
        FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        options.Stream = fileStream;

        // Keep track of the file for later cleanup.
        _createdFiles.Add(filePath);
    }

    // Called after Aspose.Diagram finishes writing to the stream.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Dispose();
            options.Stream = null;
        }
        // Files will be removed when the whole temporary folder is deleted.
    }

    // Deletes the temporary folder and all its contents.
    public void Cleanup()
    {
        if (Directory.Exists(_tempFolder))
        {
            try
            {
                Directory.Delete(_tempFolder, true);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete temporary folder '{_tempFolder}': {ex.Message}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a unique temporary folder for resource streams.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramTemp_" + Guid.NewGuid().ToString());

        // Instantiate the custom stream provider.
        var streamProvider = new TempFolderStreamProvider(tempFolder);

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options to use the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = streamProvider;

            // Save the diagram as HTML.
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder and all generated resource files.
            streamProvider.Cleanup();
        }
    }
}