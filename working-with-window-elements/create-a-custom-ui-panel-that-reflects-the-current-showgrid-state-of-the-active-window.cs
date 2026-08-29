using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Ensure there is at least one window; if not, add a default drawing window
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 800,
                    WindowHeight = 600,
                    ShowGrid = BOOL.True // initial grid visibility
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Use the first window as the active window
            Window activeWindow = diagram.Windows[0];

            while (true)
            {
                // Display current ShowGrid state
                Console.WriteLine($"Current ShowGrid state: {(activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled")}");
                Console.WriteLine("Enter 't' to toggle grid visibility, or any other key to exit.");

                string input = Console.ReadLine();
                if (input == null || input.ToLower() != "t")
                {
                    break;
                }

                // Toggle the ShowGrid property
                activeWindow.ShowGrid = activeWindow.ShowGrid == BOOL.True ? BOOL.False : BOOL.True;

                // Optionally, save the diagram to reflect the change
                try
                {
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully with updated grid state.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving diagram: {ex.Message}");
                }

                Console.WriteLine(); // Blank line for readability
            }

            Console.WriteLine("Exiting UI panel.");
        }
    }