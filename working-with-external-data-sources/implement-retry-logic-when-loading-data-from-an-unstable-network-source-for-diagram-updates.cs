using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramUrl> <OutputPath>");
            return;
        }

        string diagramUrl = args[0];
        string outputPath = args[1];

        // Ensure the output directory exists.
        string? outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Download the diagram file with retry logic.
        byte[] diagramData;
        try
        {
            diagramData = DownloadWithRetry(diagramUrl, maxAttempts: 3, initialDelayMs: 1000);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to download diagram: {ex.Message}");
            return;
        }

        // Load the diagram from the downloaded data.
        try
        {
            using (MemoryStream ms = new MemoryStream(diagramData))
            {
                // Load diagram from stream.
                Diagram diagram = new Diagram(ms);

                // Example update: modify the first data connection if it exists.
                if (diagram.DataConnections.Count > 0)
                {
                    // Set a new connection string (replace with real values as needed).
                    diagram.DataConnections[0].ConnectionString = "Data Source=.;Initial Catalog=MyDb;Integrated Security=True";
                    // Optionally update the command/query.
                    diagram.DataConnections[0].Command = "SELECT * FROM SampleTable";
                }

                // Refresh the diagram to apply data connection changes.
                diagram.Refresh();

                // Save the updated diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram updated and saved successfully.");
    }

    /// <summary>
    /// Downloads data from a URL with retry and exponential back‑off.
    /// </summary>
    /// <param name="url">The URL to download.</param>
    /// <param name="maxAttempts">Maximum number of attempts.</param>
    /// <param name="initialDelayMs">Initial delay in milliseconds before retry.</param>
    /// <returns>Byte array containing the downloaded content.</returns>
    private static byte[] DownloadWithRetry(string url, int maxAttempts, int initialDelayMs)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));

        using (HttpClient client = new HttpClient())
        {
            int attempt = 0;
            int delay = initialDelayMs;

            while (true)
            {
                attempt++;
                try
                {
                    // Synchronous call to keep the example simple.
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");

                    // Read content as byte array.
                    return response.Content.ReadAsByteArrayAsync().Result;
                }
                catch (Exception ex) when (attempt < maxAttempts)
                {
                    Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}. Retrying in {delay} ms...");
                    Thread.Sleep(delay);
                    delay *= 2; // Exponential back‑off.
                }
                catch (Exception)
                {
                    // Rethrow after final attempt.
                    throw;
                }
            }
        }
    }
}