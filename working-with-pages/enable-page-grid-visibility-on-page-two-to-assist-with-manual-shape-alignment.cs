using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least two pages (zero‑based index 1)
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page.");
                    return;
                }

                // Access the second page (page index 1) – this is the target page
                Page targetPage = diagram.Pages[1];

                // Ensure there is at least one window; if not, create a default window
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    // Set basic window properties (required for a functional window)
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    newWindow.WindowState = WindowStateValue.Maximized;
                    newWindow.WindowWidth = 1100;
                    newWindow.WindowHeight = 700;
                    diagram.Windows.Add(newWindow);
                }

                // Use the first window to control grid visibility.
                // According to the API, ShowGrid is a BOOL property on Window.
                Window window = diagram.Windows[0];
                window.ShowGrid = BOOL.True;

                // Note: ShowGrid is a global window setting; it affects the view of pages.
                // There is no direct per‑page grid visibility property in Aspose.Diagram.
                // The above setting enables the grid for the diagram view, which includes page two.

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}