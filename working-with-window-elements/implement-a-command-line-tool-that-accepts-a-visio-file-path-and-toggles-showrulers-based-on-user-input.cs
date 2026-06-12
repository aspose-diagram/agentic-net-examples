using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path was provided
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: VisioRulerToggle <VisioFilePath>");
            return;
        }

        string filePath = args[0];
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window; if not, create a default drawing window
        if (diagram.Windows.Count == 0)
        {
            Window newWindow = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                // Use a defined enum value
                WindowState = WindowStateValue.Maximized
            };
            diagram.Windows.Add(newWindow);
        }

        // Use the first window (typically the drawing window)
        Window window = diagram.Windows[0];

        // Prompt the user for the desired ruler visibility
        Console.WriteLine("Enter 'show' to display rulers, 'hide' to hide rulers, or 'toggle' to invert the current setting:");
        string input = Console.ReadLine()?.Trim().ToLowerInvariant();

        // Apply the requested change
        if (input == "show")
        {
            window.ShowRulers = BOOL.True;
        }
        else if (input == "hide")
        {
            window.ShowRulers = BOOL.False;
        }
        else // toggle or any other input
        {
            window.ShowRulers = (window.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;
        }

        try
        {
            // Save the diagram back to the same file (using VSDX format)
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            return;
        }

        Console.WriteLine($"ShowRulers is now set to {(window.ShowRulers == BOOL.True ? "True" : "False")} and the file has been saved.");
    }
}