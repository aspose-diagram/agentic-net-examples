using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Implements IStreamProvider to upload HTML resources (images, CSS, etc.) to Azure Blob Storage.
    public class AzureBlobStreamProvider : IStreamProvider
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public AzureBlobStreamProvider(string connectionString, string containerName)
        {
            // Validate constructor arguments.
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
        }

        // Called by Aspose.Diagram before writing a resource.
        public void InitStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // The DefaultPath contains the relative path/filename for the resource (e.g., "images/img1.png").
            string blobName = options.DefaultPath.Replace('\\', '/');

            // Use reflection to avoid compile‑time dependency on Azure.Storage.Blobs (prevents version conflicts).
            // Load the BlobContainerClient type.
            Type containerClientType = Type.GetType("Azure.Storage.Blobs.BlobContainerClient, Azure.Storage.Blobs");
            if (containerClientType == null) throw new InvalidOperationException("Azure.Storage.Blobs assembly not found.");

            // Create an instance: new BlobContainerClient(connectionString, containerName)
            object containerClient = Activator.CreateInstance(containerClientType, _connectionString, _containerName);
            if (containerClient == null) throw new InvalidOperationException("Failed to create BlobContainerClient.");

            // Call CreateIfNotExists() to ensure the container exists.
            var createIfNotExistsMethod = containerClientType.GetMethod("CreateIfNotExists", Type.EmptyTypes);
            createIfNotExistsMethod?.Invoke(containerClient, null);

            // Get the BlobClient for the specific blob name.
            var getBlobClientMethod = containerClientType.GetMethod("GetBlobClient", new[] { typeof(string) });
            object blobClient = getBlobClientMethod?.Invoke(containerClient, new object[] { blobName });
            if (blobClient == null) throw new InvalidOperationException("Failed to get BlobClient.");

            // Open a writable stream to the blob (overwrite = true).
            Type blobClientType = blobClient.GetType();
            var openWriteMethod = blobClientType.GetMethod("OpenWrite", new[] { typeof(bool) });
            Stream blobStream = (Stream)openWriteMethod?.Invoke(blobClient, new object[] { true });

            // Assign the stream to the options so Aspose.Diagram can write the resource.
            options.Stream = blobStream;
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created.
            if (options?.Stream != null)
            {
                options.Stream.Dispose();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Path to the source Visio file.
            string visioPath = "input.vsdx";

            // Guard: ensure the Visio file exists before proceeding.
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                return;
            }

            try
            {
                // Load the diagram from the Visio file.
                Diagram diagram = new Diagram(visioPath);

                // Azure Blob Storage connection details.
                string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
                string containerName = "visio-resources";

                // Set up HTML export options with the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new AzureBlobStreamProvider(azureConnectionString, containerName);

                // Export the diagram to HTML. Resources will be uploaded to Azure Blob Storage.
                string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML. Resources stored in Azure Blob Storage.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error console.
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}