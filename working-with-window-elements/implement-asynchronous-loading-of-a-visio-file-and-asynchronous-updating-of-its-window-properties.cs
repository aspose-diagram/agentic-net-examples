using System;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Asynchronously loads a Visio diagram from the specified file path.
        private static async Task<Diagram> LoadDiagramAsync(string filePath)
        {
            // Aspose.Diagram does not provide native async methods, so wrap the constructor in Task.Run.
            return await Task.Run(() => new Diagram(filePath));
        }

        // Asynchronously updates window properties for the given diagram.
        private static async Task UpdateWindowPropertiesAsync(Diagram diagram)
        {
            await Task.Run(() =>
            {
                // Ensure there is at least one window; if not, create a default drawing window.
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

                // Update properties of the first window.
                Window window = diagram.Windows[0];
                window.ShowGrid = BOOL.True;
                window.ShowGuides = BOOL.True;
                window.ShowRulers = BOOL.True;
                window.ShowPageBreaks = BOOL.True;
                window.DynamicGridEnabled = BOOL.True;
                window.ShowConnectionPoints = BOOL.True;
            });
        }

        // Entry point of the console application.
        static async Task Main(string[] args)
        {
            try
            {

                // Example file path; replace with actual path or pass as a command‑line argument.
                string inputPath = args.Length > 0 ? args[0] : "example.vsdx";

                // Load the diagram asynchronously.
                Diagram diagram = await LoadDiagramAsync(inputPath);
                Console.WriteLine("Diagram loaded successfully.");

                // Update window properties asynchronously.
                await UpdateWindowPropertiesAsync(diagram);
                Console.WriteLine("Window properties updated.");

                // Optionally, save the modified diagram.
                // diagram.Save("modified.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }