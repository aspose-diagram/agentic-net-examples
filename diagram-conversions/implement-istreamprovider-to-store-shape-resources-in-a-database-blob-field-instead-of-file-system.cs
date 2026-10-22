using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DatabaseStreamProvider : IStreamProvider
{
    // Simulated database table to store resource BLOBs.
    private readonly DataTable _resourceTable;
    // Tracks active streams during the export process.
    private readonly Dictionary<string, MemoryStream> _activeStreams = new Dictionary<string, MemoryStream>();

    public DatabaseStreamProvider(DataTable resourceTable)
    {
        _resourceTable = resourceTable ?? throw new ArgumentNullException(nameof(resourceTable));
    }

    // Called by Aspose when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Create a memory stream that Aspose will write the resource into.
        var memoryStream = new MemoryStream();
        // Assign the stream back to the options so Aspose can use it.
        options.Stream = memoryStream;
        // Store the stream using the default path as the key for later retrieval.
        _activeStreams[options.DefaultPath] = memoryStream;
    }

    // Called by Aspose when the resource stream is closed.
    public void CloseStream(StreamProviderOptions options)
    {
        // Retrieve the memory stream that was previously created.
        if (_activeStreams.TryGetValue(options.DefaultPath, out var memoryStream))
        {
            // Ensure all data is flushed and read from the beginning.
            memoryStream.Position = 0;
            byte[] data = memoryStream.ToArray();

            // Insert the resource into the simulated database table.
            DataRow row = _resourceTable.NewRow();
            row["Path"] = options.DefaultPath; // Resource identifier (e.g., image file name)
            row["Data"] = data;                // BLOB content
            _resourceTable.Rows.Add(row);

            // Clean up.
            memoryStream.Dispose();
            _activeStreams.Remove(options.DefaultPath);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare a DataTable to act as the database for storing resources.
        DataTable resourceTable = new DataTable("DiagramResources");
        resourceTable.Columns.Add("Path", typeof(string));
        resourceTable.Columns.Add("Data", typeof(byte[]));

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create the custom stream provider that writes resources to the DataTable.
            var streamProvider = new DatabaseStreamProvider(resourceTable);

            // Configure HTML export options and assign the stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = streamProvider;
            // Export only the first page as a single HTML file (adjust as needed).
            htmlOptions.PageIndex = 0;
            htmlOptions.PageCount = 1;

            // Output HTML file path.
            string outputHtml = "output.html";

            // Save the diagram to HTML using the custom stream provider.
            diagram.Save(outputHtml, htmlOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
            return;
        }

        // After export, display information about stored resources.
        Console.WriteLine("Resources stored in the simulated database:");
        foreach (DataRow row in resourceTable.Rows)
        {
            string path = row["Path"] as string;
            byte[] data = row["Data"] as byte[];
            Console.WriteLine($"- {path}: {data?.Length ?? 0} bytes");
        }
    }
}