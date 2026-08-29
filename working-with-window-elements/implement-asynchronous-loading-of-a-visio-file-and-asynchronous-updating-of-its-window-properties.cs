using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the Visio file path as an argument.");
                return;
            }

            string inputPath = args[0];

            // Asynchronously load the diagram
            Diagram diagram = await LoadDiagramAsync(inputPath);

            // Asynchronously update window properties
            await UpdateWindowPropertiesAsync(diagram);

            // Save the updated diagram (optional, to verify changes)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                "Updated_" + Path.GetFileName(inputPath));

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() =>
        {
            // Load diagram from file path
            return new Diagram(path);
        });
    }

    private static Task UpdateWindowPropertiesAsync(Diagram diagram)
    {
        return Task.Run(() =>
        {
            // Ensure at least one window exists; create one if necessary
            if (diagram.Windows.Count == 0)
            {
                var newWindow = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 1100,
                    WindowHeight = 700
                };
                diagram.Windows.Add(newWindow);
            }

            // Update properties of the first window
            Window window = diagram.Windows[0];
            window.ShowGrid = BOOL.True;
            window.ShowGuides = BOOL.True;
            window.ShowRulers = BOOL.True;
            window.ShowPageBreaks = BOOL.True;
            window.DynamicGridEnabled = BOOL.True;
            window.ShowConnectionPoints = BOOL.True;
            window.WindowState = WindowStateValue.Maximized;
        });
    }
}
