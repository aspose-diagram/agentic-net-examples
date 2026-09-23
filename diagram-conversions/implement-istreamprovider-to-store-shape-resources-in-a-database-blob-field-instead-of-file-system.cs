using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportWithDbStreamProvider
{
    // Custom IStreamProvider that stores streams (e.g., images) into a database BLOB field.
    public class DatabaseStreamProvider : IStreamProvider
    {
        // In-memory storage to keep streams between InitStream and CloseStream calls.
        private readonly Dictionary<string, MemoryStream> _streamCache = new();

        // Called by Aspose.Diagram when a new resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Create a memory stream to capture the resource data.
            var memStream = new MemoryStream();

            // Assign the stream to the options so Aspose.Diagram writes into it.
            options.Stream = memStream;

            // Use DefaultPath (the resource name) as the key for later retrieval.
            string key = options.DefaultPath ?? Guid.NewGuid().ToString();
            _streamCache[key] = memStream;
        }

        // Called by Aspose.Diagram after writing to the stream is finished.
        public void CloseStream(StreamProviderOptions options)
        {
            // Retrieve the memory stream that was previously stored.
            string key = options.DefaultPath ?? string.Empty;
            if (!_streamCache.TryGetValue(key, out var memStream))
            {
                // No stream found; nothing to store.
                return;
            }

            // Ensure the stream position is at the beginning before reading.
            memStream.Position = 0;
            byte[] data = memStream.ToArray();

            // -----------------------------------------------------------------
            // Insert the byte[] into a database BLOB field.
            // The following code is a placeholder illustrating typical ADO.NET usage.
            // Replace the connection string and command text with your actual schema.
            // -----------------------------------------------------------------
            /*
            using (DbConnection conn = new SqlConnection("your-connection-string"))
            {
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO DiagramResources (ResourceName, Data) VALUES (@name, @data)";
                    var paramName = cmd.CreateParameter();
                    paramName.ParameterName = "@name";
                    paramName.Value = key;
                    cmd.Parameters.Add(paramName);

                    var paramData = cmd.CreateParameter();
                    paramData.ParameterName = "@data";
                    paramData.Value = data;
                    cmd.Parameters.Add(paramData);

                    cmd.ExecuteNonQuery();
                }
            }
            */
            // Since external DB drivers are not available, we simulate the operation with a DataTable.
            DataTable simulatedTable = new DataTable("DiagramResources");
            simulatedTable.Columns.Add("ResourceName", typeof(string));
            simulatedTable.Columns.Add("Data", typeof(byte[]));
            DataRow row = simulatedTable.NewRow();
            row["ResourceName"] = key;
            row["Data"] = data;
            simulatedTable.Rows.Add(row);
            Console.WriteLine($"[Info] Resource '{key}' stored in simulated DB table (Rows: {simulatedTable.Rows.Count}).");

            // Clean up the cached stream.
            _streamCache.Remove(key);
            memStream.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                string diagramPath = "sample.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // The StreamProvider will intercept resource streams (e.g., images) during export.
                    StreamProvider = new DatabaseStreamProvider()
                };

                // Export the diagram to HTML. Resources will be captured by DatabaseStreamProvider.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine($"Diagram exported to '{outputHtml}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}