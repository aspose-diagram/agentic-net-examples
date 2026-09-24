using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage: provide the path to a Visio file.
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to a Visio file as an argument.");
                return;
            }

            string visioPath = args[0];

            try
            {
                ToggleShowGrid(visioPath);
                Console.WriteLine("ShowGrid property toggled for all windows.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads a Visio diagram, toggles the ShowGrid flag for every open window,
        /// and saves the diagram back to the same file.
        /// </summary>
        /// <param name="filePath">Path to the Visio file.</param>
        static void ToggleShowGrid(string filePath)
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Ensure there is at least one window; if none, add a default drawing window.
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 1100,
                    WindowHeight = 700,
                    ShowGrid = BOOL.True // initial value; will be toggled below
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Iterate through all windows and toggle the ShowGrid property.
            foreach (Window window in diagram.Windows)
            {
                window.ShowGrid = (window.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
            }

            // Save the modified diagram back to the original file (VSDX format).
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
    }