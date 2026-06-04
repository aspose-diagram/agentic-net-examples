using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to store HTML export resources (e.g., images) in a database BLOB field.
    public class DbStreamProvider : IStreamProvider
    {
        private readonly string _connectionString;
        // Keeps track of the memory streams created for each resource path.
        private readonly Dictionary<string, MemoryStream> _streamMap = new();

        public DbStreamProvider(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Called by Aspose.Diagram before writing a resource.
        public void InitStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Create a memory stream that Aspose will write the resource into.
            var memoryStream = new MemoryStream();
            options.Stream = memoryStream;

            // Store the stream so we can retrieve it later in CloseStream.
            _streamMap[options.DefaultPath] = memoryStream;
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (!_streamMap.TryGetValue(options.DefaultPath, out var memoryStream))
                throw new InvalidOperationException($"Stream for path '{options.DefaultPath}' was not initialized.");

            // Ensure the stream position is at the beginning before reading.
            memoryStream.Position = 0;
            byte[] data = memoryStream.ToArray();

            // Insert the resource into the database.
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(
                @"INSERT INTO DiagramResources (ResourcePath, ResourceData) VALUES (@Path, @Data)", connection))
            {
                command.Parameters.Add("@Path", SqlDbType.NVarChar, 260).Value = options.DefaultPath;
                command.Parameters.Add("@Data", SqlDbType.VarBinary, -1).Value = data;

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Clean up.
            memoryStream.Dispose();
            _streamMap.Remove(options.DefaultPath);
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file.
                string sourcePath = "input.vsdx";

                // Path where the HTML files will be generated.
                string outputPath = "output.html";

                // Connection string to the database where resources will be stored.
                string dbConnectionString = "Data Source=SERVER;Initial Catalog=DiagramDb;Integrated Security=True";

                // Load the diagram.
                var diagram = new Diagram(sourcePath);

                // Configure HTML export options and assign the custom stream provider.
                var htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new DbStreamProvider(dbConnectionString)
                };

                // Export the diagram to HTML. Resources (images, CSS, etc.) will be captured by DbStreamProvider.
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine("HTML export completed. Resources stored in the database.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}