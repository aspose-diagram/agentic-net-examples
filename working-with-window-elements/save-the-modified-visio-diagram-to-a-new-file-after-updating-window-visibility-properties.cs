using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one window; create one if none exist
            if (diagram.Windows.Count == 0)
            {
                Window newWindow = new Window();
                newWindow.WindowType = WindowTypeValue.Drawing;
                newWindow.WindowState = WindowStateValue.Maximized;
                newWindow.WindowWidth = 1100;
                newWindow.WindowHeight = 700;
                diagram.Windows.Add(newWindow);
            }

            // Update visibility-related properties for all windows in the diagram
            foreach (Window win in diagram.Windows)
            {
                win.ShowGrid = BOOL.True;                 // Show grid lines
                win.ShowGuides = BOOL.True;               // Show alignment guides
                win.ShowRulers = BOOL.True;               // Show rulers
                win.ShowPageBreaks = BOOL.True;           // Show page break indicators
                win.DynamicGridEnabled = BOOL.True;       // Enable dynamic grid
                win.ShowConnectionPoints = BOOL.True;     // Show connection points
            }

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
