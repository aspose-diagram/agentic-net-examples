using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the diagram file path (argument or user input)
        string diagramPath;
        if (args.Length > 0)
        {
            diagramPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio diagram file: ");
            diagramPath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one window; if not, create a default one
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 1100;
                defaultWindow.WindowHeight = 700;
                diagram.Windows.Add(defaultWindow);
            }

            // Use the first window as the active window
            Window activeWindow = diagram.Windows[0];

            // Interactive console UI panel
            while (true)
            {
                // Reflect current ShowGrid state
                string gridState = activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled";
                Console.WriteLine("\n--- ShowGrid UI Panel ---");
                Console.WriteLine($"Current ShowGrid state: {gridState}");
                Console.WriteLine("Options:");
                Console.WriteLine("  T - Toggle ShowGrid");
                Console.WriteLine("  Q - Quit");
                Console.Write("Select an option: ");
                string input = Console.ReadLine()?.Trim().ToUpperInvariant();

                if (input == "Q")
                {
                    break;
                }
                else if (input == "T")
                {
                    // Toggle the ShowGrid property
                    activeWindow.ShowGrid = activeWindow.ShowGrid == BOOL.True ? BOOL.False : BOOL.True;
                    Console.WriteLine($"ShowGrid has been {(activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled")}.");

                    // Save the updated diagram to a new file
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(diagramPath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(diagramPath) + "_updated.vsdx");

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to: {outputPath}");
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 'T' to toggle or 'Q' to quit.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}