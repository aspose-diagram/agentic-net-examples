using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Create a temporary folder for resources generated during HTML export.
        string tempResourceFolder = Path.Combine(Path.GetTempPath(), "AsposeHtmlResources_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempResourceFolder);

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Do not export hidden pages (property type is bool in this version).
                ExportHiddenPage = false,
                // Use a custom stream provider to write resources to the temporary folder.
                StreamProvider = new Base64EmbeddingStreamProvider(tempResourceFolder)
            };

            // Save the diagram to a single HTML file using the configured options.
            diagram.Save(outputHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        try
        {
            // Read the generated HTML content.
            string htmlContent = File.ReadAllText(outputHtmlPath, Encoding.UTF8);

            // Find all src attributes that reference external resource files.
            Regex srcRegex = new Regex(@"src\s*=\s*[""'](?<path>[^""']+)[""']", RegexOptions.IgnoreCase);
            MatchCollection matches = srcRegex.Matches(htmlContent);

            // Replace each external resource reference with a base64‑encoded data URI.
            foreach (Match match in matches)
            {
                string relativePath = match.Groups["path"].Value;
                string resourceFullPath = Path.Combine(tempResourceFolder, relativePath.Replace('/', Path.DirectorySeparatorChar));

                if (!File.Exists(resourceFullPath))
                    continue; // Skip missing resources.

                // Determine MIME type based on file extension.
                string mimeType = GetMimeType(Path.GetExtension(resourceFullPath));

                // Read the resource bytes and convert to Base64.
                byte[] fileBytes = File.ReadAllBytes(resourceFullPath);
                string base64Data = Convert.ToBase64String(fileBytes);

                // Build the data URI.
                string dataUri = $"data:{mimeType};base64,{base64Data}";

                // Replace the original path with the data URI in the HTML content.
                htmlContent = htmlContent.Replace(relativePath, dataUri);
            }

            // Write the updated HTML back to the output file.
            File.WriteAllText(outputHtmlPath, htmlContent, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML post‑processing: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary resource folder.
            try
            {
                if (Directory.Exists(tempResourceFolder))
                    Directory.Delete(tempResourceFolder, true);
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }

        Console.WriteLine($"HTML export completed successfully: {outputHtmlPath}");
    }

    // Helper method to map file extensions to MIME types.
    private static string GetMimeType(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".png":
                return "image/png";
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".gif":
                return "image/gif";
            case ".svg":
                return "image/svg+xml";
            case ".bmp":
                return "image/bmp";
            default:
                return "application/octet-stream";
        }
    }
}

// Custom stream provider that writes HTML resources to a specified folder.
class Base64EmbeddingStreamProvider : IStreamProvider
{
    private readonly string _basePath;

    public Base64EmbeddingStreamProvider(string basePath)
    {
        _basePath = basePath;
    }

    // Called by Aspose.Diagram when a resource stream is needed.
    public void InitStream(StreamProviderOptions options)
    {
        // Combine the base folder with the default path supplied by Aspose.
        string filePath = Path.Combine(_basePath, options.DefaultPath);

        // Ensure the target directory exists.
        string directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Assign a writable FileStream to the options.
        options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Close and dispose the stream if it was created.
        options.Stream?.Close();
        options.Stream?.Dispose();
    }
}