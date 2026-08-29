using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage: pass the path to a Visio file as the first argument.
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to a Visio file.");
                return;
            }

            string filePath = args[0];

            try
            {
                ToggleShowGrid(filePath);
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
        /// <param name="path">Full path to the Visio file.</param>
        public static void ToggleShowGrid(string path)
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(path);

            // Ensure there is at least one window; if none, create a default drawing window.
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 1100,
                    WindowHeight = 700
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Iterate over all windows and toggle the ShowGrid property.
            foreach (Window window in diagram.Windows)
            {
                // ShowGrid uses the BOOL enum (TRUE/FALSE).
                window.ShowGrid = (window.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
            }

            // Save the modified diagram. Using VSDX as a common format.
            diagram.Save(path, SaveFileFormat.Vsdx);
        }
    }