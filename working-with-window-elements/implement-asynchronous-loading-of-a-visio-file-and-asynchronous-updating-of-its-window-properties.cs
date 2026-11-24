using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    // Asynchronously loads a Visio diagram from the specified file path.
    private static Task<Diagram> LoadDiagramAsync(string filePath)
    {
        return Task.Run(() =>
        {
            // Load the diagram using Aspose.Diagram constructor.
            return new Diagram(filePath);
        });
    }

    // Asynchronously updates window properties of the diagram.
    private static Task UpdateWindowPropertiesAsync(Diagram diagram)
    {
        return Task.Run(() =>
        {
            // Ensure there is at least one window; otherwise add a default drawing window.
            if (diagram.Windows.Count == 0)
            {
                var newWindow = new Window();
                newWindow.WindowType = WindowTypeValue.Drawing;
                newWindow.WindowState = WindowStateValue.Maximized;
                newWindow.WindowWidth = 800;
                newWindow.WindowHeight = 600;
                diagram.Windows.Add(newWindow);
            }

            // Retrieve the first window for property updates.
            Window window = diagram.Windows[0];

            // Update BOOL properties using BOOL.True / BOOL.False (no .Value accessor).
            window.ShowGrid = BOOL.True;
            window.ShowGuides = BOOL.True;
            window.ShowRulers = BOOL.True;
            window.ShowPageBreaks = BOOL.True;
            window.ShowConnectionPoints = BOOL.True;
            window.DynamicGridEnabled = BOOL.True;
        });
    }

    // Asynchronously saves a Visio diagram to the specified file path in VSDX format.
    private static Task SaveDiagramAsync(Diagram diagram, string outputPath)
    {
        return Task.Run(() =>
        {
            // Save the diagram with the required SaveFileFormat argument.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        });
    }

    static async Task Main(string[] args)
    {
        // Validate input arguments.
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
            Console.Error.WriteLine($"Error: Input file '{inputPath}' does not exist.");
            return;
        }

        try
        {
            // Asynchronously load the diagram.
            Diagram diagram = await LoadDiagramAsync(inputPath);

            // Asynchronously update window properties.
            await UpdateWindowPropertiesAsync(diagram);

            // Asynchronously save the modified diagram.
            await SaveDiagramAsync(diagram, outputPath);

            Console.WriteLine($"Diagram successfully processed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}