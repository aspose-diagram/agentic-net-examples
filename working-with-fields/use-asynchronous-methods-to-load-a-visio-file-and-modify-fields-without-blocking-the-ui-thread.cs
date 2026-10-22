using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Asynchronously load the Visio diagram
            Diagram diagram = LoadDiagramAsync(inputPath).GetAwaiter().GetResult();

            // Iterate through all pages and shapes to modify custom properties
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Props collection
                    if (shape.Props != null)
                    {
                        // Iterate over each custom property
                        foreach (Prop prop in shape.Props)
                        {
                            // Example: update a property named "Status"
                            if (prop.Name == "Status")
                            {
                                prop.Value.Val = "Updated";
                            }
                        }
                    }
                }
            }

            // Asynchronously save the modified diagram
            SaveDiagramAsync(diagram, outputPath).GetAwaiter().GetResult();

            Console.WriteLine("Diagram processing completed successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads a Visio diagram on a background thread to avoid blocking the UI thread.
    /// </summary>
    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() => new Diagram(path));
    }

    /// <summary>
    /// Saves a Visio diagram on a background thread to avoid blocking the UI thread.
    /// </summary>
    private static Task SaveDiagramAsync(Diagram diagram, string path)
    {
        return Task.Run(() => diagram.Save(path, SaveFileFormat.Vsdx));
    }
}