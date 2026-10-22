using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect a diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio diagram file as an argument.");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch to handle any Aspose.Diagram exceptions.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window; if not, create a default drawing window.
        if (diagram.Windows.Count == 0)
        {
            try
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 1100;
                defaultWindow.WindowHeight = 700;
                diagram.Windows.Add(defaultWindow);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating default window: {ex.Message}");
                return;
            }
        }

        // Use the first window as the "active" window for this example.
        Window activeWindow = diagram.Windows[0];

        // Display the current ShowGrid state.
        Console.WriteLine($"Current ShowGrid state: {(activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled")}");

        // Prompt the user to toggle the ShowGrid setting.
        Console.Write("Toggle ShowGrid? (y/n): ");
        string response = Console.ReadLine()?.Trim().ToLowerInvariant();

        if (response == "y" || response == "yes")
        {
            // Toggle the ShowGrid property.
            activeWindow.ShowGrid = (activeWindow.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
            Console.WriteLine($"ShowGrid has been set to: {(activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled")}");
        }
        else
        {
            Console.WriteLine("No changes made to ShowGrid.");
        }

        // Prepare output path.
        string directory = Path.GetDirectoryName(inputPath) ?? "";
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_updated.vsdx");

        // Save the diagram with the updated window settings.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}