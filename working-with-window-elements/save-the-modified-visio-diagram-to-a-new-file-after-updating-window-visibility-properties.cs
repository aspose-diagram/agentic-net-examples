using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_modified.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Update visibility properties for all windows in the diagram
                foreach (Window window in diagram.Windows)
                {
                    // Show or hide UI elements using BOOL enumeration
                    window.ShowGrid = BOOL.True;                 // Show grid
                    window.ShowGuides = BOOL.False;              // Hide guides
                    window.ShowRulers = BOOL.True;               // Show rulers
                    window.ShowPageBreaks = BOOL.False;          // Hide page breaks
                    window.ShowConnectionPoints = BOOL.True;     // Show connection points
                    window.DynamicGridEnabled = BOOL.True;       // Enable dynamic grid

                    // Optionally set the window state (e.g., maximized)
                    window.WindowState = WindowStateValue.Maximized;
                }

                // Save the modified diagram to a new file using the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }