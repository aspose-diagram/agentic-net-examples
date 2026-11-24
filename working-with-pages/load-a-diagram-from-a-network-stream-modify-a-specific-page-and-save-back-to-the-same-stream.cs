using System;
using System.IO;
using System.Net.Http;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <diagramUrl> <pageIndex>");
            return;
        }

        string diagramUrl = args[0];
        if (!int.TryParse(args[1], out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index.");
            return;
        }

        // Download the diagram into a writable MemoryStream.
        MemoryStream diagramStream = new MemoryStream();
        try
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = client.GetAsync(diagramUrl).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Console.Error.WriteLine($"Failed to download diagram. HTTP {(int)response.StatusCode}");
                    return;
                }

                using (Stream responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    responseStream.CopyTo(diagramStream);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error downloading diagram: {ex.Message}");
            return;
        }

        // Ensure the stream position is at the beginning before loading.
        diagramStream.Position = 0;

        // Load the diagram from the stream, modify the specified page, and save back to the same stream.
        try
        {
            using (Diagram diagram = new Diagram(diagramStream))
            {
                // Guard against out‑of‑range page index.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target page.
                Page targetPage = diagram.Pages[pageIndex];

                // Example modification: rename the page.
                targetPage.NameU = "ModifiedPage";

                // Example modification: ensure the page is not marked as background.
                targetPage.Background = Aspose.Diagram.BOOL.False;

                // Reset the stream to overwrite the original content.
                diagramStream.Position = 0;
                diagramStream.SetLength(0);

                // Save the modified diagram back into the same stream.
                diagram.Save(diagramStream, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // At this point, diagramStream contains the updated diagram.
        // For demonstration purposes, you could write it to a local file.
        // string outputPath = "modified_diagram.vsdx";
        // File.WriteAllBytes(outputPath, diagramStream.ToArray());
    }
}