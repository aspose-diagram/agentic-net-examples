using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class HtmlResourceStreamProvider : IStreamProvider
{
    private readonly string _outputFolder;
    public List<string> CreatedResources { get; } = new List<string>();

    public HtmlResourceStreamProvider(string outputFolder)
    {
        _outputFolder = outputFolder;
        Directory.CreateDirectory(_outputFolder);
    }

    public void InitStream(StreamProviderOptions options)
    {
        // options.DefaultPath provides the relative path for the resource
        string resourcePath = Path.Combine(_outputFolder, options.DefaultPath);
        string resourceDir = Path.GetDirectoryName(resourcePath);
        if (!Directory.Exists(resourceDir))
            Directory.CreateDirectory(resourceDir);

        // Assign a file stream to the options so Aspose can write the resource
        options.Stream = new FileStream(resourcePath, FileMode.Create, FileAccess.Write);
        CreatedResources.Add(resourcePath);
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // Close the stream after Aspose finishes writing the resource
        options.Stream?.Close();
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Paths
            string inputDiagramPath = "input.vsdx";
            string outputFolder = "output";
            string htmlFilePath = Path.Combine(outputFolder, "diagram.html");

            // Ensure output folder exists
            Directory.CreateDirectory(outputFolder);

            // Load diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Set up HTML export with custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            HtmlResourceStreamProvider streamProvider = new HtmlResourceStreamProvider(outputFolder);
            htmlOptions.StreamProvider = streamProvider;

            // Export to HTML
            diagram.Save(htmlFilePath, htmlOptions);

            // Validate external resource references in the generated HTML
            ValidateHtmlResources(htmlFilePath, outputFolder);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ValidateHtmlResources(string htmlFilePath, string baseFolder)
    {
        if (!File.Exists(htmlFilePath))
            throw new Exception($"HTML file not found: {htmlFilePath}");

        string htmlContent = File.ReadAllText(htmlFilePath);

        // Regex to find src or href attributes (case-insensitive)
        Regex regex = new Regex(@"(?i)(src|href)\s*=\s*[""']([^""']+)[""']", RegexOptions.Compiled);
        MatchCollection matches = regex.Matches(htmlContent);

        List<string> missingFiles = new List<string>();

        foreach (Match match in matches)
        {
            string url = match.Groups[2].Value.Trim();

            // Skip absolute URLs (http, https, data, etc.)
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                continue;

            // Resolve relative path against the base folder
            string resourcePath = Path.Combine(baseFolder, url.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(resourcePath))
                missingFiles.Add(resourcePath);
        }

        if (missingFiles.Count > 0)
        {
            Console.WriteLine("Missing external resources detected:");
            foreach (string missing in missingFiles)
                Console.WriteLine($" - {missing}");
            throw new Exception("HTML validation failed due to missing resources.");
        }
        else
        {
            Console.WriteLine("All external resource references in the HTML are valid.");
        }
    }
}