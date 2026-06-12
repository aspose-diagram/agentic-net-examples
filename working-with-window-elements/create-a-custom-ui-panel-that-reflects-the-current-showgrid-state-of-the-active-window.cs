using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Ensure there is at least one window; if not, create a default drawing window
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    defaultWindow.WindowState = WindowStateValue.Maximized;
                    defaultWindow.WindowWidth = 800;
                    defaultWindow.WindowHeight = 600;
                    diagram.Windows.Add(defaultWindow);
                }

                // Use the first window as the active window for this example
                Window activeWindow = diagram.Windows[0];

                while (true)
                {
                    // Display current ShowGrid state
                    Console.WriteLine($"Current ShowGrid state: {(activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled")}");

                    // Prompt user for action
                    Console.WriteLine("Enter 't' to toggle ShowGrid, 's' to save and exit, or any other key to refresh:");
                    string input = Console.ReadLine();

                    if (string.Equals(input, "t", StringComparison.OrdinalIgnoreCase))
                    {
                        // Toggle the ShowGrid property
                        activeWindow.ShowGrid = (activeWindow.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
                        Console.WriteLine("ShowGrid state toggled.");
                    }
                    else if (string.Equals(input, "s", StringComparison.OrdinalIgnoreCase))
                    {
                        // Save the diagram with the updated window settings
                        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                        Console.WriteLine("Diagram saved to 'output.vsdx'. Exiting.");
                        break;
                    }
                    else
                    {
                        // Refresh display
                        Console.Clear();
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }