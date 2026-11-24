using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Assign a custom stream provider to handle embedded resources.
                // Currently the provider writes the original data without compression.
                // TODO: Implement JPEG compression of image data when an appropriate encoder is available.
                htmlOptions.StreamProvider = new JpegCompressStreamProvider(outputDir);

                // Save the diagram as HTML using the configured options.
                diagram.Save(outputPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

// Custom stream provider that writes embedded resources (e.g., images) to the output directory.
// Image compression using a JPEG encoder is not implemented due to missing API support.
class JpegCompressStreamProvider : IStreamProvider
{
    private readonly string _outputDirectory;

    public JpegCompressStreamProvider(string outputDirectory)
    {
        _outputDirectory = outputDirectory;
    }

    // Called by Aspose.Diagram before writing each resource.
    public void InitStream(StreamProviderOptions options)
    {
        // Use a memory stream to capture the original resource data.
        options.Stream = new MemoryStream();
    }

    // Called after the resource has been written to the stream.
    public void CloseStream(StreamProviderOptions options)
    {
        // Ensure the captured stream is a MemoryStream.
        if (options.Stream is MemoryStream memoryStream)
        {
            memoryStream.Position = 0;

            // Determine the target file path.
            string targetPath = Path.Combine(_outputDirectory, options.DefaultPath);

            // Write the captured data to the target file.
            using (FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(fileStream);
            }

            // Note: JPEG compression is not performed here.
        }
    }
}