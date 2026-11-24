using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Configuration for the network source and retry logic
        const string diagramUrl = "https://example.com/unstable/diagram.vsdx";
        const string tempFilePath = "downloaded_diagram.vsdx";
        const int maxRetryAttempts = 5;
        const int delayBetweenRetriesMs = 2000; // 2 seconds

        // Attempt to download the diagram file with retry logic
        bool downloadSucceeded = false;
        using (HttpClient httpClient = new HttpClient())
        {
            for (int attempt = 1; attempt <= maxRetryAttempts; attempt++)
            {
                try
                {
                    Console.WriteLine($"Attempt {attempt} to download diagram from network source...");
                    using (HttpResponseMessage response = httpClient.GetAsync(diagramUrl).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        using (Stream networkStream = response.Content.ReadAsStreamAsync().Result)
                        using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            networkStream.CopyTo(fileStream);
                        }
                    }

                    // Verify that the file was written
                    if (File.Exists(tempFilePath))
                    {
                        downloadSucceeded = true;
                        Console.WriteLine("Download succeeded.");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Download attempt {attempt} failed: {ex.Message}");
                    if (attempt < maxRetryAttempts)
                    {
                        Console.WriteLine($"Waiting {delayBetweenRetriesMs} ms before next attempt...");
                        Thread.Sleep(delayBetweenRetriesMs);
                    }
                }
            }
        }

        // Guard: ensure the file was downloaded before proceeding
        if (!downloadSucceeded || !File.Exists(tempFilePath))
        {
            Console.Error.WriteLine("Failed to download the diagram after multiple attempts.");
            return;
        }

        // Load the diagram from the downloaded file and perform an update
        try
        {
            using (FileStream diagramStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read))
            using (Diagram diagram = new Diagram(diagramStream))
            {
                // Example update: modify the first data connection if it exists
                if (diagram.DataConnections.Count > 0)
                {
                    // Update connection string and command (replace with real values as needed)
                    diagram.DataConnections[0].ConnectionString = "Data Source=Server;Initial Catalog=Database;Integrated Security=True";
                    diagram.DataConnections[0].Command = "SELECT * FROM SampleTable";
                    // Refresh the diagram to apply the data changes
                    diagram.Refresh();
                }

                // Save the updated diagram to a new file
                const string outputPath = "updated_diagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary downloaded file
            try
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
            catch (Exception cleanupEx)
            {
                Console.Error.WriteLine($"Failed to delete temporary file: {cleanupEx.Message}");
            }
        }
    }
}