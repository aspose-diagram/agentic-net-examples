using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class TempFolderStreamProvider : IStreamProvider
{
    private readonly string _tempFolder;

    public TempFolderStreamProvider()
    {
        // Create a unique temporary folder for this conversion.
        _tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempFolder);
    }

    // Called by Aspose.Diagram when a resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // The DefaultPath property contains the original resource name (e.g., image file name).
        // Build a full path inside the temporary folder.
        string fileName = Path.GetFileName(options.DefaultPath);
        string fullPath = Path.Combine(_tempFolder, fileName);

        // Create a writable file stream and assign it to the options.
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure the stream is properly closed and disposed.
        options.Stream?.Dispose();
    }

    // Helper to clean up the temporary folder after conversion.
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
                // If deletion fails, ignore – the OS may still be releasing handles.
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Load or create a diagram.
        Diagram diagram = new Diagram();

        // Configure HTML export options.
        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
        {
            // Use the custom stream provider to write resources to a temp folder.
            StreamProvider = new TempFolderStreamProvider(),
            // Export each page as separate files (default behavior).
            SaveAsSingleFile = false
        };

        // Export the diagram to HTML.
        string outputPath = "output.html";
        diagram.Save(outputPath, htmlOptions);

        // Clean up temporary resources.
        if (htmlOptions.StreamProvider is TempFolderStreamProvider provider)
        {
            provider.Cleanup();
        }

        // Optional: inform the user.
        Console.WriteLine($"Diagram exported to {outputPath}. Temporary resources have been removed.");
    }
}