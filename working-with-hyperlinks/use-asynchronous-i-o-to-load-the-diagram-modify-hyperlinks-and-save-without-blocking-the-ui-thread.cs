using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram on a background thread to avoid blocking the UI.
            Diagram diagram = LoadDiagramAsync(inputPath).GetAwaiter().GetResult();

            // Update all hyperlinks in the diagram.
            UpdateHyperlinks(diagram);

            // Save the modified diagram on a background thread.
            SaveDiagramAsync(diagram, outputPath).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Loads a diagram asynchronously by delegating the constructor to a background task.
    private static async Task<Diagram> LoadDiagramAsync(string path)
    {
        return await Task.Run(() => new Diagram(path));
    }

    // Iterates through all pages and shapes, updating each hyperlink's address and description.
    private static void UpdateHyperlinks(Diagram diagram)
    {
        // Iterate over each page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate over each shape on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a hyperlink collection.
                if (shape.Hyperlinks == null) continue;

                // Update each hyperlink attached to the shape.
                for (int i = 0; i < shape.Hyperlinks.Count; i++)
                {
                    Hyperlink link = shape.Hyperlinks[i];
                    // Set a new address.
                    link.Address.Value = "https://example.com/updated";
                    // Update the description (tooltip).
                    link.Description.Value = "Updated hyperlink";
                }
            }
        }
    }

    // Saves the diagram asynchronously by performing the save operation on a background thread.
    private static async Task SaveDiagramAsync(Diagram diagram, string path)
    {
        // Ensure the output directory exists.
        string directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await Task.Run(() => diagram.Save(path, SaveFileFormat.Vsdx));
    }
}