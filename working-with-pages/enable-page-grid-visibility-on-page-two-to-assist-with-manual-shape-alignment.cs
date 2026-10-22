using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram has at least two pages
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page.");
                    return;
                }

                // Ensure there is at least one window; if not, create a default window
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    newWindow.WindowState = WindowStateValue.Maximized;
                    newWindow.WindowWidth = 1100;
                    newWindow.WindowHeight = 700;
                    diagram.Windows.Add(newWindow);
                }

                // Enable grid visibility. The ShowGrid property is on Window, not Page.
                // This setting applies globally; page‑specific grid visibility is not supported.
                Window window = diagram.Windows[0];
                window.ShowGrid = BOOL.True;

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Grid visibility enabled (global) and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}