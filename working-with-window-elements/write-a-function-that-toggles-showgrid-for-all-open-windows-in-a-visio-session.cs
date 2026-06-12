using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string inputPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Toggle the ShowGrid setting for all open windows
                ToggleShowGrid(diagram);

                // Save the modified diagram (optional)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Toggles the ShowGrid property for every window in the diagram
        static void ToggleShowGrid(Diagram diagram)
        {
            foreach (Window window in diagram.Windows)
            {
                // ShowGrid uses the BOOL enum; flip its value
                window.ShowGrid = window.ShowGrid == BOOL.True ? BOOL.False : BOOL.True;
            }
        }
    }