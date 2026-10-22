using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Run the auto‑spacing operation asynchronously and wait for completion
        try
        {
            AutoSpaceAsync(inputPath, outputPath).GetAwaiter().GetResult();
            Console.WriteLine("Auto‑spacing completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Performs auto‑spacing on all pages of the diagram without blocking the calling thread.
    /// </summary>
    /// <param name="inputPath">Path to the source Visio file.</param>
    /// <param name="outputPath">Path where the modified Visio file will be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task AutoSpaceAsync(string inputPath, string outputPath)
    {
        await Task.Run(() =>
        {
            try
            {
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and apply auto‑spacing
                foreach (Page page in diagram.Pages)
                {
                    // Configure auto‑spacing distances (example values)
                    AutoSpaceOptions options = new AutoSpaceOptions
                    {
                        DistanceInHorizontal = 2,
                        DistanceInVertical = 2
                    };

                    // Apply auto‑spacing to the entire shape collection of the page
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Propagate any Aspose‑Diagram exceptions to the caller
                throw new Exception($"Auto‑spacing failed: {ex.Message}", ex);
            }
        });
    }
}