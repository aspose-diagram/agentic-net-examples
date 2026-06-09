using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram asynchronously
            Diagram diagram = await LoadDiagramAsync(inputPath);

            // Modify all hyperlinks in the diagram
            UpdateHyperlinks(diagram);

            // Save the modified diagram asynchronously
            await SaveDiagramAsync(diagram, outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Asynchronously loads a diagram from a file using an async FileStream
    static async Task<Diagram> LoadDiagramAsync(string path)
    {
        // Open the file with asynchronous I/O enabled
        using var stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        // Aspose.Diagram reads synchronously from the stream,
        // so wrap the constructor in Task.Run to avoid blocking the UI thread
        return await Task.Run(() => new Diagram(stream));
    }

    // Updates hyperlink addresses throughout the diagram
    static void UpdateHyperlinks(Diagram diagram)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Hyperlinks != null)
                {
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Example modification: ensure all addresses are absolute URLs
                        if (!string.IsNullOrWhiteSpace(link.Address?.Value) &&
                            !link.Address.Value.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        {
                            // Prepend a base URL to relative addresses
                            link.Address.Value = "https://example.com/" + link.Address.Value.TrimStart('/');
                        }
                    }
                }
            }
        }
    }

    // Asynchronously saves the diagram to a file using the VSDX format
    static async Task SaveDiagramAsync(Diagram diagram, string path)
    {
        // Wrap the synchronous Save call in Task.Run to keep the UI responsive
        await Task.Run(() => diagram.Save(path, SaveFileFormat.Vsdx));
    }
}
