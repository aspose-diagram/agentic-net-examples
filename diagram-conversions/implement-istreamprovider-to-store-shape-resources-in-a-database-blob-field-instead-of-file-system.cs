using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Implements IStreamProvider to store shape resources (e.g., images) in a database BLOB field.
    public class DbStreamProvider : IStreamProvider
    {
        private readonly string _connectionString;
        // Holds temporary streams for each resource identified by its default path.
        private readonly Dictionary<string, MemoryStream> _streams = new Dictionary<string, MemoryStream>();

        public DbStreamProvider(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Called by Aspose.Diagram before writing a resource.
        public void InitStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Create a memory stream that Aspose will write the resource into.
            var ms = new MemoryStream();
            _streams[options.DefaultPath] = ms;

            // Assign the stream back to the options so Aspose can write to it.
            options.Stream = ms;
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (!_streams.TryGetValue(options.DefaultPath, out var ms))
                throw new InvalidOperationException($"Stream for path '{options.DefaultPath}' not found.");

            // Ensure all data is flushed.
            ms.Flush();
            byte[] data = ms.ToArray();

            // Store the resource in the database.
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(
                @"INSERT INTO ShapeResources (ResourcePath, ResourceData) VALUES (@Path, @Data)", connection))
            {
                command.Parameters.Add("@Path", SqlDbType.NVarChar, 260).Value = options.DefaultPath;
                command.Parameters.Add("@Data", SqlDbType.VarBinary, -1).Value = data;

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Clean up.
            ms.Dispose();
            _streams.Remove(options.DefaultPath);
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Example connection string – replace with actual database details.
                string connectionString = "Data Source=.;Initial Catalog=VisioResources;Integrated Security=True";

                // Load a Visio diagram.
                Diagram diagram = new Diagram("sample.vsdx");

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new DbStreamProvider(connectionString);
                htmlOptions.DefaultFont = "Arial";

                // Export the diagram to HTML; shape resources will be saved to the database.
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("Diagram exported and resources stored in the database.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}