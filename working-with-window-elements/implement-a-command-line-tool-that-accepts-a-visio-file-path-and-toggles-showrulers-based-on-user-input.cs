using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get Visio file path from command‑line argument or prompt the user
            string filePath;
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio file: ");
                filePath = Console.ReadLine()?.Trim();
            }

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                Console.WriteLine("File not found. Exiting.");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Ensure there is at least one window; if not, create a default drawing window
            if (diagram.Windows.Count == 0)
            {
                Window newWindow = new Window();
                newWindow.WindowType = WindowTypeValue.Drawing;
                newWindow.WindowState = WindowStateValue.Maximized;
                diagram.Windows.Add(newWindow);
            }

            // Work with the first window (Visio uses the first window for UI settings)
            Window window = diagram.Windows[0];

            // Show current ruler state
            Console.WriteLine($"Current ShowRulers setting: {(window.ShowRulers == BOOL.True ? "On" : "Off")}");

            // Ask user for desired state
            Console.Write("Enter 'on' to show rulers or 'off' to hide rulers: ");
            string input = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (input == "on")
            {
                window.ShowRulers = BOOL.True;
                Console.WriteLine("Rulers will be shown.");
            }
            else if (input == "off")
            {
                window.ShowRulers = BOOL.False;
                Console.WriteLine("Rulers will be hidden.");
            }
            else
            {
                Console.WriteLine("Invalid input. No changes made.");
                return;
            }

            // Save the diagram back to the same file (using Vsdx format)
            diagram.Save(filePath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
    }