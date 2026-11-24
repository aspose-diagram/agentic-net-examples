using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DatabaseStreamProvider : IStreamProvider
{
    // Simulated database table: each row stores a resource path and its binary content.
    private static readonly DataTable _resourceTable = CreateResourceTable();

    // In-memory map to keep the active stream for each resource during saving.
    private readonly Dictionary<string, MemoryStream> _activeStreams = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

    private static DataTable CreateResourceTable()
    {
        var table = new DataTable("DiagramResources");
        table.Columns.Add("ResourcePath", typeof(string));
        table.Columns.Add("Content", typeof(byte[]));
        table.PrimaryKey = new[] { table.Columns["ResourcePath"] };
        return table;
    }

    // Called by Aspose.Diagram before writing a resource (image, shape, etc.).
    public void InitStream(StreamProviderOptions options)
    {
        // Create a fresh memory stream for the resource.
        var ms = new MemoryStream();
        // Store the stream in the dictionary using the default path as the key.
        _activeStreams[options.DefaultPath] = ms;
        // Assign the stream back to the options so Aspose can write into it.
        options.Stream = ms;
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Retrieve the memory stream that was used.
        if (_activeStreams.TryGetValue(options.DefaultPath, out MemoryStream ms))
        {
            // Ensure all data is flushed.
            ms.Flush();
            // Capture the byte array.
            byte[] data = ms.ToArray();

            // Insert or update the simulated database table.
            DataRow row = _resourceTable.Rows.Find(options.DefaultPath);
            if (row == null)
            {
                row = _resourceTable.NewRow();
                row["ResourcePath"] = options.DefaultPath;
                row["Content"] = data;
                _resourceTable.Rows.Add(row);
            }
            else
            {
                row["Content"] = data;
            }

            // Clean up.
            ms.Dispose();
            _activeStreams.Remove(options.DefaultPath);
        }
    }

    // Helper method to display stored resources (for demonstration purposes).
    public static void PrintStoredResources()
    {
        Console.WriteLine("=== Resources stored in simulated database ===");
        foreach (DataRow row in _resourceTable.Rows)
        {
            string path = row["ResourcePath"] as string;
            byte[] content = row["Content"] as byte[];
            Console.WriteLine($"Path: {path}, Size: {content?.Length ?? 0} bytes");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Export only the first page for simplicity; adjust as needed.
                PageIndex = 0,
                PageCount = 1,
                // Assign our custom stream provider to capture resources in the simulated DB.
                StreamProvider = new DatabaseStreamProvider()
            };

            // Save the diagram as HTML. Resources (images, etc.) will be routed through the stream provider.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");

            // Demonstrate that resources have been stored in the simulated database.
            DatabaseStreamProvider.PrintStoredResources();
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}