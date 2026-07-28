using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; if not, create a default drawing window
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

            while (true)
            {
                // Display current ShowGrid state
                Console.WriteLine($"Current ShowGrid state: {activeWindow.ShowGrid}");

                // Simple console UI panel
                Console.WriteLine("Options:");
                Console.WriteLine("  T - Toggle ShowGrid");
                Console.WriteLine("  S - Save diagram");
                Console.WriteLine("  Q - Quit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine()?.Trim().ToUpperInvariant();

                if (choice == "T")
                {
                    // Toggle between BOOL.True and BOOL.False
                    activeWindow.ShowGrid = (activeWindow.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine("ShowGrid state toggled.");
                }
                else if (choice == "S")
                {
                    // Save the diagram with the updated window settings
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }
                else if (choice == "Q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

                Console.WriteLine(); // Blank line for readability
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
