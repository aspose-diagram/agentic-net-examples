using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to an existing diagram (if any)
        string filePath = "sample.vsdx";

        Diagram diagram;
        if (File.Exists(filePath))
        {
            // Load existing diagram
            diagram = new Diagram(filePath);
        }
        else
        {
            // Create a new empty diagram
            diagram = new Diagram();

            // Ensure the diagram has at least one window
            Window win = new Window();
            win.WindowType = WindowTypeValue.Drawing;
            win.WindowState = WindowStateValue.Maximized;
            win.WindowWidth = 1100;
            win.WindowHeight = 700;
            diagram.Windows.Add(win);
        }

        // If for some reason there are still no windows, add a default one
        if (diagram.Windows.Count == 0)
        {
            Window win = new Window();
            win.WindowType = WindowTypeValue.Drawing;
            win.WindowState = WindowStateValue.Maximized;
            win.WindowWidth = 1100;
            win.WindowHeight = 700;
            diagram.Windows.Add(win);
        }

        // Simple console UI loop to reflect and toggle ShowGrid state
        while (true)
        {
            Window activeWindow = diagram.Windows[0];
            Console.WriteLine("Current ShowGrid state: " + (activeWindow.ShowGrid == BOOL.True ? "Enabled" : "Disabled"));
            Console.WriteLine("Enter 't' to toggle ShowGrid, 's' to save and exit, or any other key to refresh.");

            string input = Console.ReadLine();
            if (input == null) continue;

            input = input.Trim().ToLower();

            if (input == "t")
            {
                // Toggle the ShowGrid property
                activeWindow.ShowGrid = (activeWindow.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
                Console.WriteLine("ShowGrid toggled.");
            }
            else if (input == "s")
            {
                // Save the diagram and exit
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to output.vsdx");
                break;
            }
            else
            {
                // Refresh display
                continue;
            }
        }
    }
}
