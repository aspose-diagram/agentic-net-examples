using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    private readonly string _resourceFolder;

    public CustomStreamProvider(string resourceFolder)
    {
        _resourceFolder = resourceFolder;
        // Ensure the resource folder exists.
        if (!Directory.Exists(_resourceFolder))
        {
            Directory.CreateDirectory(_resourceFolder);
        }
    }

    // Called by Aspose when a resource (e.g., image) needs to be saved.
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // options.DefaultPath contains the file name Aspose wants to use.
            // Combine it with the target folder to create the full path.
            string filePath = Path.Combine(_resourceFolder, options.DefaultPath);
            // Ensure the directory for the file exists.
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            // Provide the stream back to Aspose.
            options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing stream for resource '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }

    // Called by Aspose after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            options.Stream?.Dispose();
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
        // Expect input Visio file path and output directory as arguments.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputDir = args.Length > 1 ? args[1] : "output";

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Folder where HTML resources (images, etc.) will be stored.
            string resourcesFolder = Path.Combine(outputDir, "Resources");
            var streamProvider = new CustomStreamProvider(resourcesFolder);

            // Export each page to a separate HTML file.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                string htmlPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                // Configure HTML export options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,
                    PageCount = 1,
                    StreamProvider = streamProvider
                };

                // Save the page as HTML.
                diagram.Save(htmlPath, htmlOptions);

                // Post‑process the generated HTML to replace absolute paths with relative ones.
                string htmlContent = File.ReadAllText(htmlPath);
                string processedContent = ReplaceAbsolutePaths(htmlContent);
                File.WriteAllText(htmlPath, processedContent);
            }

            Console.WriteLine("HTML export and path conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Replaces Windows absolute file paths (e.g., C:\folder\file.png) with just the file name.
    private static string ReplaceAbsolutePaths(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Regex matches patterns like C:\folder\subfolder\file.ext
        string pattern = @"[A-Za-z]:\\[^\s\""]+";

        return Regex.Replace(input, pattern, match =>
        {
            // Return only the file name part to make the path relative.
            return Path.GetFileName(match.Value);
        });
    }
}