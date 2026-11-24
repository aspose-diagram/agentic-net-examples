using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure correct number of arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Run the asynchronous processing synchronously.
        ProcessDiagramAsync(inputPath, outputPath).GetAwaiter().GetResult();
    }

    private static async Task ProcessDiagramAsync(string inputPath, string outputPath)
    {
        try
        {
            // Asynchronously read the diagram file into a byte array.
            byte[] diagramBytes = await File.ReadAllBytesAsync(inputPath);

            // Load the diagram from a memory stream.
            using (var inputStream = new MemoryStream(diagramBytes))
            using (var diagram = new Diagram(inputStream))
            {
                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    var page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        var shape = page.Shapes[shapeIndex];

                        // Iterate through all hyperlinks of the shape.
                        for (int linkIndex = 0; linkIndex < shape.Hyperlinks.Count; linkIndex++)
                        {
                            var link = shape.Hyperlinks[linkIndex];

                            // Modify the hyperlink address (example: prepend a query string).
                            string originalAddress = link.Address?.Value ?? string.Empty;
                            string modifiedAddress = string.IsNullOrEmpty(originalAddress)
                                ? "https://example.com/modified"
                                : originalAddress + "?modified";

                            link.Address.Value = modifiedAddress;

                            // Optionally, update the description.
                            link.Description.Value = "Modified hyperlink";
                        }
                    }
                }

                // Save the modified diagram to a memory stream.
                using (var outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    byte[] outputBytes = outputStream.ToArray();

                    // Asynchronously write the modified diagram to the output file.
                    await File.WriteAllBytesAsync(outputPath, outputBytes);
                }
            }

            Console.WriteLine("Diagram processing completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}