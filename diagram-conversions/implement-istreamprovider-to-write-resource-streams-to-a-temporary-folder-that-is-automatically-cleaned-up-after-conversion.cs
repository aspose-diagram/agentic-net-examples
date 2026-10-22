using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TempFolderStreamProvider : IStreamProvider
{
    private readonly string _tempFolder;

    public TempFolderStreamProvider(string tempFolder)
    {
        _tempFolder = tempFolder;
    }

    // Called by Aspose.Diagram when it needs to write a resource (image, shape, etc.)
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Ensure the temporary folder exists
            if (!Directory.Exists(_tempFolder))
                Directory.CreateDirectory(_tempFolder);

            // Combine the temporary folder with the default relative path provided by Aspose
            string targetPath = Path.Combine(_tempFolder, options.DefaultPath);

            // Ensure the directory for the target file exists (handles subfolders)
            string targetDir = Path.GetDirectoryName(targetPath);
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            // Assign a writable FileStream to the options so Aspose can write the resource
            options.Stream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing stream for resource '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }

    // Called by Aspose.Diagram after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            options.Stream?.Close();
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
        // Input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputHtmlPath = "output.html";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a unique temporary folder for resource files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramTemp_" + Guid.NewGuid().ToString("N"));

        // Instantiate the custom stream provider
        IStreamProvider streamProvider = new TempFolderStreamProvider(tempFolder);

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = streamProvider;

            // Save the diagram as HTML; resources (images, shapes) will be written to the temp folder
            diagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder and all its contents
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                    Console.WriteLine("Temporary resources cleaned up.");
                }
            }
            catch (Exception cleanupEx)
            {
                Console.Error.WriteLine($"Failed to clean up temporary folder '{tempFolder}': {cleanupEx.Message}");
            }
        }
    }
}