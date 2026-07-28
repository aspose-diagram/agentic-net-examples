using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Asynchronously loads a Visio diagram, updates its hyperlinks, and saves it.
    static async Task UpdateHyperlinksAsync(string inputPath, string outputPath)
    {
        // Open the source file with asynchronous I/O.
        await using var inputStream = new FileStream(
            inputPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        // Load the diagram from the stream.
        var diagram = new Diagram(inputStream);

        // Iterate through all pages and shapes to modify hyperlinks.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Hyperlinks != null)
                {
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Example modification: replace an old domain with a new one.
                        if (link.Address != null && link.Address.Value != null)
                        {
                            link.Address.Value = link.Address.Value.Replace("http://oldsite.com", "https://newsite.com");
                        }

                        // Optionally update the description.
                        if (link.Description != null && link.Description.Value != null)
                        {
                            link.Description.Value = "Updated hyperlink";
                        }
                    }
                }
            }
        }

        // Save the modified diagram asynchronously.
        await using var outputStream = new FileStream(
            outputPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true);

        // Diagram.Save is synchronous; wrap it in Task.Run to avoid blocking the UI thread.
        await Task.Run(() => diagram.Save(outputStream, SaveFileFormat.Vsdx));
    }

    // Entry point of the console application.
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            await UpdateHyperlinksAsync(inputPath, outputPath);
            Console.WriteLine("Diagram processed and saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
