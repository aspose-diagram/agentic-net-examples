using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage: pass the Visio file path as the first argument.
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to a Visio file.");
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
        /// <param name="filePath">Path to the Visio document.</param>
        static void ToggleShowGrid(string filePath)
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all windows and invert the ShowGrid setting.
            foreach (Window window in diagram.Windows)
            {
                // ShowGrid uses the BOOL enum (TRUE/FALSE). Flip its value.
                window.ShowGrid = (window.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
            }

            // Save the modified diagram. Use Vsdx format as a common default.
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
    }