using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one window; create one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    // Set the window type to a drawing window (required for visibility settings)
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(newWindow);
                }

                // Access the first window in the collection
                Window window = diagram.Windows[0];

                // Update visibility properties using BOOL enum values
                window.ShowGrid = BOOL.True;                // Show grid
                window.ShowGuides = BOOL.True;              // Show guides
                window.ShowRulers = BOOL.True;              // Show rulers
                window.ShowPageBreaks = BOOL.True;          // Show page breaks
                window.ShowConnectionPoints = BOOL.True;    // Show connection points
                window.DynamicGridEnabled = BOOL.True;      // Enable dynamic grid

                // Save the modified diagram to a new file
                string outputPath = "output_modified.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }