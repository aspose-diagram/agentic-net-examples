using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Custom stream provider to write external resources (images, CSS, etc.) to a specified folder
    class CustomStreamProvider : IStreamProvider
    {
        private readonly string _baseFolder;
        private readonly HashSet<string> _writtenFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public CustomStreamProvider(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        // Returns the set of files that were written by the provider
        public IReadOnlyCollection<string> WrittenFiles => _writtenFiles;

        // Called by Aspose.Diagram when a resource stream needs to be created
        public void InitStream(StreamProviderOptions options)
        {
            // Ensure the base folder exists
            Directory.CreateDirectory(_baseFolder);

            // options.DefaultPath contains the relative file name for the resource
            string fileName = options.DefaultPath ?? "resource.bin";
            string fullPath = Path.Combine(_baseFolder, fileName);

            // Create the file stream and assign it back to the options
            FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            options.Stream = fs;

            // Track the written file path for later validation
            _writtenFiles.Add(fullPath);
        }

        // Called by Aspose.Diagram after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            if (options.Stream != null)
            {
                options.Stream.Close();
                options.Stream = null;
            }
        }
    }

    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists (will be created if missing)
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the custom stream provider for HTML export
                var streamProvider = new CustomStreamProvider(outputFolder);
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Assign the stream provider to handle external resources
                    StreamProvider = streamProvider
                };

                // Export each page as a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    htmlOptions.PageIndex = i;
                    htmlOptions.PageCount = 1; // Export only the current page

                    string htmlFilePath = Path.Combine(outputFolder, $"page_{i + 1}.html");
                    diagram.Save(htmlFilePath, htmlOptions);
                }

                // After export, validate that all external resources referenced in HTML exist
                ValidateHtmlResources(outputFolder);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }

    // Scans all HTML files in the folder and checks that referenced resources exist on disk
    private static void ValidateHtmlResources(string folderPath)
    {
        // Regex to capture src or href attribute values
        Regex regex = new Regex(@"(?:src|href)\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);

        // Get all HTML files in the folder
        string[] htmlFiles = Directory.GetFiles(folderPath, "*.html", SearchOption.TopDirectoryOnly);
        foreach (string htmlFile in htmlFiles)
        {
            string htmlContent = File.ReadAllText(htmlFile);
            string htmlDir = Path.GetDirectoryName(htmlFile) ?? folderPath;

            foreach (Match match in regex.Matches(htmlContent))
            {
                string resourcePath = match.Groups[1].Value;

                // Skip absolute URLs (http, https, data)
                if (resourcePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                    resourcePath.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Resolve relative paths against the HTML file location
                string resolvedPath = Path.GetFullPath(Path.Combine(htmlDir, resourcePath));

                if (!File.Exists(resolvedPath))
                {
                    Console.Error.WriteLine($"Missing resource referenced in '{Path.GetFileName(htmlFile)}': {resourcePath}");
                }
            }
        }
    }
}