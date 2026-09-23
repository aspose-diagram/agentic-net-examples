using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Aspose.Diagram;

class Program
{
    // Attempts to download a Visio file from the given URL with retry logic.
    // Returns a Diagram instance if successful, otherwise throws the last exception.
    static Diagram LoadDiagramWithRetry(string url, int maxRetries, int delayMilliseconds)
    {
        using (HttpClient client = new HttpClient())
        {
            int attempt = 0;
            while (true)
            {
                try
                {
                    // Download the file content.
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    // Load the content into a MemoryStream and create the Diagram.
                    using (MemoryStream ms = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result))
                    {
                        // Diagram constructor loads the Visio file from the stream.
                        return new Diagram(ms);
                    }
                }
                catch (Exception ex) when (ex is HttpRequestException || ex is IOException)
                {
                    attempt++;
                    if (attempt > maxRetries)
                    {
                        // All retries exhausted – rethrow the exception.
                        throw new Exception($"Failed to load diagram after {maxRetries} attempts.", ex);
                    }

                    // Wait before the next retry.
                    Thread.Sleep(delayMilliseconds);
                }
            }
        }
    }

    static void Main(string[] args)
    {
        // Example parameters – adjust as needed.
        string diagramUrl = "https://example.com/sample.vsdx";
        int maxRetries = 3;
        int retryDelayMs = 2000; // 2 seconds between attempts.

        try
        {
            // Load the diagram with retry logic.
            Diagram diagram = LoadDiagramWithRetry(diagramUrl, maxRetries, retryDelayMs);

            // Perform a simple update: add a rectangle shape to the first page.
            Page firstPage = diagram.Pages[0];
            // Draw a rectangle at (2,2) with width 4 and height 2 inches.
            long shapeId = firstPage.DrawRectangle(2.0, 2.0, 4.0, 2.0);
            // Retrieve the shape to add text.
            var shape = firstPage.Shapes.GetShape(shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Updated via retry logic"));

            // Save the updated diagram to a local file.
            string outputPath = "updated_output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram successfully loaded, updated, and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
