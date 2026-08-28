using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to store shape resources in a database BLOB field.
    public class DbStreamProvider : IStreamProvider
    {
        // Holds temporary streams keyed by the resource path.
        private readonly Dictionary<string, MemoryStream> _streams = new Dictionary<string, MemoryStream>();

        // Called by Aspose.Diagram before a resource is written.
        public void InitStream(StreamProviderOptions options)
        {
            // Create a memory stream to capture the resource data.
            var memStream = new MemoryStream();
            options.Stream = memStream;

            // Store the stream using the default path as the key.
            // DefaultPath identifies the resource (e.g., image file name).
            _streams[options.DefaultPath] = memStream;
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Retrieve the memory stream that was used.
            if (_streams.TryGetValue(options.DefaultPath, out MemoryStream memStream))
            {
                // Ensure all data is flushed.
                memStream.Flush();

                // Get the byte array representing the resource.
                byte[] data = memStream.ToArray();

                // Persist the data to the database.
                SaveResourceToDatabase(options.DefaultPath, data);

                // Clean up.
                memStream.Dispose();
                _streams.Remove(options.DefaultPath);
            }
        }

        // Inserts or updates the resource BLOB in the database.
        private void SaveResourceToDatabase(string resourcePath, byte[] data)
        {
            // Placeholder connection string – replace with actual DB details.
            const string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True";

            // Example table schema:
            // CREATE TABLE ShapeResources (ResourcePath NVARCHAR(260) PRIMARY KEY, Data VARBINARY(MAX));
            const string sql = @"
IF EXISTS (SELECT 1 FROM ShapeResources WHERE ResourcePath = @Path)
    UPDATE ShapeResources SET Data = @Data WHERE ResourcePath = @Path;
ELSE
    INSERT INTO ShapeResources (ResourcePath, Data) VALUES (@Path, @Data);";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Path", SqlDbType.NVarChar, 260).Value = resourcePath;
                command.Parameters.Add("@Data", SqlDbType.VarBinary, -1).Value = data;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                try
                {
                    // Load the Visio diagram from a file.
                    var diagram = new Diagram("input.vsdx");

                    // Configure HTML export options and assign the custom stream provider.
                    var htmlOptions = new HTMLSaveOptions
                    {
                        StreamProvider = new DbStreamProvider()
                    };

                    // Export the diagram to HTML; resources (images, etc.) will be stored in the DB.
                    diagram.Save("output.html", htmlOptions);

                    Console.WriteLine("Diagram exported to HTML successfully. Resources saved to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}