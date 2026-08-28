using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeResourceStreamProvider : IStreamProvider
{
    // Called before a resource stream is needed.
    public void InitStream(StreamProviderOptions options)
    {
        // The default path contains the original resource name (e.g., "image1.png").
        string defaultPath = options.DefaultPath;
        string fileName = Path.GetFileName(defaultPath);

        // Determine subfolder based on simple heuristics.
        string subFolder = "Resources";
        if (defaultPath.IndexOf("image", StringComparison.OrdinalIgnoreCase) >= 0)
            subFolder = "Images";
        else if (defaultPath.IndexOf("font", StringComparison.OrdinalIgnoreCase) >= 0)
            subFolder = "Fonts";

        // Ensure the folder exists and create the file stream.
        string outputDir = Path.Combine("output", subFolder);
        Directory.CreateDirectory(outputDir);
        string fullPath = Path.Combine(outputDir, fileName);
        options.Stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
    }

    // Called after the resource has been written.
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

            // Load the source Visio diagram.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Set up HTML export with the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new ShapeResourceStreamProvider();

            // Export to HTML; resources will be placed in the appropriate subfolders.
            string outputPath = Path.Combine("output", "diagram.html");
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("HTML export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}