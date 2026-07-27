using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class TempFolderStreamProvider : IStreamProvider
{
    private readonly string _tempFolder;

    public TempFolderStreamProvider()
    {
        // Create a unique temporary folder for this conversion
        _tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempFolder);
    }

    // Called by Aspose.Diagram when a resource stream is needed
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the temporary folder with the default path provided by the options
        // DefaultPath is read‑only and contains the relative file name (e.g., "images/img1.png")
        string filePath = Path.Combine(_tempFolder, options.DefaultPath);

        // Ensure the directory for the file exists
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // Assign a writable FileStream to the options
        options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Called after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Dispose();
        options.Stream = null;
    }

    // Cleanup method to delete the temporary folder after conversion
    public void Cleanup()
    {
        if (Directory.Exists(_tempFolder))
        {
            try
            {
                Directory.Delete(_tempFolder, true);
            }
            catch
            {
                // Ignored – folder may be in use or already deleted
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load a diagram (replace with your actual file path)
            string inputPath = "sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options with the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            TempFolderStreamProvider streamProvider = new TempFolderStreamProvider();
            htmlOptions.StreamProvider = streamProvider;

            // Save the diagram to HTML; resources (images, CSS, etc.) will be written to the temp folder
            string outputHtml = "output.html";
            diagram.Save(outputHtml, htmlOptions);

            // Clean up temporary resources
            streamProvider.Cleanup();

            Console.WriteLine($"Diagram exported to {outputHtml}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}