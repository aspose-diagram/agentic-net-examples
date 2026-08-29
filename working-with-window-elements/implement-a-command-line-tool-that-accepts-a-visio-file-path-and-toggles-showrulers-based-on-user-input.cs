using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <VisioFilePath> <on|off|toggle>");
            return;
        }

        string inputPath = args[0];
        string command = args[1].Trim().ToLowerInvariant();

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window; create one if none exist
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

        // Access the first window (global settings are per window)
        Window win = diagram.Windows[0];

        // Determine the desired state
        BOOL newState;
        if (command == "on" || command == "true" || command == "1")
        {
            newState = BOOL.True;
        }
        else if (command == "off" || command == "false" || command == "0")
        {
            newState = BOOL.False;
        }
        else if (command == "toggle")
        {
            newState = (win.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;
        }
        else
        {
            Console.WriteLine("Invalid command. Use 'on', 'off', or 'toggle'.");
            return;
        }

        // Apply the new ShowRulers setting
        win.ShowRulers = newState;
        Console.WriteLine($"ShowRulers set to {(newState == BOOL.True ? "True" : "False")}.");

        // Save the diagram (overwrite the original file)
        try
        {
            diagram.Save(inputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
