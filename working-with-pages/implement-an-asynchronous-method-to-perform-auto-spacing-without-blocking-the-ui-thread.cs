using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static async Task Main(string[] args)
    {
        // Prompt user for input diagram path
        Console.Write("Enter the path to the input Visio file: ");
        string inputPath = Console.ReadLine()?.Trim() ?? string.Empty;

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Prompt user for output diagram path
        Console.Write("Enter the desired output path (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine()?.Trim() ?? string.Empty;

        // Prompt user for horizontal spacing distance
        Console.Write("Enter horizontal spacing distance (in inches, e.g., 0.5): ");
        if (!double.TryParse(Console.ReadLine(), out double horizDist))
        {
            Console.Error.WriteLine("Error: Invalid horizontal distance value.");
            return;
        }

        // Prompt user for vertical spacing distance
        Console.Write("Enter vertical spacing distance (in inches, e.g., 0.5): ");
        if (!double.TryParse(Console.ReadLine(), out double vertDist))
        {
            Console.Error.WriteLine("Error: Invalid vertical distance value.");
            return;
        }

        // Perform auto-spacing asynchronously
        await AutoSpaceAsync(inputPath, outputPath, horizDist, vertDist);
    }

    /// <summary>
    /// Asynchronously loads a Visio diagram, applies auto-spacing to all shapes on the first page,
    /// and saves the modified diagram to the specified output path.
    /// </summary>
    /// <param name="inputPath">Path to the source Visio file.</param>
    /// <param name="outputPath">Path where the modified diagram will be saved.</param>
    /// <param name="horizontalDistance">Horizontal spacing distance (in inches).</param>
    /// <param name="verticalDistance">Vertical spacing distance (in inches).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task AutoSpaceAsync(string inputPath, string outputPath, double horizontalDistance, double verticalDistance)
    {
        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Use the first page for auto-spacing
            Page page = diagram.Pages[0];

            // Configure auto-spacing options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = horizontalDistance,
                DistanceInVertical = verticalDistance
            };

            // Run the potentially CPU‑intensive auto‑spacing on a background thread
            await Task.Run(() =>
            {
                // Auto-space all shapes on the page using the configured options
                page.AutoSpaceShapes(page.Shapes, options);
            });

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Auto-spacing completed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any exceptions to the error stream
            Console.Error.WriteLine($"An error occurred during auto-spacing: {ex.Message}");
        }
    }
}