using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; if not, create a default drawing window
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 1100,
                    WindowHeight = 700
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Restore default visibility settings for each window
            foreach (Window window in diagram.Windows)
            {
                window.ShowGrid = BOOL.True;
                window.ShowGuides = BOOL.True;
                window.ShowRulers = BOOL.True;
                window.ShowPageBreaks = BOOL.True;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with default window settings to: {outputPath}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
