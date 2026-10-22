using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the number of windows to add for the performance test.
        const int windowCount = 5000;

        // Define output file path.
        string outputPath = "PerformanceTestOutput.vsdx";

        // Guard: ensure we can write to the output directory.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required.
            using (Diagram diagram = new Diagram())
            {
                // -----------------------------------------------------------------
                // Approach 1: Add windows individually (new Window instance each time).
                // -----------------------------------------------------------------
                Stopwatch swIndividual = Stopwatch.StartNew();

                for (int i = 0; i < windowCount; i++)
                {
                    // Create a new window and set basic properties.
                    Window win = new Window
                    {
                        // Set the window type to Drawing.
                        WindowType = WindowTypeValue.Drawing,
                        // Use Maximized state (Normal state is not available).
                        WindowState = WindowStateValue.Maximized,
                        // Assign arbitrary size and position.
                        WindowWidth = 800,
                        WindowHeight = 600,
                        WindowLeft = 0,
                        WindowTop = 0
                    };

                    // Add the window to the diagram's window collection.
                    diagram.Windows.Add(win);
                }

                swIndividual.Stop();
                Console.WriteLine($"Individual addition time for {windowCount} windows: {swIndividual.ElapsedMilliseconds} ms");

                // -----------------------------------------------------------------
                // Approach 2: Add windows in a loop using a pre‑created window template.
                // This reduces object allocation overhead by reusing a single Window instance.
                // -----------------------------------------------------------------
                // First, clear previously added windows to have a fair comparison.
                diagram.Windows.Clear();

                Stopwatch swLoop = Stopwatch.StartNew();

                // Create a template window once.
                Window templateWin = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 800,
                    WindowHeight = 600,
                    WindowLeft = 0,
                    WindowTop = 0
                };

                for (int i = 0; i < windowCount; i++)
                {
                    // Clone the template window for each addition.
                    // Since Window does not provide a Clone method, we create a new instance
                    // and copy the property values manually (still cheaper than setting all
                    // properties from scratch each iteration).
                    Window win = new Window
                    {
                        WindowType = templateWin.WindowType,
                        WindowState = templateWin.WindowState,
                        WindowWidth = templateWin.WindowWidth,
                        WindowHeight = templateWin.WindowHeight,
                        WindowLeft = templateWin.WindowLeft,
                        WindowTop = templateWin.WindowTop
                    };

                    diagram.Windows.Add(win);
                }

                swLoop.Stop();
                Console.WriteLine($"Loop addition time for {windowCount} windows: {swLoop.ElapsedMilliseconds} ms");

                // Save the resulting diagram (contains windows from the second approach).
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}