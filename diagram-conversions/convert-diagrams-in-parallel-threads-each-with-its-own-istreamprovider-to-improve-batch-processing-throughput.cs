using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    private readonly string _outputDirectory;

    public MyStreamProvider(string outputDirectory)
    {
        _outputDirectory = outputDirectory;
    }

    // Initialize a stream for each resource (image, shape, etc.) during HTML export
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base output directory with the default relative path provided by Aspose
        string filePath = Path.Combine(_outputDirectory, options.DefaultPath);

        // Ensure the target directory exists
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Assign a writable FileStream to the options
        options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Close and dispose the stream after the resource has been written
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
    static void Main(string[] args)
    {
        // Expect two arguments: input folder containing diagrams and output folder for HTML files
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Gather supported diagram files from the input folder
        string[] allFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".mmd"
        };

        var diagramFiles = new List<string>();
        foreach (string filePath in allFiles)
        {
            if (supportedExtensions.Contains(Path.GetExtension(filePath)))
            {
                diagramFiles.Add(filePath);
            }
        }

        // Process each diagram file in parallel to improve throughput
        Parallel.ForEach(diagramFiles, inputPath =>
        {
            // Guard against missing files (should not happen, but safe)
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            try
            {
                // Determine output HTML file path
                string baseName = Path.GetFileNameWithoutExtension(inputPath);
                string htmlOutputPath = Path.Combine(outputFolder, baseName + ".html");

                // Create a subdirectory for HTML resources (images, CSS, etc.)
                string resourceDirectory = Path.Combine(outputFolder, baseName + "_files");
                if (!Directory.Exists(resourceDirectory))
                {
                    Directory.CreateDirectory(resourceDirectory);
                }

                // Configure HTML save options with a custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new MyStreamProvider(resourceDirectory);

                // Load the diagram inside a using block as required
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Save the diagram to HTML using the configured options
                    diagram.Save(htmlOutputPath, htmlOptions);
                }

                Console.WriteLine($"Converted: {inputPath} -> {htmlOutputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {inputPath}: {ex.Message}");
            }
        });
    }
}