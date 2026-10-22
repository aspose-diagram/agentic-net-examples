using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path (same folder, suffix "_updated.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Asynchronously load the diagram
            Diagram diagram = LoadDiagramAsync(inputPath).GetAwaiter().GetResult();

            // Asynchronously update window properties
            UpdateWindowPropertiesAsync(diagram).GetAwaiter().GetResult();

            // Asynchronously save the modified diagram
            SaveDiagramAsync(diagram, outputPath).GetAwaiter().GetResult();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Asynchronously loads a Visio diagram from the specified file path
    private static async Task<Diagram> LoadDiagramAsync(string path)
    {
        return await Task.Run(() =>
        {
            try
            {
                // Load the diagram using Aspose.Diagram constructor
                Diagram diagram = new Diagram(path);
                return diagram;
            }
            catch (Exception ex)
            {
                // Propagate loading errors
                throw new Exception($"Failed to load diagram: {ex.Message}", ex);
            }
        });
    }

    // Asynchronously updates window properties of the loaded diagram
    private static async Task UpdateWindowPropertiesAsync(Diagram diagram)
    {
        await Task.Run(() =>
        {
            try
            {
                // Ensure there is at least one window; add one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    newWindow.WindowState = WindowStateValue.Maximized;
                    newWindow.WindowWidth = 1200;
                    newWindow.WindowHeight = 800;
                    diagram.Windows.Add(newWindow);
                }

                // Access the first window
                Window window = diagram.Windows[0];

                // Update visibility and grid settings using BOOL values
                window.ShowGrid = BOOL.True;
                window.ShowGuides = BOOL.True;
                window.ShowRulers = BOOL.True;
                window.ShowPageBreaks = BOOL.True;
                window.DynamicGridEnabled = BOOL.True;
                window.ShowConnectionPoints = BOOL.True;
            }
            catch (Exception ex)
            {
                // Propagate update errors
                throw new Exception($"Failed to update window properties: {ex.Message}", ex);
            }
        });
    }

    // Asynchronously saves the diagram to the specified output path in VSDX format
    private static async Task SaveDiagramAsync(Diagram diagram, string outputPath)
    {
        await Task.Run(() =>
        {
            try
            {
                // Save the diagram using the VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Propagate saving errors
                throw new Exception($"Failed to save diagram: {ex.Message}", ex);
            }
        });
    }
}