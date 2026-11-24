using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <VisioFilePath> [show|hide]");
            return;
        }

        string filePath = args[0];
        // Guard against missing input file
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        // Determine the desired ShowRulers state
        BOOL desiredState;
        if (args.Length >= 2)
        {
            if (!TryParseShowRulersArgument(args[1], out desiredState))
            {
                Console.Error.WriteLine("Error: Invalid ShowRulers argument. Use 'show', 'hide', 'true', 'false', '1', or '0'.");
                return;
            }
        }
        else
        {
            Console.Write("Enter desired ShowRulers state (show/hide): ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!TryParseShowRulersArgument(input, out desiredState))
            {
                Console.Error.WriteLine("Error: Invalid input. Expected 'show' or 'hide'.");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(filePath))
            {
                // Ensure there is at least one window; create one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    newWindow.WindowState = WindowStateValue.Maximized;
                    diagram.Windows.Add(newWindow);
                }

                // Toggle the ShowRulers property on the first window
                diagram.Windows[0].ShowRulers = desiredState;

                // Save the diagram back to the original file (overwrites)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Successfully set ShowRulers to {(desiredState == BOOL.True ? "True" : "False")} and saved the file.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }

    // Helper method to convert user input into BOOL values
    private static bool TryParseShowRulersArgument(string arg, out BOOL result)
    {
        result = BOOL.False; // default
        if (string.IsNullOrWhiteSpace(arg))
            return false;

        string normalized = arg.Trim().ToLowerInvariant();
        switch (normalized)
        {
            case "show":
            case "true":
            case "1":
                result = BOOL.True;
                return true;
            case "hide":
            case "false":
            case "0":
                result = BOOL.False;
                return true;
            default:
                return false;
        }
    }
}