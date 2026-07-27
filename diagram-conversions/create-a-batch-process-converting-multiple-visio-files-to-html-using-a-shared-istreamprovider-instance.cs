using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Called before a resource stream is created.
    public void InitStream(StreamProviderOptions options)
    {
        // Provide a temporary memory stream for the resource.
        options.Stream = new MemoryStream();
    }

    // Called after the resource stream is no longer needed.
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing Visio files (default "InputVisio").
        string inputFolder = args.Length > 0 ? args[0] : "InputVisio";

        // Output folder for generated HTML files (default "OutputHtml").
        string outputFolder = args.Length > 1 ? args[1] : "OutputHtml";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Shared IStreamProvider instance for all conversions.
        IStreamProvider streamProvider = new CustomStreamProvider();

        // Process each Visio file in the input folder.
        foreach (string visioPath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(visioPath);

                // Prepare HTML save options and assign the shared stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = streamProvider
                };

                // Determine output HTML file path.
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(visioPath);
                string htmlPath = Path.Combine(outputFolder, fileNameWithoutExt + ".html");

                // Save the diagram as HTML.
                diagram.Save(htmlPath, htmlOptions);

                Console.WriteLine($"Converted: {visioPath} -> {htmlPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{visioPath}': {ex.Message}");
            }
        }
    }
}