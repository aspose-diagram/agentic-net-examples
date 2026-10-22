using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least the file path argument is provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <VisioFilePath> [on|off]");
            return;
        }

        string filePath = args[0];

        // Guard: verify the Visio file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Determine the desired ShowRulers setting from arguments or interactive input
        string toggleInput = args.Length > 1 ? args[1] : null;
        if (string.IsNullOrWhiteSpace(toggleInput))
        {
            Console.Write("Enter 'on' to show rulers, 'off' to hide: ");
            toggleInput = Console.ReadLine();
        }

        if (toggleInput == null)
        {
            Console.Error.WriteLine("No input provided for ShowRulers setting.");
            return;
        }

        toggleInput = toggleInput.Trim().ToLowerInvariant();

        BOOL showRulersValue;
        if (toggleInput == "on" || toggleInput == "true" || toggleInput == "1" || toggleInput == "show")
        {
            showRulersValue = BOOL.True;
        }
        else if (toggleInput == "off" || toggleInput == "false" || toggleInput == "0" || toggleInput == "hide")
        {
            showRulersValue = BOOL.False;
        }
        else
        {
            Console.Error.WriteLine("Invalid value for ShowRulers. Use 'on' or 'off'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(filePath))
            {
                // Ensure there is at least one Window object; create one if missing
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    newWindow.WindowState = WindowStateValue.Maximized;
                    diagram.Windows.Add(newWindow);
                }

                // Access the first window and set its ShowRulers property
                Window window = diagram.Windows[0];
                window.ShowRulers = showRulersValue;

                // Save the modified diagram, overwriting the original file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"ShowRulers set to {(showRulersValue == BOOL.True ? "On" : "Off")} for file: {filePath}");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}